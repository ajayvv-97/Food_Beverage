using CustomMessageBox;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms.UserControl
{
    public partial class Frmkeypad : Form
    {
        public Frmkeypad()
        {
            InitializeComponent();
        }

        #region Decalartion
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private bool isDragging = false;
        private Point lastMousePos;

        private Form callingForm;

        public int typeRfid;
        ClsGeneral objGeneral = new ClsGeneral();

        public string ICardNo = "";
        public int seletionlength_No = 0;

        private static readonly Font smallFont = new Font(SystemFonts.DefaultFont.FontFamily, 10);
        private static readonly Font largeFont = new Font(SystemFonts.DefaultFont.FontFamily, 20);

        #endregion

        public Frmkeypad(Form callingForm, int type)
        {
            InitializeComponent();
            this.callingForm = callingForm;
            typeRfid = type;
        }
        private void Frmkeypad_Load(object sender, EventArgs e)
        {
            try
            {
                textb_shape();
                this.BeginInvoke((Action)(() =>
                {
                    txtkeypadno.Text = "00-0000-0000";
                    txtkeypadno.Focus();
                    txtkeypadno.SelectionStart = txtkeypadno.Text.Length;
                }));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page Frmkeypad_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        public void textb_shape()
        {
            try
            {
                txtkeypadno.Font = new Font(txtkeypadno.Font.FontFamily, 22);
                ApplyRoundedTextBox(txtkeypadno, 10);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                // Create a new GraphicsPath object
                GraphicsPath path = new GraphicsPath();
                // Define the shape of the path using a rounded rectangle
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        public void ReplaceDigit(int valu)
        {
            try
            {
                if(seletionlength_No>0)
                {
                    int selectionLength = txtkeypadno.SelectionLength;
                    char[] textArray = txtkeypadno.Text.ToCharArray();
                    int selectionStart = txtkeypadno.SelectionStart;
                    string selectedText = txtkeypadno.Text.Substring(selectionStart, txtkeypadno.SelectionLength);

                    #region Press numbers on keyboard (SELECTED DIGIT)
                    int replaceIndex = selectionStart + selectionLength - 1;
                    for (int i = replaceIndex; i >= selectionStart; i--)
                    {
                        if (char.IsDigit(textArray[i]))
                        {
                            textArray[i] = Convert.ToChar(valu);
                            break;
                        }
                    }
                    txtkeypadno.Text = new string(textArray);
                    txtkeypadno.SelectionStart = selectionStart;
                    txtkeypadno.SelectionLength = 0;
                    return;
                    #endregion
                }

            }
            catch (Exception ex)
            {
                objGeneral.LogException("Keypad page ReplaceDigit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));
            }

        }


        private void btn_7_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "7";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                seletionlength_No = txtkeypadno.SelectionLength;
                ReplaceDigit(7);
                string input = txtkeypadno.Text + "7";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_7_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_8_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtkeypadno.Text + "8";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
                //int currentPosition = txtkeypadno.SelectionStart;
                //string newInput = input.Substring(0, input.Length - 1) + "8";
                ////txtkeypadno.Text += "8";
                //txtkeypadno.Text = newInput;
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_8_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_9_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "9";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "9";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_9_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_4_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "4";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "4";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_4_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_5_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "5";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "5";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_5_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_6_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "6";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "6";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_6_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_1_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "1";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "1";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_1_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_2_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "2";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "2";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_2_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_3_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "3";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "3";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_3_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_0_Click(object sender, EventArgs e)
        {
            try
            {
                //int currentPosition = txtkeypadno.SelectionStart;
                //txtkeypadno.Text += "0";
                //txtkeypadno.Focus();
                //txtkeypadno.SelectionStart = currentPosition + 1;
                string input = txtkeypadno.Text + "0";
                int position = GetFirstNonZeroPosition(input);
                input = input.Remove(0, position).Replace("-", "").Trim();
                input = input.PadLeft(10, '0');
                input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                txtkeypadno.Text = input;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_0_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                txtkeypadno.Text = "00-0000-0000";
                txtkeypadno.SelectionStart = txtkeypadno.Text.Length; 
                txtkeypadno.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_clear_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }



        private void btndel_Click(object sender, EventArgs e)
        {
            try
            {            
                if (!string.IsNullOrEmpty(txtkeypadno.Text))
                {
                    int currentPosition = txtkeypadno.SelectionStart - 1;
                    char[] textArray = txtkeypadno.Text.ToCharArray();
                    if (currentPosition >= 0 && char.IsDigit(textArray[currentPosition]))
                    {
                        textArray[currentPosition] = '0'; 
                    }
                    txtkeypadno.Text = new string(textArray);
                    txtkeypadno.SelectionStart = Math.Max(currentPosition, 0);
                    txtkeypadno.Focus();
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btndel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));
            }
        }
        private void btn_closekeypad_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_closekeypad_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Remove all whitespace characters from the TextBox text
                string cardNumber = new string(txtkeypadno.Text.Where(c => !char.IsWhiteSpace(c)).ToArray());
                cardNumber = cardNumber.Replace("-", "").Trim();

                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    // Show an error message to the user
                    CMessageBox.Show("Please Enter Rfid Card No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtkeypadno.Focus();
                    return;
                }
                else if (cardNumber.Length != 10)
                {
                    int currentPosition = txtkeypadno.SelectionStart;
                    // Show an error message to the user
                    CMessageBox.Show("Rfid Card No must contain exactly 10 Digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtkeypadno.Focus();
                    txtkeypadno.Select(currentPosition, 0); // Move the cursor to the last position
                    return;
                }
                else
                {
                    if (cardNumber == "0000000000")
                    {
                        CMessageBox.Show("Please enter a valid RFID card number.", "Invalid RFID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtkeypadno.Focus();
                        return;
                    }
                    ICardNo = FrmLogin.CorpID + cardNumber;
                    //if (callingForm is FrmFamilyCard)
                    //{
                    //    FrmFamilyCard frmFamilyCard = (FrmFamilyCard)callingForm;
                    //    if (typeRfid == 1)
                    //        frmFamilyCard.MasterCard(ICardNo);
                    //    else if (typeRfid == 2)
                    //        frmFamilyCard.Childcard(ICardNo);
                    //}
                    //else if (callingForm is FrmGroupSale)
                    //{
                    //    FrmGroupSale frmgroup = (FrmGroupSale)callingForm;
                    //    frmgroup.Keypad_Rfid(ICardNo);
                    //}
                    //else if (callingForm is FrmRecharge)
                    //{
                    //    FrmRecharge frmrech = (FrmRecharge)callingForm;
                    //    frmrech.Keypad_Rfid(ICardNo);
                    //}
                    //else if (callingForm is FrmProfile)
                    //{
                    //    FrmProfile frmrpro = (FrmProfile)callingForm;
                    //    frmrpro.Keypad_Rfid(ICardNo);
                    //}
                    //else if (callingForm is FrmCustAccount)
                    //{
                    //    FrmCustAccount frmcus = (FrmCustAccount)callingForm;
                    //    frmcus.Keypad_Rfid(ICardNo);
                    //}
                    //else if (callingForm is FrmTMpanel)
                    //{
                    //    FrmTMpanel frmtm = (FrmTMpanel)callingForm;
                    //    frmtm.txtSearchBox.Text = ICardNo;
                    //    frmtm.FillData_Filter();
                    //}
                    //else if (callingForm is FoodandBeverage.Forms.UserControl.Frmdashboardcusaccount)
                    //{
                    //    FoodandBeverage.Forms.UserControl.Frmdashboardcusaccount frmdashcus = (FoodandBeverage.Forms.UserControl.Frmdashboardcusaccount)callingForm;
                    //    frmdashcus.keypad_Rfid_cus(ICardNo);
                    //}
                }
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btnsubmit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

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
                    btnsubmit.PerformClick();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page txtkeypadno_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void Frmkeypad_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = true;
                    lastMousePos = e.Location;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page Frmkeypad_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void Frmkeypad_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isDragging)
                {
                    int deltaX = e.X - lastMousePos.X;
                    int deltaY = e.Y - lastMousePos.Y;
                    this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page Frmkeypad_MouseMove()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void Frmkeypad_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                isDragging = false;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page Frmkeypad_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void txtkeypadno_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
                seletionlength_No = txtkeypadno.SelectionLength;
                char[] textArray = txtkeypadno.Text.ToCharArray();
                if (seletionlength_No > 0)
                {
                    int selectionStart = txtkeypadno.SelectionStart;
                    string selectedText = txtkeypadno.Text.Substring(selectionStart, txtkeypadno.SelectionLength);

                    #region Press backspace key on keyboard (SELECTED DIGIT)
                    int selectionLength = txtkeypadno.SelectionLength;
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        // Loop through the selected characters from RIGHT to LEFT
                        for (int i = selectionStart + selectionLength - 1; i >= selectionStart; i--)
                        {
                            if (char.IsDigit(textArray[i]))  // Ensure it's a digit before replacing
                            {
                                textArray[i] = '0';
                            }
                        }

                        txtkeypadno.Text = new string(textArray);
                        txtkeypadno.SelectionStart = selectionStart; // Keep cursor position
                        txtkeypadno.SelectionLength = 0; // Remove selection after replacing

                        e.Handled = true; 
                        return;
                    }
                    #endregion

                    #region Press numbers on keyboard (SELECTED DIGIT)
                    int replaceIndex = selectionStart + selectionLength - 1;                  
                    for (int i = replaceIndex; i >= selectionStart; i--)
                    {
                        if (char.IsDigit(textArray[i]))  
                        {
                            textArray[i] = e.KeyChar; 
                            break; 
                        }
                    }
                    txtkeypadno.Text = new string(textArray);
                    txtkeypadno.SelectionStart = selectionStart; 
                    txtkeypadno.SelectionLength = 0; 
                    e.Handled = true; 
                    return;
                    #endregion
                }               
            
                else
                {
                    #region Press backspace key on keyboard
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        if (!string.IsNullOrEmpty(txtkeypadno.Text))
                        {
                            int currentPosition = txtkeypadno.SelectionStart - 1;
                            textArray = txtkeypadno.Text.ToCharArray();
                            if (currentPosition >= 0 && char.IsDigit(textArray[currentPosition]))
                            {
                                textArray[currentPosition] = '0';
                            }
                            txtkeypadno.Text = new string(textArray);
                            txtkeypadno.SelectionStart = Math.Max(currentPosition, 0);
                            //txtkeypadno.Focus();
                        }
                    }
                    #endregion

                    #region Press numbers on keyboard
                    if (char.IsDigit(e.KeyChar))
                    {
                        int caretPosition = txtkeypadno.SelectionStart;
                        string input = txtkeypadno.Text.Replace("-", "").Trim();
                        if (!char.IsControl(e.KeyChar))
                        {
                            input += e.KeyChar;
                        }
                        input = input.PadLeft(10, '0');
                        if (input.Length > 10)
                        {
                            input = input.Substring(input.Length - 10);
                        }
                        input = $"{input.Substring(0, 2)}-{input.Substring(2, 4)}-{input.Substring(6, 4)}";
                        txtkeypadno.Text = input;
                        txtkeypadno.SelectionStart = Math.Min(caretPosition + 1, txtkeypadno.Text.Length);
                        e.Handled = true;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Keypad page txtkeypadno_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));
            }
        }
        private void btn_7_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_7.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_7.Font = new Font("", 20);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_7_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_7_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_7.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_7.Font = new Font("", 10);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_7_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_8_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_8.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_8.Font = new Font("", 10);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_8_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_8_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_8.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_8.Font = new Font("", 20);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_8_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }




        private void btn_9_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_9.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_9.Font = smallFont;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_9_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_9_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_9.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_9.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_9_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_4_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_4.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_4.Font = smallFont; 
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_4_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_4_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_4.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_4.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_4_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }

        private void btn_5_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_5.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_5.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_5_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_5_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_5.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_5.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_5_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_6_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_6.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_6.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_6_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_6_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_6.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_6.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_6_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_1.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_1.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_1_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_1.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_1.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_1_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_2.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_2.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_2_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_2_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_2.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_2.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_2_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_3_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_3.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_3.Font =smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_3_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_3_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_3.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_3.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_3_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_0_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_0.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_0.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_0_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_0_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_0.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_0.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_0_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_clear_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btn_clear.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btn_clear.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_clear_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btn_clear_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btn_clear.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btn_clear.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btn_clear_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btndel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                btndel.BackgroundImage = FoodandBeverage.Properties.Resources.blank_1;
                btndel.Font = smallFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btndel_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));

            }
        }
        private void btndel_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                btndel.BackgroundImage = FoodandBeverage.Properties.Resources.blank;
                btndel.Font = largeFont;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Keypad page btndel_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Keypad));
            }
        }
        
        
        public int GetFirstNonZeroPosition(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                // Check if the character is a digit and not zero
                if (char.IsDigit(input[i]) && input[i] != '0')
                {
                    return i; // Return the position of the first non-zero digit
                }
            }
            return -1; // Return -1 if no non-zero digit is found
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    btnsubmit.PerformClick();
                    return true;  

                case Keys.Escape:
                    btn_closekeypad.PerformClick();
                    return true;  
            }
            return base.ProcessCmdKey(ref msg, keyData);  
        }
        private void Frmkeypad_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
