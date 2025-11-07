using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;

namespace FoodandBeverage.Forms.UserControl
{
    public partial class Frmnumdiskeypad : Form
    {
        int Ktype = 0;

        public Frmnumdiskeypad(int type)
        {
            Ktype = type;
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += Frmnumdiskeypad_Shown;
        }

        #region Declaration
        private string inputDigits = "";
        private bool isDecimalMode = false;
        private int decimalDigitCount = 0;

        private bool isDragging = false;
        private Point lastMousePos;

        public static bool flagpercentage = false;
        public  bool Resetflag = true;
        #endregion

        private void Frmnumdiskeypad_Shown(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (Ktype == Convert.ToInt32(KeypadType.None))
                {
                    tlp_reason.Visible = false;
                    btnclear.Dock = DockStyle.None;
                    btnper.Dock = DockStyle.None;
                    btnper.Visible = false;
                }
                else
                {
                    btnclear.Dock = DockStyle.Fill;
                    btnper.Dock = DockStyle.Fill;
                    tlp_reason.Visible = true;
                    btnper.Visible = true;
                }

                SetRoundedCorners(40);
                ApplyRoundedTextBox(txtresult, 20);
                ApplyRoundedTextBox(txtdesc, 20);
                ApplyRoundedCorners(pnl_description,20);

                this.BackColor = ColorTranslator.FromHtml("#e4e4e4");
                txtresult.TextAlign = HorizontalAlignment.Right;
                txtresult.Text = "0";
                cmbreason.SelectedIndex = 0;
                txtdesc.ForeColor = Color.LightGray;

                txtdesc.Text="Enter Remarks Here.....";

            }));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetRoundedCorners(40);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                GraphicsPath path = new GraphicsPath();
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }    

        }

        private void btnother_Click(object sender, EventArgs e)
        {
            try
            {
                string textValue = txtresult.Text.Replace("%", "").Trim();

                if (decimal.TryParse(textValue, out decimal number))
                {
                    if (number > 100)
                    {
                        MessageBox.Show("Percentage cannot be more than 100.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!txtresult.Text.EndsWith("%"))
                    {
                        txtresult.Text = number.ToString("0.##") + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page btnother_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                inputDigits = "";
                isDecimalMode = false;
                decimalDigitCount = 0;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page btnclear_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }

        }
       

    private void KeypadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    string digit = btn.Text;

                    if (!digit.All(char.IsDigit))
                        return;

                    if (inputDigits.Length >= 13)
                        return;

                    if (isDecimalMode)
                    {
                        if (decimalDigitCount < 2)
                        {
                            inputDigits += digit;
                            decimalDigitCount++;
                        }
                    }
                    else
                    {
                        inputDigits += digit;
                    }

                    UpdateDisplay();
                }
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page KeypadButton_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }       

        }

        private void UpdateDisplay()
        {
            try
            {
                if (string.IsNullOrEmpty(inputDigits))
                {
                    txtresult.Text = "0";
                    return;
                }

                if (isDecimalMode)
                {
                    int totalLen = inputDigits.Length;
                    int wholeLen = totalLen - decimalDigitCount;

                    string wholePart = wholeLen > 0 ? inputDigits.Substring(0, wholeLen) : "0";
                    string decimalPart = inputDigits.Substring(wholeLen);

                    txtresult.Text = $"{wholePart}.{decimalPart}";
                }
                else
                {
                    txtresult.Text = inputDigits;
                }
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page UpdateDisplay()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }        
        
        }

      

        private void AddFixedAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    if (decimal.TryParse(btn.Text, out decimal addValue))
                    {
                        decimal currentValue = 0;
                        decimal.TryParse(txtresult.Text, out currentValue);
                        decimal newValue = currentValue + addValue;
                        txtresult.Text = newValue.ToString("0.##");
                        inputDigits = newValue.ToString("0").Replace(".", "");
                        isDecimalMode = false;
                        decimalDigitCount = 0;
                    }
                }
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page AddFixedAmount_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void PercentageButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    string percentText = btn.Text;
                    if (!percentText.EndsWith("%"))
                        return;
                    txtresult.Text = percentText;
                    inputDigits = "";
                    isDecimalMode = false;
                    decimalDigitCount = 0;
                }
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page PercentageButton_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }           
        }

        private void SetRoundedCorners(int radius)
        {
            try
            {
                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
                GraphicsPath path = new GraphicsPath();
                int diameter = radius * 2;

                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page SetRoundedCorners()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }   
        }

        private void btnokay_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtresult.Text.Contains('%'))
                {
                    flagpercentage = true;
                }
                else
                    flagpercentage = false;

                string resultText = txtresult.Text.Replace("%", "").Trim();
                txtkeypadvalue.Text = resultText;
                this.Close();
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("NumberPercentageKeypad page btnokay_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }      
        }

        private void Frmnumdiskeypad_Paint(object sender, PaintEventArgs e)
        {
            
            try
            {
                int radius = 40;
                int borderWidth = 6; // reduced from 10 to improve performance
                Color borderColor = ColorTranslator.FromHtml("#630000");

                using (GraphicsPath path = GetRoundedPath(new Rectangle(0, 0, this.Width, this.Height), radius))
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page Frmnumdiskeypad_Paint()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
          
        }

        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private void Frmnumdiskeypad_MouseDown(object sender, MouseEventArgs e)
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
              //  objGeneral.LogException("NumberPercentageKeypad page Frmnumdiskeypad_MouseDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void Frmnumdiskeypad_MouseMove(object sender, MouseEventArgs e)
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
              //  objGeneral.LogException("NumberPercentageKeypad page Frmnumdiskeypad_MouseMove()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void Frmnumdiskeypad_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                isDragging = false;
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page Frmnumdiskeypad_MouseUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void SetButtonEffect_num(Button button, bool isPressed)
        {
            try
            {
                if (isPressed)
                {
                    button.BackgroundImage = FoodandBeverage.Properties.Resources.keypad_large;
                    button.Font = new Font(button.Font.FontFamily, 14); 
                }
                else
                {
                    button.BackgroundImage = FoodandBeverage.Properties.Resources.keypad_small;
                    button.Font = new Font(button.Font.FontFamily, 8); 
                }
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page SetButtonEffect_num()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }

        private void SetButtonEffect_fdnum(Button button, bool isPressed)
        {
            try
            {
                if (isPressed)
                {
                    button.BackgroundImage = FoodandBeverage.Properties.Resources.fdnumber_large;
                    button.Font = new Font(button.Font.FontFamily, 14); 
                }
                else
                {
                    button.BackgroundImage = FoodandBeverage.Properties.Resources.fdnumber_small;
                    button.Font = new Font(button.Font.FontFamily, 8); 
                }
            }
            catch (Exception ex)
            {
               // objGeneral.LogException("NumberPercentageKeypad page SetButtonEffect_fdnum()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
        }


        private void btn7_MouseDown(object sender, MouseEventArgs e)
        {
            SetButtonEffect_num((Button)sender, false);
        }

        private void btn7_MouseUp(object sender, MouseEventArgs e)
        {
            SetButtonEffect_num((Button)sender, true);
        }

        private void btn10_MouseDown(object sender, MouseEventArgs e)
        {
            SetButtonEffect_fdnum((Button)sender, false);
        }

        private void btn10_MouseUp(object sender, MouseEventArgs e)
        {
            SetButtonEffect_fdnum((Button)sender, true);
        }

        private void btndecimal_MouseDown(object sender, MouseEventArgs e)
        {
            btndecimal.BackgroundImage = FoodandBeverage.Properties.Resources.keypad_small;
            btndecimal.Font = new Font(btndecimal.Font.FontFamily, 26);
        }

        private void btndecimal_MouseUp(object sender, MouseEventArgs e)
        {
            btndecimal.BackgroundImage = FoodandBeverage.Properties.Resources.keypad_large;
            btndecimal.Font = new Font(btndecimal.Font.FontFamily, 26);
        }

        private void cmbreason_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbreason.SelectedItem.ToString() == "Other")
            {
                tlp_main.Enabled = false;
                pnl_description.Visible = true;
                CenterPanelInForm(); 
            }
            else
            {
                pnl_description.Visible = false;
            }
        }

        private void CenterPanelInForm()
        {
            int x = (this.ClientSize.Width - pnl_description.Width) / 2;
            int y = (this.ClientSize.Height - pnl_description.Height) / 2;
            pnl_description.Location = new Point(x, y);
        }


        public void ApplyRoundedCorners(Control control, float borderRadius)
        {
            if (control == null) return;

            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(control.ClientRectangle.X, control.ClientRectangle.Y, control.ClientRectangle.Width, control.ClientRectangle.Height);

            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();

            control.Region = new Region(path);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Resetflag = false;
                tlp_main.Enabled = true;
                pnl_description.Visible = false;
                cmbreason.SelectedIndex = 0;
            }
            catch( Exception ex)
            {

            }
            finally
            {
                Resetflag = true;
            }

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                Resetflag = false;
                tlp_main.Enabled = true;
                pnl_description.Visible = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Resetflag = true;
            }

        }

        private void txtresult_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btndecimal_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isDecimalMode)
                {
                    isDecimalMode = true;
                    decimalDigitCount = 0;
                    if (string.IsNullOrEmpty(inputDigits))
                    {
                        inputDigits = "0";
                    }

                    UpdateDisplay();
                }
            }
            catch (Exception ex)
            {
              //  objGeneral.LogException("NumberPercentageKeypad page btndecimal_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.NumberPercentageKeypad));
            }
           
        }



        #region Keyboard
        public void ShowKeyboard(TextBox activeTextBox)
        {
            if (activeTextBox == null) return;

            Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
            int desiredX = 0, desiredY = 0;
            if (Frmkeyboard.IsOpen)
            {
                Frmkeyboard.Instance.Close();
            }


            Frmkeyboard frmKey = new Frmkeyboard(0);

            #region Calculate Position of Keyboard 
            desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
            desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
            Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
            if (desiredX < screenBounds.Left)
                desiredX = screenBounds.Left;
            else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
            if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
            {
                desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                if (desiredY < screenBounds.Top)
                    desiredY = screenBounds.Top;
            }
            Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
            Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
            #endregion


            frmKey.OnKeyPressed += (keyText) =>
            {
                if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.backspace))
                {
                    if (activeTextBox.Text.Length > 0)
                        activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                }
                else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.space))
                {
                    activeTextBox.Text += " ";
                }
                else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.clear))
                {
                    activeTextBox.Text = "";
                }
                else
                {
                    activeTextBox.Text += keyText;
                }
                activeTextBox.SelectionStart = activeTextBox.Text.Length;
                activeTextBox.SelectionLength = 0;
            };
            frmKey.Show();
        }

        #endregion

        private void txtdesc_Click(object sender, EventArgs e)
        {

        }

        private void txtdesc_Enter(object sender, EventArgs e)
        {
            ApplyPlaceholder(txtdesc, "Enter Remarks Here.....", 1);
        }

        private void txtdesc_Leave(object sender, EventArgs e)
        {
            ApplyPlaceholder(txtdesc, "Enter Remarks Here.....", 0);
        }
        private void ApplyPlaceholder(TextBox textbox, string placeholder, int type)
        {
            if (type == 1)
            {
                if (textbox.Text == placeholder)
                {
                    textbox.Text = "";
                    textbox.ForeColor = Color.Black;
                }
                else
                {
                    textbox.ForeColor = Color.Black;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    textbox.Text = placeholder;
                    textbox.ForeColor = Color.LightGray;
                }
                else if (textbox.Text != placeholder)
                {
                    textbox.ForeColor = Color.Black;
                }
            }
        }

        private void Frmnumdiskeypad_Load(object sender, EventArgs e)
        {

        }

        private void Frmnumdiskeypad_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
