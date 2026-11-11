using Dapper;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FoodandBeverage.Services
{
    /// <summary>
    /// Registers a Windows PC against the branch. New workstations must select
    /// their role explicitly so a printer PC is not accidentally registered as Office.
    /// </summary>
    public static class KOTWorkstationRegistration
    {
        public const int Kitchen = 1;
        public const int Counter = 2;
        public const int Office = 3;

        public static int EnsureRegistered(int branchId, int employeeId, IWin32Window owner)
        {
            if (branchId <= 0)
                return 0;

            using (IDbConnection connection = DAL.DapperRepository<object>.OpenConnection())
            {
                int? currentRole = connection.QueryFirstOrDefault<int?>(
                    @"SELECT StationType
                      FROM dbo.tblKOTWorkstation
                      WHERE BranchId = @BranchId
                        AND MachineName = @MachineName
                        AND IsActive = 1;",
                    new
                    {
                        BranchId = branchId,
                        MachineName = Environment.MachineName
                    });

                if (currentRole.HasValue)
                    return currentRole.Value;
            }

            using (KOTWorkstationSetupForm form =
                new KOTWorkstationSetupForm(branchId, employeeId, 0))
            {
                return form.ShowDialog(owner) == DialogResult.OK
                    ? form.SavedStationType
                    : 0;
            }
        }

        /// <summary>
        /// Can be called from an administrator/settings menu to change the
        /// registered role for this PC after its initial registration.
        /// </summary>
        public static bool ConfigureCurrentMachine(
            int branchId, int employeeId, IWin32Window owner)
        {
            int currentRole = Office;

            using (IDbConnection connection = DAL.DapperRepository<object>.OpenConnection())
            {
                int? value = connection.QueryFirstOrDefault<int?>(
                    @"SELECT StationType
                      FROM dbo.tblKOTWorkstation
                      WHERE BranchId = @BranchId
                        AND MachineName = @MachineName;",
                    new
                    {
                        BranchId = branchId,
                        MachineName = Environment.MachineName
                    });

                if (value.HasValue)
                    currentRole = value.Value;
            }

            using (KOTWorkstationSetupForm form =
                new KOTWorkstationSetupForm(branchId, employeeId, currentRole))
            {
                return form.ShowDialog(owner) == DialogResult.OK;
            }
        }
    }

    internal sealed class KOTWorkstationSetupForm : Form
    {
        private readonly int _branchId;
        private readonly int _employeeId;
        private readonly ComboBox _cmbStation;
        private readonly Button _btnSave;
        private readonly Button _btnCancel;

        public int SavedStationType { get; private set; }

        public KOTWorkstationSetupForm(int branchId, int employeeId, int selectedRole)
        {
            _branchId = branchId;
            _employeeId = employeeId;

            Text = "KOT Workstation Registration";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowInTaskbar = false;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(460, 260);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            Label title = new Label
            {
                Text = "Register this workstation",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(39, 48, 69),
                Location = new Point(24, 20),
                Size = new Size(410, 32)
            };

            Label description = new Label
            {
                Text = "Select which printer station this PC belongs to. Office is the default and does not print KOT tickets.",
                ForeColor = Color.FromArgb(90, 99, 115),
                Location = new Point(26, 60),
                Size = new Size(405, 46)
            };

            Label machine = new Label
            {
                Text = "PC: " + Environment.MachineName,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(62, 70, 86),
                Location = new Point(26, 112),
                Size = new Size(405, 24)
            };

            Label stationLabel = new Label
            {
                Text = "Workstation type",
                ForeColor = Color.FromArgb(62, 70, 86),
                Location = new Point(26, 145),
                Size = new Size(130, 25)
            };

            _cmbStation = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(160, 141),
                Size = new Size(270, 30),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            // Do not silently register a new printer PC as Office. A role
            // must be selected explicitly so KOT printing is not disabled by
            // accepting a preselected default.
            _cmbStation.Items.Add(new StationOption(0, "-- Select workstation type --"));
            _cmbStation.Items.Add(new StationOption(
                KOTWorkstationRegistration.Kitchen, "Kitchen printer"));
            _cmbStation.Items.Add(new StationOption(
                KOTWorkstationRegistration.Counter, "Counter printer"));
            _cmbStation.Items.Add(new StationOption(
                KOTWorkstationRegistration.Office, "Office (no KOT printing)"));

            for (int i = 0; i < _cmbStation.Items.Count; i++)
            {
                StationOption option = (StationOption)_cmbStation.Items[i];
                if (option.Value == selectedRole)
                {
                    _cmbStation.SelectedIndex = i;
                    break;
                }
            }

            if (_cmbStation.SelectedIndex < 0)
                _cmbStation.SelectedIndex = 0;

            _btnSave = new Button
            {
                Text = "Register",
                BackColor = Color.FromArgb(91, 65, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(250, 198),
                Size = new Size(90, 36)
            };
            _btnSave.FlatAppearance.BorderSize = 0;
            _btnSave.Click += Save_Click;

            _btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(238, 240, 245),
                ForeColor = Color.FromArgb(48, 55, 70),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(350, 198),
                Size = new Size(80, 36)
            };
            _btnCancel.FlatAppearance.BorderSize = 0;

            Controls.Add(title);
            Controls.Add(description);
            Controls.Add(machine);
            Controls.Add(stationLabel);
            Controls.Add(_cmbStation);
            Controls.Add(_btnSave);
            Controls.Add(_btnCancel);

            AcceptButton = _btnSave;
            CancelButton = _btnCancel;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                StationOption selected = _cmbStation.SelectedItem as StationOption;
                if (selected == null || selected.Value == 0)
                {
                    MessageBox.Show(
                        this,
                        "Please select Kitchen, Counter, or Office before registering this PC.",
                        "KOT Workstation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                using (IDbConnection connection = DAL.DapperRepository<object>.OpenConnection())
                {
                    connection.Execute(
                        "dbo.prdKOTWorkstation_Register",
                        new
                        {
                            BranchId = _branchId,
                            MachineName = Environment.MachineName,
                            StationType = selected.Value,
                            EmployeeId = _employeeId
                        },
                        commandType: CommandType.StoredProcedure);
                }

                SavedStationType = selected.Value;
                MessageBox.Show(
                    this,
                    "This PC has been registered as " + selected.Label + ".",
                    "KOT Workstation Registered",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to register this workstation.\r\n\r\n" + ex.Message +
                    "\r\n\r\nConfirm that PartyBookingKOT_Scheduler.sql has been executed on the database.",
                    "KOT Workstation Registration",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private sealed class StationOption
        {
            public int Value { get; private set; }
            public string Label { get; private set; }

            public StationOption(int value, string label)
            {
                Value = value;
                Label = label;
            }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}