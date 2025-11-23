using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Configuration;
using FoodandBeverage.Classes;
using System.Collections.Generic;

namespace FoodandBeverage
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            bool instanceCountOne = false;
            using (Mutex mtex = new Mutex(true, "FoodandBeverage", out instanceCountOne))
            {
                if (instanceCountOne)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    string crystalValue = "0";
                    crystalValue = ConfigurationManager.AppSettings["CrystalInstalled"];
                    if (crystalValue == "0")
                    {
                        FolderPermissionSetter.EnsureAppFolderPermissions();
                        UpdateAppConfig();
                        FoodandBeverage.Classes.CrystalInstaller.EnsureCrystalInstalled();
                    }

                    Application.Run(new Forms.FrmloginTablet());
                }
                else
                {
                    // Check if FoodandBeverage application is running
                    if (IsApplicationRunning("FoodandBeverage"))
                    {
                        MessageBox.Show("An instance of the application is already running.", "Food and Beverage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BringExistingInstanceToFront();
                    }
                    else
                    {
                        MessageBox.Show("Application not found or closed.", "Food and Beverage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public static void UpdateAppConfig()
        {
            var updates = new Dictionary<string, string>
                {
                    { "CrystalInstalled", "1" },
                    { "POSpagescount", "15" },
                    { "PrintpathValue", "2" }
                };
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (var item in updates)
            {
                if (config.AppSettings.Settings[item.Key] != null)
                    config.AppSettings.Settings[item.Key].Value = item.Value;
                else
                    config.AppSettings.Settings.Add(item.Key, item.Value);
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static bool IsApplicationRunning(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            Process oldestProcess = null;
            if (processes.Length > 0)
            {
                oldestProcess = processes.OrderBy(p => p.StartTime).FirstOrDefault();
            }
            if (oldestProcess != null)
            {
                string mainModuleFileName = oldestProcess.MainModule.FileName;
                if (mainModuleFileName.EndsWith("FoodandBeverage.exe", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        private const int SW_MAXIMIZE = 3;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void BringExistingInstanceToFront()
        {
            try
            {

                //var p = System.Diagnostics.Process.GetProcessesByName("FoodandBeverage").OrderBy(pr => pr.StartTime).FirstOrDefault();
                //if (p != null)
                //{
                //    ShowWindow(p.MainWindowHandle, SW_MAXIMIZE);
                //}
                //var p = System.Diagnostics.Process.GetProcessesByName("FoodandBeverage").OrderBy(pr => pr.StartTime).FirstOrDefault();
                //if (p != null)
                //{
                //    var element = AutomationElement.FromHandle(p.MainWindowHandle);
                //    if (element != null)
                //    {
                //        var pattern = element.GetCurrentPattern(WindowPattern.Pattern) as WindowPattern;
                //        if (pattern != null)
                //            pattern.SetWindowVisualState(WindowVisualState.Maximized);
                //    }
                //}

            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., access denied, process not found, etc.)
                MessageBox.Show($"Error bringing existing instance to front: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
