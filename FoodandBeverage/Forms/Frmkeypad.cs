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
        public string ICardNo { get; set; }

        private Form callingForm;
        public int typeRfid;
        public Frmkeypad(Form callingForm, int type)
        {
            InitializeComponent();
            this.callingForm = callingForm;
            typeRfid = type;
        }

        //public Frmkeypad(FrmEmployeeShort frmEmployeeShort, int v)
        //{ 
        //}

        private void mbtnSave_Click(object sender, EventArgs e)
        {
            string cardNumber = new string(txtkeypadno.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                // Show an error message to the user
                MessageBox.Show("Please Enter Rfid Card No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtkeypadno.Focus();
                return;
            }
            else if (cardNumber.Length != 16)
            {
                int currentPosition = txtkeypadno.SelectionStart;
                // Show an error message to the user
                MessageBox.Show("Rfid Card No must contain exactly 25 Digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "7";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtkeypadno.Text = "";
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtkeypadno.Text))
            {
                int currentPosition = txtkeypadno.SelectionStart;
                txtkeypadno.Text = txtkeypadno.Text.Substring(0, txtkeypadno.Text.Length - 1);
                txtkeypadno.SelectionStart = currentPosition - 1;
            }
            txtkeypadno.Focus();
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "0";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "3";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "2";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "1";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "6";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "5";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "4";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "9";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            int currentPosition = txtkeypadno.SelectionStart;
            txtkeypadno.Text += "8";
            txtkeypadno.Focus();
            txtkeypadno.SelectionStart = currentPosition + 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtkeypadno.Text != null && txtkeypadno.Text != "")
            {
                txtkeypadno.Text = txtkeypadno.Text.Substring(0, txtkeypadno.Text.Length - 1);
            }
        }

        private void Frmkeypad_Load(object sender, EventArgs e)
        {

        }

        private void txtkeypadno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkeypadno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Ignore the Enter key press to prevent the newline character
                e.SuppressKeyPress = true; // This prevents the 'ding' sound when pressing Enter

                // Perform the button click here
                mbtnSave.PerformClick();
            }
        }
    }
}
