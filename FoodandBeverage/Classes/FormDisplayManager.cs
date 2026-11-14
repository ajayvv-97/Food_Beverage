using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    /// <summary>
    /// Applies the application's standard full-screen behaviour to operational forms.
    /// Small utility windows (search, keypad, progress and splash windows) retain their
    /// designed size so that they remain usable as dialogs.
    /// </summary>
    internal static class FormDisplayManager
    {
        private static Timer _formMonitor;
        private static readonly HashSet<Form> PreparedForms = new HashSet<Form>();

        private static readonly string[] UtilityFormNames =
        {
            "FrmSearch", "Frmkeypad", "Frmkeyboard", "Frmnumdiskeypad",
            "FrmProgressBar", "FrmSplash", "LoadingGIF_Timer",
            "FrmUpcomingPartyBookings"
        };

        public static void Start()
        {
            if (_formMonitor != null)
                return;

            _formMonitor = new Timer();
            _formMonitor.Interval = 250;
            _formMonitor.Tick += MonitorOpenForms;
            _formMonitor.Start();
        }

        private static void MonitorOpenForms(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (form == null || form.IsDisposed || !form.Visible ||
                    !form.TopLevel || PreparedForms.Contains(form))
                    continue;

                if (IsUtilityWindow(form))
                {
                    PreparedForms.Add(form);
                    form.FormClosed += delegate { PreparedForms.Remove(form); };
                    continue;
                }

                ApplyFullScreen(form);
                PreparedForms.Add(form);
                form.FormClosed += delegate { PreparedForms.Remove(form); };
            }
        }

        private static bool IsUtilityWindow(Form form)
        {
            string name = form.GetType().Name;
            return UtilityFormNames.Any(x =>
                string.Equals(x, name, StringComparison.OrdinalIgnoreCase));
        }

        public static void ApplyFullScreen(Form form)
        {
            if (form == null || form.IsDisposed || !form.TopLevel)
                return;

            // Do not change FormBorderStyle: custom title bars and close buttons
            // used by existing forms must continue to work.
            form.StartPosition = FormStartPosition.CenterScreen;
            form.WindowState = FormWindowState.Maximized;
        }
    }
}
