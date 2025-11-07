using FoodandBeverage.Interfaces;
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

namespace FoodandBeverage.Forms.UserControl
{
    public partial class Frmkeyboard : Form
    {
        public Frmkeyboard(int KBtype)
        {
            keyboardtype = KBtype;
            InitializeComponent();
            Instance = this;
        }
        public int keyboardtype = 0;

        // Singleton
        public static Frmkeyboard Instance { get; private set; }
        public static bool IsOpen => Instance != null && !Instance.IsDisposed;

        // Keyboard state
        private bool isDragging = false;
        private Point lastMousePos;
        private bool isUpperCase = false;
        public string EnteredText { get; private set; }

        public static int KeyTypevalue;

        // Static font reuse to avoid GDI leaks
        private static readonly Font FontLarge = new Font("Segoe UI", 14);
        private static readonly Font FontSmall = new Font("Segoe UI", 8);

        // Delegate and event
        public delegate void KeyPressedHandler(string keyText);
        public event KeyPressedHandler OnKeyPressed;

        public delegate void SpecialKeyPressedHandler(Keys key);
        public event SpecialKeyPressedHandler OnSpecialKeyPressed;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeResources();
            Instance = null;
            base.OnFormClosed(e);
        }

        public void DisposeResources()
        {
            OnKeyPressed = null;
        }

        private void Frmkeyboard_Shown(object sender, EventArgs e)
        {
            SetRoundedCorners(40);
            if (keyboardtype == Convert.ToInt32(KeyboardType.FunctionKey))
                btn_alpha_num.PerformClick();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetRoundedCorners(40);
        }

        private void SetRoundedCorners(int radius)
        {
            try
            {
                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
                using (GraphicsPath path = new GraphicsPath())
                {
                    int diameter = radius * 2;

                    path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                    path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                    path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                    path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                    path.CloseAllFigures();

                    this.Region = new Region(path);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Frmkeyboard_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (GraphicsPath path = GetRoundedPath(new Rectangle(0, 0, this.Width, this.Height), 40))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#630000"), 10))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
            catch (Exception ex)
            {
               //objGeneral.LogException("Keyboard page Frmkeyboard_Paint()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.Keyboard));
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

        private void Frmkeyboard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastMousePos = e.Location;
            }
        }

        private void Frmkeyboard_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Frmkeyboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - lastMousePos.X;
                int deltaY = e.Y - lastMousePos.Y;
                this.Location = new Point(this.Location.X + deltaX, this.Location.Y + deltaY);
            }
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.Alpha;
            if (sender is Button btn)
            {
                string key = isUpperCase ? btn.Text.ToUpper() : btn.Text.ToLower();
                EnteredText += key;
                OnKeyPressed?.Invoke(key);
            }
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_alpha_num_Click(object sender, EventArgs e)
        {
            isUpperCase = false;
            bool isAlphaVisible = tlp_alpha.Visible;

            tlp_alpha.Visible = !isAlphaVisible;
            tlp_num.Visible = isAlphaVisible;

            string oldText = btn_alpha_num.Text;
            btn_alpha_num.Text = btn_alpha_num.Tag?.ToString();
            btn_alpha_num.Tag = oldText;
        }

        private void btn_shift_Click(object sender, EventArgs e)
        {
            isUpperCase = !isUpperCase;
            btn_shift.BackgroundImage = null;
            if (isUpperCase)
            {
                btn_shift.BackgroundImage = FoodandBeverage.Properties.Resources.shift_pressed;
            }
            else
            {
                btn_shift.BackgroundImage = FoodandBeverage.Properties.Resources.shift_large;
            }
        }


        private void btn_backspace_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.backspace;
            OnKeyPressed?.Invoke("");
        }

        private void btn_space_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.space;
            OnKeyPressed?.Invoke("");
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.clear;
            OnKeyPressed?.Invoke("");
        }

        // Button animation handlers
        private void SetButtonEffect_key(Button button, bool isPressed)
        {
            try
            {
                button.BackgroundImage = isPressed
                    ? FoodandBeverage.Properties.Resources.key_large
                    : FoodandBeverage.Properties.Resources.key_smalll;

                button.Font = isPressed ? FontLarge : FontSmall;
            }
            catch (Exception ex)
            {
                //objGeneral.LogException("Keyboard page SetButtonEffect_key()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.Keyboard));
            }
        }

        private void btn_q_MouseDown(object sender, MouseEventArgs e) => SetButtonEffect_key((Button)sender, false);
        private void btn_q_MouseUp(object sender, MouseEventArgs e) => SetButtonEffect_key((Button)sender, true);
        //private void btn_shift_MouseDown(object sender, MouseEventArgs e) => SetShiftAppearance(false);
        //private void btn_shift_MouseUp(object sender, MouseEventArgs e) => SetShiftAppearance(true);
        private void btn_backspace_MouseDown(object sender, MouseEventArgs e) => SetBackspaceAppearance(false);
        private void btn_backspace_MouseUp(object sender, MouseEventArgs e) => SetBackspaceAppearance(true);
        private void btn_alpha_num_MouseDown(object sender, MouseEventArgs e) => SetAlphaNumAppearance(false);
        private void btn_alpha_num_MouseUp(object sender, MouseEventArgs e) => SetAlphaNumAppearance(true);
        private void btn_space_MouseDown(object sender, MouseEventArgs e) => SetSpaceAppearance(false);
        private void btn_space_MouseUp(object sender, MouseEventArgs e) => SetSpaceAppearance(true);
        private void btn_home_MouseDown(object sender, MouseEventArgs e) => SetHomeAppearance(false);
        private void btn_home_MouseUp(object sender, MouseEventArgs e) => SetHomeAppearance(true);
        private void btn_backspace2_MouseDown(object sender, MouseEventArgs e) => SetBackspaceAppearance(false);
        private void btn_backspace2_MouseUp(object sender, MouseEventArgs e) => SetBackspaceAppearance(true);

        // Appearance helpers
        private void SetShiftAppearance(bool large)
        {
            btn_shift.BackgroundImage = large ? FoodandBeverage.Properties.Resources.shift_large : FoodandBeverage.Properties.Resources.shift_small;
            btn_shift.Font = large ? FontLarge : FontSmall;
        }

        private void SetBackspaceAppearance(bool large)
        {
            btn_backspace.BackgroundImage = large ? FoodandBeverage.Properties.Resources.backspace_large : FoodandBeverage.Properties.Resources.backspace_small;
            btn_backspace.Font = large ? FontLarge : FontSmall;
        }

        private void SetAlphaNumAppearance(bool large)
        {
            btn_alpha_num.BackgroundImage = large ? FoodandBeverage.Properties.Resources._123_BUTTON_large : FoodandBeverage.Properties.Resources._123_BUTTON_small;
            btn_alpha_num.Font = large ? FontLarge : FontSmall;
        }

        private void SetSpaceAppearance(bool large)
        {
            btn_space.BackgroundImage = large ? FoodandBeverage.Properties.Resources.space_large : FoodandBeverage.Properties.Resources.space_small;
            btn_space.Font = large ? FontLarge : FontSmall;
        }

        private void SetHomeAppearance(bool large)
        {
            //btn_home.BackgroundImage = large ? GamePOS.Properties.Resources.home_large : GamePOS.Properties.Resources.home_small;
            //btn_home.Font = large ? FontLarge : FontSmall;
        }

        private void btn_f5_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.keyf5;
            OnKeyPressed?.Invoke("");
            this.Close();
        }

        private void btn_f8_Click(object sender, EventArgs e)
        {
            KeyTypevalue = (int)KeyType.keyf8;
            OnKeyPressed?.Invoke("");
            this.Close();
        }

        private void btn_clear_MouseDown(object sender, MouseEventArgs e)
        {
            btn_clear.BackgroundImage = FoodandBeverage.Properties.Resources.clear_small;
            btn_clear.Font =  FontSmall;
        }

        private void btn_clear_MouseUp(object sender, MouseEventArgs e)
        {
            btn_clear.BackgroundImage = FoodandBeverage.Properties.Resources.CLEAR_large;
            btn_clear.Font =  FontLarge;
        }

        private void Frmkeyboard_Load(object sender, EventArgs e)
        {

        }

        private void Frmkeyboard_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
