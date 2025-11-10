using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    
    public class AutoLogoutManager : IDisposable
    {
        private System.Timers.Timer inactivityTimer;
        private System.Windows.Forms.Timer countdownTimer;
        private int timeoutSeconds;
        private int remainingSeconds;
        private Form trackedForm;
        private string formname = "";
        private bool disposed = false;

        public void Start(Form form)
        {
            try
            {
                Stop();
                trackedForm = form;
                formname = form.Name;

                decimal timeoutMinutes = FoodandBeverage.Forms.FrmLogin.AutoLogoutIdleTimeoutMinutes;
                if (timeoutMinutes <= 0)
                    return;

                timeoutSeconds = (int)(timeoutMinutes * 60m);
                remainingSeconds = timeoutSeconds;
                double timeoutMilliseconds = timeoutSeconds * 1000;

                // Main inactivity timer (triggers logout)
                inactivityTimer = new System.Timers.Timer(timeoutMilliseconds);
                inactivityTimer.Elapsed += InactivityTimer_Elapsed;
                inactivityTimer.AutoReset = false;
                inactivityTimer.Start();

                // Countdown display timer (updates every second)
                countdownTimer = new System.Windows.Forms.Timer();
                countdownTimer.Interval = 1000;
                countdownTimer.Tick += CountdownTimer_Tick;
                countdownTimer.Start();

                // Hook user activity events (form level)
                form.MouseMove += ResetTimer;
                form.KeyPress += ResetTimer;
                form.MouseClick += ResetTimer;

                HookControlEvents(form);
            }
            catch (Exception ex)
            {
            }
        }

        private void HookControlEvents(Control parent)
        {
            try
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ctrl.MouseMove += ResetTimer;
                    ctrl.MouseClick += ResetTimer;
                    ctrl.KeyPress += ResetTimer;

                    if (ctrl.HasChildren)
                        HookControlEvents(ctrl);
                }
            }
            catch
            {

            }
        }

        private void ResetTimer(object sender, EventArgs e)
        {
            try
            {
                if (disposed) return;

                if (inactivityTimer != null)
                {
                    inactivityTimer.Stop();
                    inactivityTimer.Start();
                }
                remainingSeconds = timeoutSeconds;
            }
            catch
            {
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (disposed) return;

                remainingSeconds--;
                var dash = Application.OpenForms.OfType<FoodandBeverage.Forms.FrmDashboard>().FirstOrDefault();
                if (dash != null && dash.lblinact_timer != null && !dash.lblinact_timer.IsDisposed)
                {
                    int min = remainingSeconds / 60;
                    int sec = remainingSeconds % 60;
                    dash.lblinact_timer.Text = $"{min:D2}:{sec:D2}";
                }
                if (remainingSeconds <= 0)
                {
                    countdownTimer?.Stop();
                    InactivityTimer_Elapsed(this, null);
                }
            }
            catch { }
        }

        private void InactivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (disposed) return;

                inactivityTimer?.Stop();
                countdownTimer?.Stop();

                if (trackedForm == null || trackedForm.IsDisposed)
                    return;

                Form safeInvoker = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.IsHandleCreated);
                if (safeInvoker == null)
                    return;

                safeInvoker.BeginInvoke((Action)(async () =>
                {
                    MessageBox.Show(
                        $"{formname} - You have been automatically logged out due to inactivity.\nPlease login again to continue.",
                        "Auto Logout",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
                    {
                        if (!(openForm is FoodandBeverage.Forms.FrmLogin) && !(openForm is FoodandBeverage.Forms.FrmSplash))
                            openForm.Close();
                    }

                    await Task.Delay(300);

                    var existingLogin = Application.OpenForms.OfType<FoodandBeverage.Forms.FrmLogin>().FirstOrDefault();
                    if (existingLogin == null)
                        new FoodandBeverage.Forms.FrmLogin().Show();
                    else
                    {
                        existingLogin.Opacity = 1;
                        existingLogin.BringToFront();
                        existingLogin.Focus();
                    }
                }));
            }
            catch
            {

            }
        }

        public void Stop()
        {
            try
            {
                if (disposed) return;

                if (inactivityTimer != null)
                {
                    inactivityTimer.Stop();
                    inactivityTimer.Elapsed -= InactivityTimer_Elapsed;
                    inactivityTimer.Dispose();
                    inactivityTimer = null;
                }

                if (countdownTimer != null)
                {
                    countdownTimer.Stop();
                    countdownTimer.Tick -= CountdownTimer_Tick;
                    countdownTimer.Dispose();
                    countdownTimer = null;
                }

                if (trackedForm != null)
                {
                    trackedForm.MouseMove -= ResetTimer;
                    trackedForm.KeyPress -= ResetTimer;
                    trackedForm.MouseClick -= ResetTimer;
                    UnhookControlEvents(trackedForm);
                    trackedForm = null;
                }
                formname = "";
            }
            catch { }
        }

        private void UnhookControlEvents(Control parent)
        {
            try
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ctrl.MouseMove -= ResetTimer;
                    ctrl.MouseClick -= ResetTimer;
                    ctrl.KeyPress -= ResetTimer;

                    if (ctrl.HasChildren)
                        UnhookControlEvents(ctrl);
                }
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Stop();
                disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~AutoLogoutManager()
        {
            Dispose();
        }
    }
}
