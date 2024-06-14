using CustomMessageBox;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms.UserAccount
{
    public partial class Frmkeypad : Form
    {
        ClsGeneral clsGen = new ClsGeneral();
        public string ICardNo { get; set; }

        private Form callingForm;
        public int typeRfid;
        public Frmkeypad(Form callingForm, int type)
        {
            InitializeComponent();
            this.callingForm = callingForm;
            typeRfid = type;
        }

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string cardNumber = new string(txtkeypadno.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());

                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    // Show an error message to the user
                    MessageBox.Show("Please enter RFID Card No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtkeypadno.Focus();
                    return;
                }
                else if (cardNumber.Length != 16)
                {
                    int currentPosition = txtkeypadno.SelectionStart;
                    // Show an error message to the user
                    MessageBox.Show("RFID Card No. must contain exactly 16 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtkeypadno.Focus();
                    txtkeypadno.Select(currentPosition, 0); // Move the cursor to the last position
                    return;
                }
                else
                {
                    string cardno = "AYJ1AYJ1E001" + txtkeypadno.Text;
                    if (callingForm is FrmEmployeeShort)
                    {
                        FrmEmployeeShort frmemp = (FrmEmployeeShort)callingForm;
                        frmemp.Keypad_Rfid(cardno);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page mbtnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page mbtnClose_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "7";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_7_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                txtkeypadno.Text = "";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_clear_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtkeypadno.Text))
                {
                    int currentPosition = txtkeypadno.SelectionStart;
                    txtkeypadno.Text = txtkeypadno.Text.Substring(0, txtkeypadno.Text.Length - 1);
                    txtkeypadno.SelectionStart = currentPosition - 1;
                }
                txtkeypadno.Focus();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btndel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "0";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_0_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "3";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_3_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "2";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_2_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "1";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_1_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "6";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_6_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "5";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_5_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "4";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_4_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "9";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_9_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            try
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text += "8";
                txtkeypadno.Focus();
                txtkeypadno.SelectionStart = currentPosition + 1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page btn_8_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtkeypadno.Text))
                {
                    txtkeypadno.Text = txtkeypadno.Text.Substring(0, txtkeypadno.Text.Length - 1);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page button2_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void Frmkeypad_Load(object sender, EventArgs e)
        {
            try
            {
                // Your load logic here
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page Frmkeypad_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void txtkeypadno_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page txtkeypadno_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }

        private void txtkeypadno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // Ignore the Enter key press to prevent the newline character
                    e.SuppressKeyPress = true; // This prevents the 'ding' sound when pressing Enter

                    // Perform the button click here
                    mbtnSave.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGen.LogException("Keypad Page txtkeypadno_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Keypad));
            }
        }
    }
}
