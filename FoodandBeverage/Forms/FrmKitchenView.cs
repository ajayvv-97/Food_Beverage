using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BLL;
using Domain;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView : Form
    {
        public FrmKitchenView()
        {
            InitializeComponent();
        }
        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2);
        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtorderid_search })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception)
            {

            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
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


        #endregion

        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
            textb_shape();
            Fillorders();

        }
        public void Fillorders()
        {
            try
            {
                #region Fill Orders in Datagrid

                BllSearch<DmnOrderMaster> bllSearch = new BllSearch<DmnOrderMaster>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = " tblOrderMaster";
                dmnSearch.WhereNdGroupBy = "where status=1";
                dmnSearch.OrderBy = "order by OrderMasterId DESC";
                var gridSourcemaster = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSourcemaster.Count > 0)
                {                  
                    int rowIndex = 0;
                    flp_order_view.Controls.Clear();
                    TableLayoutPanel tlp_order_col = new TableLayoutPanel();
                    tlp_order_col.Dock = DockStyle.Fill;
                    for (int i = 0; i < gridSourcemaster.Count; i++)
                    {
                        dmnSearch.Select = "om.OrderMasterId Field1,ti.TableCode Field2,om.status Field3,od.itemName Field4,od.Quantity Field5,od.Notes Field6,od.OrderNo Field6,od.CreatedDateTime Fielddatetime";
                        dmnSearch.Table = " tblOrderDetails od Left Join tblOrderMaster om on  om.OrderMasterId= od.OrderMasterId Left Join tblTable_info  ti on ti.Tid=om.Tid";
                        dmnSearch.WhereNdGroupBy = "where om.status=1 and  od.status=1";
                        dmnSearch.OrderBy = "order by od.OrderDetailsId DESC";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                        //var distinctCategories = gridSource_details
                        //.Select(item => item.Field4)
                        //.Distinct()
                        //.ToList();

                        #region Main TableLayoutPanel 
                        // Main TableLayoutPanel for each product
                        TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                        tlp_product_box.Name = "tlp_product_box" + gridSource[i].Field1;
                        tlp_product_box.Dock = DockStyle.Top;
                        tlp_product_box.Width = 300;
                        tlp_product_box.Height = 300;
                        tlp_product_box.BackColor = boxcolor;
                        tlp_product_box.RowCount = 3;
                        tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                        tlp_product_box.Margin = new Padding(20,0,0,0);
                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        #endregion

                        #region Heading Time and Tablecode Frist Row
                        // Sub TableLayoutPanel for Heading
                        TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                        tlp_product_heading.Name = "tlp_product_heading" + gridSource[i].Field1;
                        tlp_product_heading.Dock = DockStyle.Top;
                        tlp_product_heading.Height = 20;
                        tlp_product_heading.ColumnCount = 2;
                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                        // Labels in Heading
                        Label label1 = new Label() { Text = gridSource[i].Field2, 
                            Name="lbl_tablecode"+gridSource[i].Field1, 
                            Dock = DockStyle.Fill,ForeColor=Color.White,
                            Font = new Font("Arial", 10,FontStyle.Bold)
                        };


                        DateTime startTime = gridSource[i].Fielddatetime;
                        DateTime currentTime = DateTime.Now;
                        TimeSpan timeDifference = currentTime - startTime;
                        int hoursDifference = timeDifference.Hours;
                        int minutesDifference = timeDifference.Minutes;
                        int secondsDifference = timeDifference.Seconds;
                        string timeDifferenceString = $"{hoursDifference} hours, {minutesDifference} minutes, {secondsDifference} seconds";
                        Label label2 = new Label()
                        {
                            Text = timeDifferenceString,
                            Name = "lbl_tablecode" + gridSource[i].Field1,
                            Dock = DockStyle.Fill,
                            ForeColor = Color.White,
                            Font = new Font("Arial", 10, FontStyle.Bold) ,
                            
                        };

                        tlp_product_heading.Controls.Add(label1, 0, 0);
                        tlp_product_heading.Controls.Add(label2, 1, 0);

                        // Adding Heading TableLayoutPanel to the first row
                        tlp_product_box.Controls.Add(tlp_product_heading, 0, 0);
                        #endregion

                        #region DataGridView Second Row
                        // DataGridView for product details
                        DataGridView dgv_product_details = new DataGridView();   
                        dgv_product_details.Name = "dgv_product_details" + gridSource[i].Field1;

                        dgv_product_details.BorderStyle = BorderStyle.None;
                        dgv_product_details.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                        dgv_product_details.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        dgv_product_details.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                        dgv_product_details.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                        dgv_product_details.BackgroundColor = Color.White;

                        dgv_product_details.EnableHeadersVisualStyles = false;
                        dgv_product_details.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                        dgv_product_details.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                        dgv_product_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgv_product_details.Dock = DockStyle.Fill;
              
                        dgv_product_details.ColumnHeadersHeight = 30; 

                        dgv_product_details.RowTemplate.Height = 30;

                        dgv_product_details.RowHeadersVisible = false;
                        // Add columns with specific cell type
                        DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                        DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();

                        dgv_product_details.Columns.Add(columnSlno);
                        dgv_product_details.Columns.Add(columnItemName);
                        dgv_product_details.Columns.Add(columnQty);
                        dgv_product_details.Columns.Add(columnNotes);

                        // Set cell type for each column
                        foreach (DataGridViewColumn column in dgv_product_details.Columns)
                        {
                            column.CellTemplate = new DataGridViewTextBoxCell(); 
                        }
                        // Set column headers
                        dgv_product_details.Columns[0].HeaderText = "Slno";
                        dgv_product_details.Columns[1].HeaderText = "Item Name";
                        dgv_product_details.Columns[2].HeaderText = "Qty";
                        dgv_product_details.Columns[3].HeaderText = "Notes";

                        dgv_product_details.Rows.Add(gridSource.Count);
                        for(int j=0;j< gridSource.Count;j++)
                        {
                            dgv_product_details.Rows[j].Cells[0].Value = j + 1;
                            dgv_product_details.Rows[j].Cells[1].Value = gridSource[j].Field4;
                            dgv_product_details.Rows[j].Cells[2].Value = gridSource[j].Field5;
                            dgv_product_details.Rows[j].Cells[3].Value = gridSource[j].Field6;
                        }

                        // Set AutoSizeMode for each column
                        dgv_product_details.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgv_product_details.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgv_product_details.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dgv_product_details.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        #endregion

                        #region Ready and Close Button Thrid Row
                        // Adding DataGridView to the second row
                        tlp_product_box.Controls.Add(dgv_product_details, 0, 1);

                        // Sub TableLayoutPanel for Buttons
                        TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                        tlp_buttons.Name = "tlp_buttons" + gridSource[i].Field1;
                        tlp_buttons.Dock = DockStyle.Top;
                        tlp_buttons.Height = 30;
                        tlp_buttons.ColumnCount = 2;
                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                        // PictureBox controls in the last row
                        PictureBox pb_ready = new PictureBox()
                        {
                            Name = "pb_ready" + gridSource[i].Field1,
                            Image = Properties.Resources.ready_button,  // Replace "YourAddImage" with the name of your resource image
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Dock = DockStyle.None,
                            Height = 30, Width = 60,
                            Tag = gridSource[i].Field1,
                        };
                        pb_ready.Click += pbreadyfood_Click;

                        PictureBox pb_cancelfood = new PictureBox()
                        {
                            Name = "pictureBoxRemove" + gridSource[i].Field1,
                            Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Dock = DockStyle.None,
                            Height=30,Width=60,
                            Tag= gridSource[i].Field1,
                        };
                        pb_cancelfood.Click += pbcancelfood_Click;

                        tlp_buttons.Controls.Add(pb_ready, 0, 0);
                        tlp_buttons.Controls.Add(pb_cancelfood, 1, 0);

                        // Adding Buttons TableLayoutPanel to the last row
                        tlp_product_box.Controls.Add(tlp_buttons, 0, 2);
                        #endregion

                        flp_order_view.Controls.Add(tlp_product_box);
                    }

                }
                
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            string pictureBoxName = clickedPictureBox.Name;

            /*  Control clickedControl = sender as Control;
                string txtname = "txtqty_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;*/

            // Add your logic here based on the click event
        }
        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            // Handle the click event here
            PictureBox clickedPictureBox = (PictureBox)sender;
            // You can access the properties of the clicked PictureBox, for example:
            string pictureBoxName = clickedPictureBox.Name;

            // Add your logic here based on the click event
        }
    }
}
