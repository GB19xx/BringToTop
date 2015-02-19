using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advanced_Combat_Tracker;

namespace BringToTop
{
    class ActLog : IDisposable
    {
        public ActLog()
        {
            ActGlobals.oFormActMain.OnLogLineRead += this.oFormActMain_OnLogLineRead;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            ActGlobals.oFormActMain.OnLogLineRead -= this.oFormActMain_OnLogLineRead;

            GC.SuppressFinalize(this);
        }

        private void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo)
        {
            if (isImport) return;

            var logLine = logInfo.logLine.Trim();

            if (logLine.Contains(""))
            {
                //
            }
        }
    }
}
