using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;

namespace FoodandBeverage.Forms
{
    public partial class FrmPickup : Form
    {
        #region Declaration 

        public int panelsPerRow = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public int Searchmode = 0;
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        ClsGeneral clsgen = new ClsGeneral();

        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
        DmnSearch dmnSearch = new DmnSearch();

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Green;
        public Color Netamountfontcolor = Color.Green;

        #endregion
        public FrmPickup(int type)
        {
            InitializeComponent();
            Searchmode = 0;
        }

        private void btncolse_Click(object sender, System.EventArgs e)
        {
            //refreshtimer.Stop();
            this.Close();
            this.Dispose();
        }

        private void FrmPickup_Load(object sender, System.EventArgs e)
        {
            LoadPickupView(Searchmode);
            textb_shape();
            txtPkid_search.Text = "Token No ....";
            txtPkid_search.ForeColor = Color.DarkGray;
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtPkid_search })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
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
                clsgen.LogException("TableView page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }


        #endregion

        private void btnaddnew_Click(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                {
                    FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                    if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                    {
                        _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        _instanceFrmOrder.ShowDialog();
                        _instanceFrmOrder.BringToFront();
                    }
                }
                else
                {
                    FrmOrder _instanceFrmOrder = new FrmOrder(0, Convert.ToInt32(DashboardTableType.PickupOrder), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage), 0, Convert.ToInt32(SourcepageType.Pickuppage));
                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    _instanceFrmOrder.Opacity = 0;
                    _instanceFrmOrder.Visible = false;
                    _instanceFrmOrder.ShowDialog();
                    _instanceFrmOrder.BringToFront();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("TableView page  Button_pickupClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        public void LoadPickupView(int serachmode)
        {
            try
            {
                BllPickupview<DmnGridFields> SearchDetails = new BllPickupview<DmnGridFields>();
                DmnGridFields dmnSearchpickdata = new DmnGridFields();
                dmnSearchpickdata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                switch (serachmode)
                {
                    case 0:
                        dmnSearchpickdata.Field29 = "0"; 
                        break;
                    case 1:
                        dmnSearchpickdata.Field29 = "1";
                        break;
                    case 3:
                        dmnSearchpickdata.Field29 = "3";
                        break;
                }
                var gridSource = SearchDetails.SerachData("Prd_Pickupview", dmnSearchpickdata);
                if (gridSource.Count > 0)
                {
                    #region Remove all expect addnew tablelayoutpanel Pickup on complete or cancel
                    string addbuttonname = "tlp_addnew";
                    foreach (Control control in flp_pickup_view.Controls.Cast<TableLayoutPanel>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && tlp.Name != addbuttonname)
                        {
                            flp_pickup_view.Controls.Remove(control);
                            control.Dispose();
                        }
                    }
                    #endregion

                    flp_pickup_view.SuspendLayout();

                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        var item = gridSource[i];

                        #region Pickup box Fetching Orders  

                        #region  TableLayoutPanel inner pickup
                        // Configure the tlp_table_inner_pkup
                        TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                        tlp_table_inner_pkup_fetch.RowCount = 2;
                        tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + item.Field3;
                        tlp_table_inner_pkup_fetch.Tag = item.Field3;
                        tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                        tlp_table_inner_pkup_fetch.Dock = DockStyle.Top;
                        tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                        tlp_table_inner_pkup_fetch.AccessibleDescription = item.Field1;
                        tlp_table_inner_pkup_fetch.Visible = false;
                        //tlp_table_inner_pkup_fetch.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                        #endregion

                        #region  label name  inside tablelayoutpanel inner
                        // Configure the Label
                        Label lbltablename_pkup_fetch = new Label();
                        lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + item.Field3;
                        lbltablename_pkup_fetch.Tag = item.Field3;
                        lbltablename_pkup_fetch.Text = "Pick Up \n " + item.Field6;
                        lbltablename_pkup_fetch.ForeColor = Color.Black;
                        lbltablename_pkup_fetch.BackColor = Color.Transparent;
                        lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                        lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                        lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                        lbltablename_pkup_fetch.TextAlign = ContentAlignment.TopCenter;
                        lbltablename_pkup_fetch.AccessibleDescription = item.Field1;
                        #endregion

                        #region Net Amount
                        // Configure the Label
                        Label netamountlabel_pkup_fetch = new Label();
                        netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + item.Field3;
                        netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                        netamountlabel_pkup_fetch.Tag = item.Field3;
                        netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                        netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                        netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                        netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                        netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                        netamountlabel_pkup_fetch.Visible = false;
                        netamountlabel_pkup_fetch.AccessibleDescription = item.Field1;
                        #endregion

                        #region Netamount Show and hide order complete
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        dmnSearch.Select = "COUNT(*) Field1";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field3;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                        if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                        {
                            netamountlabel_pkup_fetch.Visible = true;
                            tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 60);
                            tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                            netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                            lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                            netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                            lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                        }
                        else
                        {
                            netamountlabel_pkup_fetch.Visible = false;
                            tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                            tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                            lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                            lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pkup_fetch.Text = "";
                        }
                        #endregion

                        #region Size setting Pickup Box
                        if (screenWidth > 1366)
                        {
                            tlp_table_inner_pkup_fetch.Width = 300;
                            tlp_table_inner_pkup_fetch.Height = 100;
                            tlp_addnew.Width = 300;
                            tlp_addnew.Height = 100;
                        }
                        else if (screenWidth >= 1024 && screenWidth <= 1366)
                        {
                            tlp_table_inner_pkup_fetch.Width = 120;
                            tlp_table_inner_pkup_fetch.Height = 80;

                            tlp_addnew.Width = 120;
                            tlp_addnew.Height = 80;
                            
                        }

                        if (screenWidth > 1366)
                        {
                            panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                        }
                        else if (screenWidth >= 1024 && screenWidth <= 1366)
                        {
                            panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                        }
                        #endregion

                        tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                        tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                        lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                        netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                        tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                        flp_pickup_view.Controls.Add(tlp_table_inner_pkup_fetch);
                        tlp_table_inner_pkup_fetch.Visible = true;

                        #endregion
                    }
                    flp_pickup_view.ResumeLayout();

                    AutoTimer.Start();
                    int totalcount = flp_pickup_view.Controls.OfType<TableLayoutPanel>().Count();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Pickuppage page  LoadPickupView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }
        private void PickupboxClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                AutoTimer.Stop();
                Control clickedControl = sender as Control;
                int pkid = Convert.ToInt32(clickedControl.AccessibleDescription);
                if (clickedControl != null)
                {
                    if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                    {
                        FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                        if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                        {
                            _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            _instanceFrmOrder.ShowDialog();
                            _instanceFrmOrder.BringToFront();
                        }
                    }
                    else
                    {
                        if (clickedControl.Tag != null)
                        {
                            FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.PickupOrder), 0, CheckedtableNames, Convert.ToInt32(PageType.Billpage), Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(SourcepageType.Pickuppage));
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.ShowDialog();
                            _instanceFrmOrder.BringToFront();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Pickuppage page  PickupboxClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            UpdatePickupView(Searchmode);
        }
        public void UpdatePickupView(int serachmode)
        {
            try
            {
                AutoTimer.Stop();
                BllPickupview<DmnGridFields> SearchDetails = new BllPickupview<DmnGridFields>();
                DmnGridFields dmnSearchpickdata = new DmnGridFields();
                dmnSearchpickdata.Field30 = Convert.ToString(FrmLogin.BookingInterwell);
                switch (serachmode)
                {
                    case 0:
                        dmnSearchpickdata.Field29 = "0";
                        break;
                    case 1:
                        dmnSearchpickdata.Field29 = "1";
                        break;
                    case 3:
                        dmnSearchpickdata.Field29 = "3";
                        break;
                }
                var gridSource = SearchDetails.SerachData("Prd_Pickupview", dmnSearchpickdata);

                if (gridSource.Count == 0 )
                {
                    #region Remove all expect addnew tablelayoutpanel Pickup on complete or cancel
                    string addbuttonname = "tlp_addnew";
                    foreach (Control control in flp_pickup_view.Controls.Cast<TableLayoutPanel>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && tlp.Name != addbuttonname)
                        {
                            flp_pickup_view.Controls.Remove(control);
                            control.Dispose();
                        }
                    }
                    #endregion
                }
                else if (gridSource.Count > 0)
                {
                    #region Remove Pickup on complete or cancel Orders
                    string addbuttonname = "tlp_addnew";
                    var preserveNames = gridSource.Select(orders => "tlpboxpkup_add_" + orders.Field3);
                    foreach (Control control in flp_pickup_view.Controls.Cast<TableLayoutPanel>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && !preserveNames.Contains(tlp.Name) && tlp.Name != addbuttonname)
                        {
                            flp_pickup_view.Controls.Remove(control);
                            control.Dispose();
                        }
                    }
                    #endregion

                    foreach (var item in gridSource)
                    {
                        #region Pickup Order

                        string tlpboxname = "tlpboxpkup_add_" + item.Field3;
                        string lbtable = "lbtablename_pkup_" + item.Field3;
                        string lblnetamountname = "lb_netamount_pkup_" + item.Field3;

                        TableLayoutPanel tlp_table_pbox = this.Controls.Find(tlpboxname, true).FirstOrDefault() as TableLayoutPanel;
                        Label lbltablename_p = this.Controls.Find(lbtable, true).FirstOrDefault() as Label;
                        Label netamountlabel_p = this.Controls.Find(lblnetamountname, true).FirstOrDefault() as Label;
                        if (tlp_table_pbox != null)//update 
                        {
                            #region Netamount Show and hide order complete
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field3;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                            {
                                netamountlabel_p.Visible = true;
                                tlp_table_pbox.RowStyles[0] = new RowStyle(SizeType.Percent, 60);
                                tlp_table_pbox.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                                netamountlabel_p.Dock = DockStyle.Fill;
                                lbltablename_p.Dock = DockStyle.Fill;
                                netamountlabel_p.Text = "\u20B9" + Convert.ToString(item.Field4);
                                lbltablename_p.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_p.TextAlign = ContentAlignment.MiddleCenter;
                            }
                            else
                            {
                                netamountlabel_p.Visible = false;
                                tlp_table_pbox.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                tlp_table_pbox.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                lbltablename_p.Dock = DockStyle.Fill;
                                lbltablename_p.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_p.Text = "";
                            }
                            #endregion
                        }
                        else // insert new 
                        {
                            flp_pickup_view.SuspendLayout();
                            #region Pickup box Fetching Orders  db

                            #region  TableLayoutPanel inner pickup
                            // Configure the tlp_table_inner_pkup
                            TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                            tlp_table_inner_pkup_fetch.RowCount = 2;
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + item.Field3;
                            tlp_table_inner_pkup_fetch.Tag = item.Field3;
                            tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                            tlp_table_inner_pkup_fetch.Dock = DockStyle.Top;
                            tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                            tlp_table_inner_pkup_fetch.AccessibleDescription = item.Field1;
                            //tlp_table_inner_pkup_fetch.CellBorderStyle =TableLayoutPanelCellBorderStyle.Single;
                            //tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                            #endregion

                            #region  Label name  for  inside tablelayoutpanel inner
                            // Configure the Label
                            Label lbltablename_pkup_fetch = new Label();
                            lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + item.Field3;
                            lbltablename_pkup_fetch.Tag = item.Field3;
                            lbltablename_pkup_fetch.Text = "Pick Up \n " + item.Field6;
                            lbltablename_pkup_fetch.ForeColor = Color.Black;
                            lbltablename_pkup_fetch.BackColor = Color.Transparent;
                            lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                            lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                            lbltablename_pkup_fetch.TextAlign = ContentAlignment.TopCenter;
                            lbltablename_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Net Amount
                            // Configure the Label
                            Label netamountlabel_pkup_fetch = new Label();
                            netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + item.Field3;
                            netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pkup_fetch.Tag = item.Field3;
                            netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                            netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                            netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                            netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                            netamountlabel_pkup_fetch.Visible = false;
                            netamountlabel_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Netamount Show and hide order complete
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "COUNT(*)  Field1";//;= SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field3;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                            {
                                netamountlabel_pkup_fetch.Visible = true;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent,20);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent,80);
                                netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            }
                            else
                            {
                                netamountlabel_pkup_fetch.Visible = false;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.Text = "";
                            }
                            #endregion

                            #region Size setting Pickup Box
                            if (screenWidth > 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 300;
                                tlp_table_inner_pkup_fetch.Height = 100;
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 120;
                                tlp_table_inner_pkup_fetch.Height = 80;
                            }

                            #endregion

                            tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                            tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                            lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                            flp_pickup_view.Controls.Add(tlp_table_inner_pkup_fetch);

                            #endregion

                            #region Box Color
                            Color color = ColorTranslator.FromHtml(item.Field1);
                            tlp_table_inner_pkup_fetch.BackColor = color;
                            #endregion

                            flp_pickup_view.ResumeLayout();
                        }
                        #endregion
                    }
                    AutoTimer.Start();
                }
                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Pickuppage page  TableStaus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txtPkid_search.Text = "Token No ....";
            txtPkid_search.ForeColor = Color.DarkGray;
            LoadPickupView(Convert.ToInt32(Food_Status.Pending));
        }

        private void FrmPickup_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }  
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if( txtPkid_search.Text!= "Token No ....")
                {
                    AutoTimer.Stop();
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "PkId Field1,PickupOrderFlag Field2,OrderMasterId Field3,NetAmount Field4,KitchenNumber Field5,pkid Field6";
                    dmnSearch.Table = "tblOrderMaster";
                    dmnSearch.WhereNdGroupBy = "WHERE Status in(1,2) and  Pkid = " + txtPkid_search.Text;
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {

                        #region Remove all expect addnew tablelayoutpanel Pickup on complete or cancel
                        string addbuttonname = "tlp_addnew";
                        foreach (Control control in flp_pickup_view.Controls.Cast<TableLayoutPanel>().ToArray())
                        {
                            if (control is TableLayoutPanel tlp && tlp.Name != addbuttonname)
                            {
                                flp_pickup_view.Controls.Remove(control);
                                control.Dispose();
                            }
                        }
                        #endregion

                        flp_pickup_view.SuspendLayout();

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            var item = gridSource[i];

                            #region Pickup box Fetching Orders  

                            #region  TableLayoutPanel inner pickup
                            // Configure the tlp_table_inner_pkup
                            TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                            tlp_table_inner_pkup_fetch.RowCount = 2;
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + item.Field3;
                            tlp_table_inner_pkup_fetch.Tag = item.Field3;
                            tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                            tlp_table_inner_pkup_fetch.Dock = DockStyle.Top;
                            tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                            tlp_table_inner_pkup_fetch.AccessibleDescription = item.Field1;
                            tlp_table_inner_pkup_fetch.Visible = false;
                            //tlp_table_inner_pkup_fetch.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                            #endregion

                            #region  label name  inside tablelayoutpanel inner
                            // Configure the Label
                            Label lbltablename_pkup_fetch = new Label();
                            lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + item.Field3;
                            lbltablename_pkup_fetch.Tag = item.Field3;
                            lbltablename_pkup_fetch.Text = "Pick Up \n " + item.Field6;
                            lbltablename_pkup_fetch.ForeColor = Color.Black;
                            lbltablename_pkup_fetch.BackColor = Color.Transparent;
                            lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                            lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                            lbltablename_pkup_fetch.TextAlign = ContentAlignment.TopCenter;
                            lbltablename_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Net Amount
                            // Configure the Label
                            Label netamountlabel_pkup_fetch = new Label();
                            netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + item.Field3;
                            netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pkup_fetch.Tag = item.Field3;
                            netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                            netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                            netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                            netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                            netamountlabel_pkup_fetch.Visible = false;
                            netamountlabel_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Netamount Show and hide order complete
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "COUNT(*) Field1";
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field3;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                            {
                                netamountlabel_pkup_fetch.Visible = true;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 60);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                                netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            }
                            else
                            {
                                netamountlabel_pkup_fetch.Visible = false;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.Text = "";
                            }
                            #endregion

                            #region Size setting Pickup Box
                            if (screenWidth > 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 300;
                                tlp_table_inner_pkup_fetch.Height = 100;
                                tlp_addnew.Width = 300;
                                tlp_addnew.Height = 100;
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 120;
                                tlp_table_inner_pkup_fetch.Height = 80;

                                tlp_addnew.Width = 120;
                                tlp_addnew.Height = 80;

                            }

                            if (screenWidth > 1366)
                            {
                                panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                            }
                            #endregion

                            tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                            tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                            lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                            flp_pickup_view.Controls.Add(tlp_table_inner_pkup_fetch);
                            tlp_table_inner_pkup_fetch.Visible = true;

                            #endregion
                        }
                        flp_pickup_view.ResumeLayout();

                    }
                    else
                    {
                        CMessageBox.Show("Please Enter Valid Tocken Number", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    CMessageBox.Show("Please Enter A Token Number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPkid_search.Focus();
                }


            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Pickuppage page  LoadPickupView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void txtPkid_search_Leave(object sender, EventArgs e)
        {
            if (txtPkid_search.Text == "")
            {
                txtPkid_search.Text = "Token No ....";
                txtPkid_search.ForeColor = Color.DarkGray;
            }
        }

        private void txtPkid_search_Enter(object sender, EventArgs e)
        {
            if (txtPkid_search.Text == "Token No ....")
            {
                txtPkid_search.Text = "";
                txtPkid_search.ForeColor = Color.Black;
            }
        }

        private void txtPkid_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AutoTimer.Stop();
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "PkId Field1,PickupOrderFlag Field2,OrderMasterId Field3,NetAmount Field4,KitchenNumber Field5,pkid Field6";
                    dmnSearch.Table = "tblOrderMaster";
                    dmnSearch.WhereNdGroupBy = "WHERE Status in(1,2) and  Pkid = " + txtPkid_search.Text;
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {

                        #region Remove all expect addnew tablelayoutpanel Pickup on complete or cancel
                        string addbuttonname = "tlp_addnew";
                        foreach (Control control in flp_pickup_view.Controls.Cast<TableLayoutPanel>().ToArray())
                        {
                            if (control is TableLayoutPanel tlp && tlp.Name != addbuttonname)
                            {
                                flp_pickup_view.Controls.Remove(control);
                                control.Dispose();
                            }
                        }
                        #endregion

                        flp_pickup_view.SuspendLayout();

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            var item = gridSource[i];

                            #region Pickup box Fetching Orders  

                            #region  TableLayoutPanel inner pickup
                            // Configure the tlp_table_inner_pkup
                            TableLayoutPanel tlp_table_inner_pkup_fetch = new TableLayoutPanel();
                            tlp_table_inner_pkup_fetch.RowCount = 2;
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_table_inner_pkup_fetch.Name = "tlpboxpkup_add_" + item.Field3;
                            tlp_table_inner_pkup_fetch.Tag = item.Field3;
                            tlp_table_inner_pkup_fetch.BorderStyle = BorderStyle.Fixed3D;
                            tlp_table_inner_pkup_fetch.Dock = DockStyle.Top;
                            tlp_table_inner_pkup_fetch.Margin = new Padding(15, 10, 15, 10);
                            tlp_table_inner_pkup_fetch.AccessibleDescription = item.Field1;
                            tlp_table_inner_pkup_fetch.Visible = false;
                            //tlp_table_inner_pkup_fetch.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                            #endregion

                            #region  label name  inside tablelayoutpanel inner
                            // Configure the Label
                            Label lbltablename_pkup_fetch = new Label();
                            lbltablename_pkup_fetch.Name = "lbtablename_pkup_" + item.Field3;
                            lbltablename_pkup_fetch.Tag = item.Field3;
                            lbltablename_pkup_fetch.Text = "Pick Up \n " + item.Field6;
                            lbltablename_pkup_fetch.ForeColor = Color.Black;
                            lbltablename_pkup_fetch.BackColor = Color.Transparent;
                            lbltablename_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                            lbltablename_pkup_fetch.ForeColor = Defaultfontcolor;
                            lbltablename_pkup_fetch.TextAlign = ContentAlignment.TopCenter;
                            lbltablename_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Net Amount
                            // Configure the Label
                            Label netamountlabel_pkup_fetch = new Label();
                            netamountlabel_pkup_fetch.Name = "lb_netamount_pkup_" + item.Field3;
                            netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            netamountlabel_pkup_fetch.Tag = item.Field3;
                            netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                            netamountlabel_pkup_fetch.ForeColor = Netamountfontcolor;
                            netamountlabel_pkup_fetch.BackColor = Color.Transparent;
                            netamountlabel_pkup_fetch.Font = new Font("Arial", 10, FontStyle.Bold);
                            netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                            netamountlabel_pkup_fetch.Visible = false;
                            netamountlabel_pkup_fetch.AccessibleDescription = item.Field1;
                            #endregion

                            #region Netamount Show and hide order complete
                            BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "COUNT(*) Field1";
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "WHERE status=3 and AddonStatus in(1,0) and OrderMasterId = " + item.Field3;
                            dmnSearch.OrderBy = "";
                            var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);

                            if (Convert.ToInt32(gridSource_order[0].Field1) > 0)
                            {
                                netamountlabel_pkup_fetch.Visible = true;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 60);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                                netamountlabel_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                netamountlabel_pkup_fetch.Text = "\u20B9" + Convert.ToString(item.Field4);
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                            }
                            else
                            {
                                netamountlabel_pkup_fetch.Visible = false;
                                tlp_table_inner_pkup_fetch.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                                tlp_table_inner_pkup_fetch.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                                lbltablename_pkup_fetch.Dock = DockStyle.Fill;
                                lbltablename_pkup_fetch.TextAlign = ContentAlignment.MiddleCenter;
                                netamountlabel_pkup_fetch.Text = "";
                            }
                            #endregion

                            #region Size setting Pickup Box
                            if (screenWidth > 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 300;
                                tlp_table_inner_pkup_fetch.Height = 100;
                                tlp_addnew.Width = 300;
                                tlp_addnew.Height = 100;
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                tlp_table_inner_pkup_fetch.Width = 120;
                                tlp_table_inner_pkup_fetch.Height = 80;

                                tlp_addnew.Width = 120;
                                tlp_addnew.Height = 80;

                            }

                            if (screenWidth > 1366)
                            {
                                panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                            }
                            else if (screenWidth >= 1024 && screenWidth <= 1366)
                            {
                                panelsPerRow = (int)Math.Floor((double)this.Width / (tlp_table_inner_pkup_fetch.Width + 30));
                            }
                            #endregion

                            tlp_table_inner_pkup_fetch.Controls.Add(lbltablename_pkup_fetch, 0, 0);
                            tlp_table_inner_pkup_fetch.Controls.Add(netamountlabel_pkup_fetch, 0, 1);

                            lbltablename_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            netamountlabel_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);
                            tlp_table_inner_pkup_fetch.Click += new EventHandler(PickupboxClickEventHandler);

                            flp_pickup_view.Controls.Add(tlp_table_inner_pkup_fetch);
                            tlp_table_inner_pkup_fetch.Visible = true;

                            #endregion
                        }
                        flp_pickup_view.ResumeLayout();

                    }
                    else
                    {
                        CMessageBox.Show("Please Enter Valid Tocken Number", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Pickuppage page  LoadPickupView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void txtPkid_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the pressed key is not a digit and not a control key (like backspace), suppress it
                e.Handled = true;
            }
        }


     
    }

}
