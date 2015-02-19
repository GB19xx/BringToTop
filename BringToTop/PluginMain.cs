using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advanced_Combat_Tracker;
using System.Windows.Forms;

namespace BringToTop
{
    public class PluginMain : IActPluginV1
    {
        readonly string configPath = System.IO.Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\BringToTop.config.xml");

        TabPage tabPage;
        Label label;
        ControlPanel controlPanel;

        //PluginConfig config;
        internal PluginConfig Config { get; set; }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            try
            {
                this.tabPage = pluginScreenSpace;
                this.label = pluginStatusText;

                pluginScreenSpace.Text = "BringToTop";

                LoadConfig();
                this.controlPanel = new ControlPanel(this, this.Config);
                this.controlPanel.Dock = DockStyle.Fill;
                this.tabPage.Controls.Add(this.controlPanel);
                
                this.label.Text = "Started.";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

                throw;
            }
        }

        public void DeInitPlugin()
        {
            ActGlobals.oFormActMain.OnLogLineRead -= this.controlPanel.oFormActMain_OnLogLineRead; 
            this.controlPanel.Dispose();
            SaveConfig();

            this.label.Text = "Ended.";
        }

        private void LoadConfig()
        {            
            this.Config = PluginConfig.Load(configPath);
        }

        private void SaveConfig()
        {
            PluginConfig.Save(configPath, this.Config);
        }
    }
}
