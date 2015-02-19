using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Advanced_Combat_Tracker;

namespace BringToTop
{
    public partial class ControlPanel : UserControl
    {
        Regex regexBegun = new Regex(@"(.+ has begun\.$|„.+“ hat begonnen.$|La mission “.+” commence\.$|「.+?」の攻略を開始した。$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        PluginMain pluginMain;
        PluginConfig config;

        private bool IsForced { get { return this.checkBoxForce.Checked; } }
        private List<string> SelectedItems
        {
            get
            {
                List<string> checkedItems = new List<string>();
                foreach (ListViewItem checkedItem in listViewProcesses.CheckedItems)
                {
                    if (checkedItem.Text.Contains("FFXIVGAME")) continue;
                    checkedItems.Add(checkedItem.SubItems[1].Text);
                }

                return checkedItems;
            }
        }

        public ControlPanel(PluginMain pluginMain, PluginConfig config)
        {
            InitializeComponent();

            this.pluginMain = pluginMain;
            this.config = config;

            checkBoxForce.Checked = this.config.IsForcedToRun;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ActGlobals.oFormActMain.OnLogLineRead += this.oFormActMain_OnLogLineRead;
            this.ParentForm.FormClosing += this.ParentForm_FormClosing;
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ActGlobals.oFormActMain.OnLogLineRead -= this.oFormActMain_OnLogLineRead;
            pluginMain.Config.IsForcedToRun = this.IsForced;
            pluginMain.Config.TaskName = this.SelectedItems;
        }

        internal void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (isImport) return;

            if (logInfo.logLine.ToLower().Contains("btt"))
            {
                // command
                BringToTop();
            }
            else if (this.IsForced && (
                logInfo.logLine.Contains("begun.") || logInfo.logLine.Contains("begonnen.") ||
                logInfo.logLine.Contains("commence.") || logInfo.logLine.Contains("の攻略を開始した。")))
            {
                if (regexBegun.IsMatch(logInfo.logLine))
                {
                    // is begun?
                    BringToTop();
                }
            }
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            RefreshProcess();
        }

        private void buttonToTop_Click(object sender, EventArgs e)
        {
            BringToTop();
        }

        private void BringToTop()
        {
            RefreshProcess();

            ListViewItem ffItem = listViewProcesses.FindItemWithText("FFXIVGAME");
            if (ffItem == null) return;

            int ffIndex = ffItem.Index;
            foreach (ListViewItem checkedItem in listViewProcesses.CheckedItems)
            {
                if (checkedItem.Index <= ffIndex) continue;

                // Bring to top.
                IntPtr handle = (IntPtr)Int32.Parse(checkedItem.SubItems[2].Text, System.Globalization.NumberStyles.HexNumber);
                Win32API.BringWindowToTop(handle);
            }
        }

        private void RefreshProcess()
        {
            listViewProcesses.Items.Clear();

            Win32API.EnumWindows(new Win32API.EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);
        }

        private int GetZOrder(IntPtr hWnd)
        {
            IntPtr handle = hWnd;
            int z = 0;
            do
            {
                z++;
                handle = Win32API.GetWindow(handle, Win32API.GetWindow_Cmd.GW_HWNDPREV);
            } while (handle != IntPtr.Zero);
            return z;
        }

        private bool EnumWindowCallBack(IntPtr hWnd, IntPtr lparam)
        {
            //ウィンドウのタイトルの長さを取得する
            int textLen = Win32API.GetWindowTextLength(hWnd);

            if (0 < textLen && Win32API.IsWindowVisible(hWnd))
            {
                StringBuilder tsb = new StringBuilder(textLen + 1);
                Win32API.GetWindowText(hWnd, tsb, tsb.Capacity);

                StringBuilder csb = new StringBuilder(256);
                Win32API.GetClassName(hWnd, csb, csb.Capacity);

                ListViewItem item = new ListViewItem(csb.ToString());
                item.SubItems.Add(tsb.ToString());
                item.SubItems.Add(hWnd.ToString("X"));
                item.SubItems.Add(GetZOrder(hWnd).ToString());

                if (csb.ToString().Equals("FFXIVGAME"))
                {
                    item.BackColor = Color.DarkOrange;
                    item.Checked = true;
                }
                else
                {
                    if (this.config.TaskName != null)
                    {
                        foreach (string taskName in this.config.TaskName)
                        {
                            if (item.Text.Contains(taskName) || item.SubItems[1].Text.Contains(taskName))
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }

                listViewProcesses.Items.Add(item);
            }

            return true;
        }
    }
}
