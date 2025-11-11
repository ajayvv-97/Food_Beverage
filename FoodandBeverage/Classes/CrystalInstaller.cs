using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Threading.Tasks;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Forms;

namespace FoodandBeverage.Classes
{
    public static class CrystalInstaller
    {
        public static void EnsureCrystalInstalled()
        {
            string exePath32 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Crystalreport", "CrystalRuntime32.msi");
            string exePath64 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Crystalreport", "CrystalRuntime64.msi");

            bool installedSomething = false;

            FrmProgressBar progressForm = new FrmProgressBar();
            progressForm.Shown += (s, e) =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        progressForm.Invoke((Action)(() => progressForm.StartProgress()));
                        if (!IsCrystalInstalled(false) && File.Exists(exePath32))
                        {
                            RunInstallerSync(exePath32);
                        }
                        if (!IsCrystalInstalled(true) && File.Exists(exePath64))
                        {
                            RunInstallerSync(exePath64);
                            installedSomething = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        progressForm.Invoke((Action)(() =>
                        {
                            MessageBox.Show("Error installing Crystal Reports: " + ex.Message,
                                            "Installer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }
                    finally
                    {
                        progressForm.Invoke((Action)(() =>
                        {
                            //if (installedSomething)
                            progressForm.StopProgress();
                            System.Threading.Thread.Sleep(1000);

                            progressForm.Close();

                            if (installedSomething)
                            {
                                DialogResult dr = MessageBox.Show(
                                    "Crystal Reports runtime was installed successfully.\n\n" +
                                    "The application needs to restart for changes to take effect.\n\n" +
                                    "Do you want to restart now?",
                                    "Restart Required",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Information);

                                if (dr == DialogResult.Yes)
                                {
                                    Application.Restart();
                                    Environment.Exit(0);
                                }
                            }
                        }));
                    }
                });
            };

            progressForm.ShowDialog();
        }


        private static bool IsCrystalInstalled(bool is64bit)
        {
            try
            {
                RegistryView view = is64bit ? RegistryView.Registry64 : RegistryView.Registry32;
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view))
                {
                    using (RegistryKey crKey = baseKey.OpenSubKey(@"SOFTWARE\SAP BusinessObjects\Crystal Reports"))
                    {
                        if (crKey != null)
                        {
                            foreach (var subkeyName in crKey.GetSubKeyNames())
                            {
                                using (RegistryKey versionKey = crKey.OpenSubKey(subkeyName))
                                {
                                    if (versionKey != null)
                                    {
                                        var installDir = versionKey.GetValue("InstallDir");
                                        if (installDir != null && Directory.Exists(installDir.ToString()))
                                            return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore errors
            }
            return false;
        }

        private static void RunInstallerSync(string msiPath)
        {
            var process = new Process();
            process.StartInfo.FileName = "msiexec.exe";
            process.StartInfo.Arguments = $"/i \"{msiPath}\" /qn /norestart";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.Verb = "runas"; // Run as admin
            process.Start();
            process.WaitForExit(); // ⏳ Wait until install finishes
        }
    }
}

