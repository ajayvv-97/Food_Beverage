using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    
    class FolderPermissionSetter
    {
        /// <summary>
        /// Ensures the app runs as Admin and sets Everyone -> Full Control on the app folder.
        /// </summary>
        public static void EnsureAppFolderPermissions()
        {
            string folderPath = Application.StartupPath; // Auto-detect app folder

            if (!IsRunAsAdmin())
            {
                try
                {
                    // Relaunch as admin
                    ProcessStartInfo proc = new ProcessStartInfo
                    {
                        FileName = Application.ExecutablePath,
                        UseShellExecute = true,
                        Verb = "runas" // Triggers UAC
                    };

                    Process.Start(proc);
                    Environment.Exit(0); // Exit current non-admin instance
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Administrator rights are required.\n" + ex.Message,
                        "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Now we are Admin, apply permissions
                SetFullControlForEveryone(folderPath);
            }
        }

        private static bool IsRunAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private static void SetFullControlForEveryone(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                DirectoryInfo dInfo = new DirectoryInfo(folderPath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();

                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                FileSystemAccessRule fullControlRule = new FileSystemAccessRule(
                    everyone,
                    FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow);

                dSecurity.AddAccessRule(fullControlRule);
                dInfo.SetAccessControl(dSecurity);

                //MessageBox.Show($"Full permissions applied to:\n{folderPath}",
                //    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting folder permissions:\n" + ex.Message,
                    "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
