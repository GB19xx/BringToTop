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
    public partial class ControlPanel : UserControl,IDisposable
    {
        Regex regexBegun = new Regex(@"(.+ has begun\.$|„.+“ hat begonnen.$|La mission “.+” commence\.$|「.+?」の攻略を開始した。$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        bool isRefresh; 

        PluginMain pluginMain;

        public ControlPanel(PluginMain pluginMain)
        {
            InitializeComponent();

            this.pluginMain = pluginMain;
            ActGlobals.oFormActMain.OnLogLineRead += this.oFormActMain_OnLogLineRead;

            checkBoxForce.Checked = pluginMain.Config.IsForcedToRun;
            RefreshProcess();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                ActGlobals.oFormActMain.OnLogLineRead -= this.oFormActMain_OnLogLineRead;
            }
            base.Dispose(disposing);
        }

        #region Private Events
        private void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (!ActGlobals.oFormActMain.Visible) return;

            if (isImport) return;

            if (logInfo.logLine.ToLower().Contains("btt"))
            {
                // command
                BringToTop();
            }
            else if (pluginMain.Config.IsForcedToRun && (
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

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcess();
        }

        private void buttonToTop_Click(object sender, EventArgs e)
        {
            BringToTop();
        }

        private void listViewProcesses_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (isRefresh) return;

            List<string> checkedItems = new List<string>();
            foreach (ListViewItem checkedItem in listViewProcesses.CheckedItems)
            {
                if (checkedItem.Text.Contains("FFXIVGAME")) continue;
                checkedItems.Add(checkedItem.SubItems[1].Text);
            }

            pluginMain.Config.TaskName = checkedItems;
        }

        private void checkBoxForce_CheckedChanged(object sender, EventArgs e)
        {
            pluginMain.Config.IsForcedToRun = checkBoxForce.Checked;
        }
        #endregion

        #region Private Methods
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

            RefreshProcess();
        }

        private void RefreshProcess()
        {
            isRefresh = true;

            listViewProcesses.Items.Clear();

            Win32API.EnumWindows(new Win32API.EnumWindowsDelegate(EnumWindowCallBack), IntPtr.Zero);

            ListViewItem ffItem = listViewProcesses.FindItemWithText("FFXIVGAME");
            if (ffItem != null)
            {
                ffItem.BackColor = Color.DarkOrange;
                ffItem.Checked = true;
            }
            string[] uncheckedItem = pluginMain.Config.TaskName.ToArray();

            foreach (ListViewItem list in listViewProcesses.Items)
            {
                for (int i = 0; i < pluginMain.Config.TaskName.Count; i++)
                {
                    if (string.IsNullOrEmpty(uncheckedItem[i])) continue;

                    var taskName = pluginMain.Config.TaskName[i];
                    if (list.Text.Contains(taskName) || list.SubItems[1].Text.Contains(taskName))
                    {
                        if (list.SubItems[1].Text.Equals(taskName))
                        {
                            list.Checked = true;

                            uncheckedItem[i] = string.Empty;
                            break;
                        }
                    }
                }
            }

            isRefresh = false;
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

                listViewProcesses.Items.Add(item);
            }

            return true;
        }
        #endregion
    }
}
