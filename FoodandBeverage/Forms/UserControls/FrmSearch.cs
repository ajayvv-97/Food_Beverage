using BLL;
using Domain;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms.UserControls
{
    public partial class FrmSearch<T>: MetroFramework.Forms.MetroForm where T:class
    {
        #region Declarations
        public string _retValue { get; set; }
        DmnSearch _dmnSearch;
        List<string> _lstHeaders;
        List<string> _lstFields;
        BllSearch<T> bllSearch = new BllSearch<T>();
        int _formWidth, _formHeight,_formTop,_formLeft;
        SearchMode _searchType;
        FrmDataGridView<T> _frmGridSearch;
        string _defaultWhere = string.Empty;
        DataTable _dtColumnAlign;
        const int colIndex = 0;
        private MetroPanel mpSearch;
        private TableLayoutPanel tlpFilter;
        private MetroTextBox metroTextBox1;
        private MetroTextBox metroTextBox2;
        private MetroTextBox metroTextBox10;
        private MetroTextBox metroTextBox3;
        private MetroTextBox metroTextBox9;
        private MetroTextBox metroTextBox4;
        private MetroTextBox metroTextBox8;
        private MetroTextBox metroTextBox5;
        private MetroTextBox metroTextBox7;
        private MetroTextBox metroTextBox6;
        private MetroPanel mpGeneralGrid;
        private DataGridView dgvGeneral;
        private Label lblRecordCount;
        private Label lblInfo;
        const int colAlign = 1;

        #endregion

        #region Functions
        public FrmSearch()
        {
            InitializeComponent();
        }
        public FrmSearch(DmnSearch dmnSearch, SearchMode searchType, int formWidth = 0, int formHeight = 0, int formTop = 0, int formLeft = 0, DataTable dtColumnAlign=null)
        {
            _dmnSearch = dmnSearch;
            _searchType = searchType;
            _formWidth = formWidth;
            _formHeight = formHeight;
            _formTop = formTop;
            _formLeft = formLeft;
            _defaultWhere = string.IsNullOrWhiteSpace(dmnSearch.WhereNdGroupBy) ? "Where 1=1 " : dmnSearch.WhereNdGroupBy;
            _dtColumnAlign = dtColumnAlign;
            InitializeComponent();
        }
        #endregion

        #region Events
        public void FrmSelect_Load(object sender, EventArgs e)
        {
            _frmGridSearch=new FrmDataGridView<T>();

            try
            {
                
                this.Top = _formTop;
                this.Left = _formLeft;
                mpSearch.Top = 25;
                mpSearch.Left = 5;
                mpGeneralGrid.Top =  70;
                mpGeneralGrid.Left = 20;
                dgvGeneral.Top = 7;
                dgvGeneral.Left = 8;
                lblRecordCount.Text = string.Empty;

                //this.Width = _formWidth;
                //this.Height = _formHeight;
                //mpGeneralGrid.Width = _formWidth;
                //mpGeneralGrid.Height = _formHeight+150;
                //dgvGeneral.Width = _formWidth - 15;
                //dgvGeneral.Height = _formHeight + 148;
                //mpSearch.Width = _formWidth;



                this.Width = _formWidth;
                this.Height = _formHeight;
                mpGeneralGrid.Width = _formWidth;
                mpGeneralGrid.Height = _formHeight - 5;
                dgvGeneral.Width = _formWidth - 15;
                dgvGeneral.Height = _formHeight - 20;
                mpSearch.Width = _formWidth;

    //            if(_dmnSearch.Select.Contains("FromOfferForm"))
				//{
    //                _dmnSearch.ColumnHeaders.Replace(",Offer", ",");
    //                _dmnSearch.Select.Replace(",'FromOfferForm' as Field4", ",");
    //                dgvGeneral.MultiSelect = true;
    //            }
                _lstHeaders = _dmnSearch.ColumnHeaders.Split(',').ToList();
                _lstFields = _dmnSearch.Select.Split(',').ToList();
                var gridSource = _frmGridSearch.GetSearchGrid(_dmnSearch);
                foreach(DataGridViewColumn col in gridSource.Columns)
                {
                    dgvGeneral.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvGeneral.DataSource = gridSource.DataSource;
                lblRecordCount.Text = "Count : " + Convert.ToString(dgvGeneral.RowCount);
                if (dgvGeneral.Rows.Count > 0)
                {
                    if (_dtColumnAlign != null)
                    {
                        if (_dtColumnAlign.Rows.Count > 0)
                        {
                            foreach (DataGridViewColumn col in dgvGeneral.Columns)
                            {
                                foreach (DataRow row in _dtColumnAlign.Rows)
                                {
                                    if (col.Index == Convert.ToInt32(row[colIndex].ToString()))
                                    {
                                        if (row[colAlign].ToString().ToUpper() == "M")
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        }
                                        else if (row[colAlign].ToString().ToUpper() == "R")
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        }
                                        else
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                        }
                                        if (row[colAlign].ToString().ToUpper() == "N2")
                                        {
                                            int colin = col.Index;
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                            dgvGeneral.Columns[col.Index].DefaultCellStyle.Format = "N2";
                                        }
                                    }
                                }

                            }
                        }
                    }
                    dgvGeneral.Rows[0].Selected = false;
                    dgvGeneral.Columns[1].MinimumWidth = 320;
                }
                int mtxbxWidth = 0;
                mtxbxWidth = mpSearch.Width / _lstHeaders.Count;


                for (int index = 1; index < _lstFields.Count; ++index)
                {
                    Control[] ctrl  = this.Controls.Find("metroTextBox"+index.ToString(), true);
                    MetroTextBox textBox = (MetroTextBox)ctrl[0];
                    textBox.Tag = ((_lstFields[index].Replace("as",string.Empty)).Replace("Field"+index.ToString(),string.Empty)).Trim();
                    textBox.Visible = true;
                    textBox.WaterMark = _lstHeaders[index];
                    textBox.Width = mtxbxWidth;
                    
                }
                metroTextBox1.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void FrmSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Escape)
                {
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void MetroTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mpSearch = new MetroFramework.Controls.MetroPanel();
            this.metroTextBox6 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox7 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox5 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox8 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox4 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox9 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox3 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox10 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.tlpFilter = new System.Windows.Forms.TableLayoutPanel();
            this.mpGeneralGrid = new MetroFramework.Controls.MetroPanel();
            this.dgvGeneral = new System.Windows.Forms.DataGridView();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.mpSearch.SuspendLayout();
            this.tlpFilter.SuspendLayout();
            this.mpGeneralGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // mpSearch
            // 
            this.mpSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(37)))), ((int)(((byte)(94)))));
            this.mpSearch.Controls.Add(this.tlpFilter);
            this.mpSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.mpSearch.HorizontalScrollbarBarColor = true;
            this.mpSearch.HorizontalScrollbarHighlightOnWheel = false;
            this.mpSearch.HorizontalScrollbarSize = 10;
            this.mpSearch.Location = new System.Drawing.Point(20, 60);
            this.mpSearch.Name = "mpSearch";
            this.mpSearch.Size = new System.Drawing.Size(806, 40);
            this.mpSearch.TabIndex = 4;
            this.mpSearch.UseCustomBackColor = true;
            this.mpSearch.VerticalScrollbarBarColor = true;
            this.mpSearch.VerticalScrollbarHighlightOnWheel = false;
            this.mpSearch.VerticalScrollbarSize = 10;
            // 
            // metroTextBox6
            // 
            // 
            // 
            // 
            this.metroTextBox6.CustomButton.Image = null;
            this.metroTextBox6.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox6.CustomButton.Name = "";
            this.metroTextBox6.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox6.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox6.CustomButton.TabIndex = 1;
            this.metroTextBox6.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox6.CustomButton.UseSelectable = true;
            this.metroTextBox6.CustomButton.Visible = false;
            this.metroTextBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox6.Lines = new string[0];
            this.metroTextBox6.Location = new System.Drawing.Point(213, 3);
            this.metroTextBox6.MaxLength = 32767;
            this.metroTextBox6.Name = "metroTextBox6";
            this.metroTextBox6.PasswordChar = '\0';
            this.metroTextBox6.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox6.SelectedText = "";
            this.metroTextBox6.SelectionLength = 0;
            this.metroTextBox6.SelectionStart = 0;
            this.metroTextBox6.ShortcutsEnabled = true;
            this.metroTextBox6.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox6.TabIndex = 5;
            this.metroTextBox6.UseSelectable = true;
            this.metroTextBox6.Visible = false;
            this.metroTextBox6.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox6.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox7
            // 
            // 
            // 
            // 
            this.metroTextBox7.CustomButton.Image = null;
            this.metroTextBox7.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox7.CustomButton.Name = "";
            this.metroTextBox7.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox7.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox7.CustomButton.TabIndex = 1;
            this.metroTextBox7.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox7.CustomButton.UseSelectable = true;
            this.metroTextBox7.CustomButton.Visible = false;
            this.metroTextBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox7.Lines = new string[0];
            this.metroTextBox7.Location = new System.Drawing.Point(255, 3);
            this.metroTextBox7.MaxLength = 32767;
            this.metroTextBox7.Name = "metroTextBox7";
            this.metroTextBox7.PasswordChar = '\0';
            this.metroTextBox7.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox7.SelectedText = "";
            this.metroTextBox7.SelectionLength = 0;
            this.metroTextBox7.SelectionStart = 0;
            this.metroTextBox7.ShortcutsEnabled = true;
            this.metroTextBox7.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox7.TabIndex = 6;
            this.metroTextBox7.UseSelectable = true;
            this.metroTextBox7.Visible = false;
            this.metroTextBox7.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox7.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox5
            // 
            // 
            // 
            // 
            this.metroTextBox5.CustomButton.Image = null;
            this.metroTextBox5.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox5.CustomButton.Name = "";
            this.metroTextBox5.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox5.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox5.CustomButton.TabIndex = 1;
            this.metroTextBox5.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox5.CustomButton.UseSelectable = true;
            this.metroTextBox5.CustomButton.Visible = false;
            this.metroTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox5.Lines = new string[0];
            this.metroTextBox5.Location = new System.Drawing.Point(171, 3);
            this.metroTextBox5.MaxLength = 32767;
            this.metroTextBox5.Name = "metroTextBox5";
            this.metroTextBox5.PasswordChar = '\0';
            this.metroTextBox5.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox5.SelectedText = "";
            this.metroTextBox5.SelectionLength = 0;
            this.metroTextBox5.SelectionStart = 0;
            this.metroTextBox5.ShortcutsEnabled = true;
            this.metroTextBox5.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox5.TabIndex = 4;
            this.metroTextBox5.UseSelectable = true;
            this.metroTextBox5.Visible = false;
            this.metroTextBox5.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox5.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox8
            // 
            // 
            // 
            // 
            this.metroTextBox8.CustomButton.Image = null;
            this.metroTextBox8.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox8.CustomButton.Name = "";
            this.metroTextBox8.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox8.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox8.CustomButton.TabIndex = 1;
            this.metroTextBox8.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox8.CustomButton.UseSelectable = true;
            this.metroTextBox8.CustomButton.Visible = false;
            this.metroTextBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox8.Lines = new string[0];
            this.metroTextBox8.Location = new System.Drawing.Point(297, 3);
            this.metroTextBox8.MaxLength = 32767;
            this.metroTextBox8.Name = "metroTextBox8";
            this.metroTextBox8.PasswordChar = '\0';
            this.metroTextBox8.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox8.SelectedText = "";
            this.metroTextBox8.SelectionLength = 0;
            this.metroTextBox8.SelectionStart = 0;
            this.metroTextBox8.ShortcutsEnabled = true;
            this.metroTextBox8.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox8.TabIndex = 7;
            this.metroTextBox8.UseSelectable = true;
            this.metroTextBox8.Visible = false;
            this.metroTextBox8.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox8.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox4
            // 
            // 
            // 
            // 
            this.metroTextBox4.CustomButton.Image = null;
            this.metroTextBox4.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox4.CustomButton.Name = "";
            this.metroTextBox4.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox4.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox4.CustomButton.TabIndex = 1;
            this.metroTextBox4.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox4.CustomButton.UseSelectable = true;
            this.metroTextBox4.CustomButton.Visible = false;
            this.metroTextBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox4.Lines = new string[0];
            this.metroTextBox4.Location = new System.Drawing.Point(129, 3);
            this.metroTextBox4.MaxLength = 32767;
            this.metroTextBox4.Name = "metroTextBox4";
            this.metroTextBox4.PasswordChar = '\0';
            this.metroTextBox4.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox4.SelectedText = "";
            this.metroTextBox4.SelectionLength = 0;
            this.metroTextBox4.SelectionStart = 0;
            this.metroTextBox4.ShortcutsEnabled = true;
            this.metroTextBox4.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox4.TabIndex = 3;
            this.metroTextBox4.UseSelectable = true;
            this.metroTextBox4.Visible = false;
            this.metroTextBox4.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox4.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox9
            // 
            // 
            // 
            // 
            this.metroTextBox9.CustomButton.Image = null;
            this.metroTextBox9.CustomButton.Location = new System.Drawing.Point(8, 2);
            this.metroTextBox9.CustomButton.Name = "";
            this.metroTextBox9.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox9.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox9.CustomButton.TabIndex = 1;
            this.metroTextBox9.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox9.CustomButton.UseSelectable = true;
            this.metroTextBox9.CustomButton.Visible = false;
            this.metroTextBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox9.Lines = new string[0];
            this.metroTextBox9.Location = new System.Drawing.Point(339, 3);
            this.metroTextBox9.MaxLength = 32767;
            this.metroTextBox9.Name = "metroTextBox9";
            this.metroTextBox9.PasswordChar = '\0';
            this.metroTextBox9.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox9.SelectedText = "";
            this.metroTextBox9.SelectionLength = 0;
            this.metroTextBox9.SelectionStart = 0;
            this.metroTextBox9.ShortcutsEnabled = true;
            this.metroTextBox9.Size = new System.Drawing.Size(40, 34);
            this.metroTextBox9.TabIndex = 8;
            this.metroTextBox9.UseSelectable = true;
            this.metroTextBox9.Visible = false;
            this.metroTextBox9.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox9.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox3
            // 
            // 
            // 
            // 
            this.metroTextBox3.CustomButton.Image = null;
            this.metroTextBox3.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox3.CustomButton.Name = "";
            this.metroTextBox3.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox3.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox3.CustomButton.TabIndex = 1;
            this.metroTextBox3.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox3.CustomButton.UseSelectable = true;
            this.metroTextBox3.CustomButton.Visible = false;
            this.metroTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox3.Lines = new string[0];
            this.metroTextBox3.Location = new System.Drawing.Point(87, 3);
            this.metroTextBox3.MaxLength = 32767;
            this.metroTextBox3.Name = "metroTextBox3";
            this.metroTextBox3.PasswordChar = '\0';
            this.metroTextBox3.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox3.SelectedText = "";
            this.metroTextBox3.SelectionLength = 0;
            this.metroTextBox3.SelectionStart = 0;
            this.metroTextBox3.ShortcutsEnabled = true;
            this.metroTextBox3.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox3.TabIndex = 2;
            this.metroTextBox3.UseSelectable = true;
            this.metroTextBox3.Visible = false;
            this.metroTextBox3.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox3.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox10
            // 
            // 
            // 
            // 
            this.metroTextBox10.CustomButton.Image = null;
            this.metroTextBox10.CustomButton.Location = new System.Drawing.Point(482, 2);
            this.metroTextBox10.CustomButton.Name = "";
            this.metroTextBox10.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox10.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox10.CustomButton.TabIndex = 1;
            this.metroTextBox10.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox10.CustomButton.UseSelectable = true;
            this.metroTextBox10.CustomButton.Visible = false;
            this.metroTextBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox10.Lines = new string[0];
            this.metroTextBox10.Location = new System.Drawing.Point(385, 3);
            this.metroTextBox10.MaxLength = 32767;
            this.metroTextBox10.Name = "metroTextBox10";
            this.metroTextBox10.PasswordChar = '\0';
            this.metroTextBox10.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox10.SelectedText = "";
            this.metroTextBox10.SelectionLength = 0;
            this.metroTextBox10.SelectionStart = 0;
            this.metroTextBox10.ShortcutsEnabled = true;
            this.metroTextBox10.Size = new System.Drawing.Size(514, 34);
            this.metroTextBox10.TabIndex = 9;
            this.metroTextBox10.UseSelectable = true;
            this.metroTextBox10.Visible = false;
            this.metroTextBox10.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox10.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox2
            // 
            // 
            // 
            // 
            this.metroTextBox2.CustomButton.Image = null;
            this.metroTextBox2.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox2.CustomButton.Name = "";
            this.metroTextBox2.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox2.CustomButton.TabIndex = 1;
            this.metroTextBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox2.CustomButton.UseSelectable = true;
            this.metroTextBox2.CustomButton.Visible = false;
            this.metroTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox2.Lines = new string[0];
            this.metroTextBox2.Location = new System.Drawing.Point(45, 3);
            this.metroTextBox2.MaxLength = 32767;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.PasswordChar = '\0';
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox2.SelectedText = "";
            this.metroTextBox2.SelectionLength = 0;
            this.metroTextBox2.SelectionStart = 0;
            this.metroTextBox2.ShortcutsEnabled = true;
            this.metroTextBox2.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox2.TabIndex = 1;
            this.metroTextBox2.UseSelectable = true;
            this.metroTextBox2.Visible = false;
            this.metroTextBox2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroTextBox1
            // 
            // 
            // 
            // 
            this.metroTextBox1.CustomButton.Image = null;
            this.metroTextBox1.CustomButton.Location = new System.Drawing.Point(4, 2);
            this.metroTextBox1.CustomButton.Name = "";
            this.metroTextBox1.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.metroTextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTextBox1.CustomButton.TabIndex = 1;
            this.metroTextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroTextBox1.CustomButton.UseSelectable = true;
            this.metroTextBox1.CustomButton.Visible = false;
            this.metroTextBox1.DisplayIcon = true;
            this.metroTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTextBox1.Lines = new string[0];
            this.metroTextBox1.Location = new System.Drawing.Point(3, 3);
            this.metroTextBox1.MaxLength = 32767;
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.PasswordChar = '\0';
            this.metroTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.metroTextBox1.SelectedText = "";
            this.metroTextBox1.SelectionLength = 0;
            this.metroTextBox1.SelectionStart = 0;
            this.metroTextBox1.ShortcutsEnabled = true;
            this.metroTextBox1.Size = new System.Drawing.Size(36, 34);
            this.metroTextBox1.TabIndex = 0;
            this.metroTextBox1.UseSelectable = true;
            this.metroTextBox1.Visible = false;
            this.metroTextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.metroTextBox1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tlpFilter
            // 
            this.tlpFilter.ColumnCount = 10;
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpFilter.Controls.Add(this.metroTextBox1, 0, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox2, 1, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox10, 9, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox3, 2, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox9, 8, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox4, 3, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox8, 7, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox5, 4, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox7, 6, 0);
            this.tlpFilter.Controls.Add(this.metroTextBox6, 5, 0);
            this.tlpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFilter.Location = new System.Drawing.Point(0, 0);
            this.tlpFilter.Name = "tlpFilter";
            this.tlpFilter.RowCount = 1;
            this.tlpFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFilter.Size = new System.Drawing.Size(806, 40);
            this.tlpFilter.TabIndex = 2;
            // 
            // mpGeneralGrid
            // 
            this.mpGeneralGrid.BackColor = System.Drawing.Color.White;
            this.mpGeneralGrid.Controls.Add(this.dgvGeneral);
            this.mpGeneralGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpGeneralGrid.HorizontalScrollbarBarColor = true;
            this.mpGeneralGrid.HorizontalScrollbarHighlightOnWheel = false;
            this.mpGeneralGrid.HorizontalScrollbarSize = 10;
            this.mpGeneralGrid.Location = new System.Drawing.Point(20, 100);
            this.mpGeneralGrid.Name = "mpGeneralGrid";
            this.mpGeneralGrid.Padding = new System.Windows.Forms.Padding(10);
            this.mpGeneralGrid.Size = new System.Drawing.Size(806, 314);
            this.mpGeneralGrid.TabIndex = 5;
            this.mpGeneralGrid.UseCustomBackColor = true;
            this.mpGeneralGrid.VerticalScrollbarBarColor = true;
            this.mpGeneralGrid.VerticalScrollbarHighlightOnWheel = false;
            this.mpGeneralGrid.VerticalScrollbarSize = 10;
            // 
            // dgvGeneral
            // 
            this.dgvGeneral.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvGeneral.AllowUserToAddRows = false;
            this.dgvGeneral.AllowUserToDeleteRows = false;
            this.dgvGeneral.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGeneral.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGeneral.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvGeneral.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGeneral.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGeneral.ColumnHeadersHeight = 40;
            this.dgvGeneral.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGeneral.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGeneral.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGeneral.EnableHeadersVisualStyles = false;
            this.dgvGeneral.Location = new System.Drawing.Point(10, 10);
            this.dgvGeneral.MultiSelect = false;
            this.dgvGeneral.Name = "dgvGeneral";
            this.dgvGeneral.ReadOnly = true;
            this.dgvGeneral.RowHeadersVisible = false;
            this.dgvGeneral.RowTemplate.Height = 35;
            this.dgvGeneral.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGeneral.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGeneral.ShowCellErrors = false;
            this.dgvGeneral.ShowCellToolTips = false;
            this.dgvGeneral.ShowEditingIcon = false;
            this.dgvGeneral.Size = new System.Drawing.Size(786, 294);
            this.dgvGeneral.TabIndex = 0;
            this.dgvGeneral.TabStop = false;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRecordCount.AutoSize = true;
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordCount.Location = new System.Drawing.Point(-35, 439);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(45, 19);
            this.lblRecordCount.TabIndex = 6;
            this.lblRecordCount.Text = "label1";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(760, 437);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(122, 19);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Press Esc To Close";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FrmSearch
            // 
            this.ClientSize = new System.Drawing.Size(846, 434);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblRecordCount);
            this.Controls.Add(this.mpGeneralGrid);
            this.Controls.Add(this.mpSearch);
            this.Name = "FrmSearch";
            this.mpSearch.ResumeLayout(false);
            this.tlpFilter.ResumeLayout(false);
            this.mpGeneralGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeneral)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DgvGeneral_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DgvGeneral_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void FilterGrid()
        {
            _frmGridSearch = new FrmDataGridView<T>();
            try
            {
                dgvGeneral.DataSource = null;
                dgvGeneral.Rows.Clear();
                bllSearch = new BllSearch<T>();
               _dmnSearch.WhereNdGroupBy = _defaultWhere;
               
                    for (int index = 1; index < _lstFields.Count; ++index)
                    {
                        Control[] ctrl = this.Controls.Find("metroTextBox" + index.ToString(), true);
                        MetroTextBox textBox = (MetroTextBox)ctrl[0];
                    ////////////if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        string str1 = textBox.Tag.ToString();
                        str1 = str1.Replace("As", "");
                        if ((textBox.Text.Length)>0)
                            {
                            if (_searchType == SearchMode.AllCharacters)
                            //if (_searchType == SearchMode.FirstCharacter)
                            {
                                _dmnSearch.WhereNdGroupBy += " and " + str1 + " like '%" + textBox.Text + "%'";
                            }
                            else
                            {
                                _dmnSearch.WhereNdGroupBy += " and " + str1 + " like '%" + textBox.Text + "%'";
                            }
                        }
                    }
                        
                    }
                
                var gridSource = _frmGridSearch.GetSearchGrid(_dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvGeneral.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvGeneral.DataSource = gridSource.DataSource;
                if (dgvGeneral.Rows.Count > 0)
                {
                    if (_dtColumnAlign != null)
                    {
                        if (_dtColumnAlign.Rows.Count > 0)
                        {
                            foreach (DataGridViewColumn col in dgvGeneral.Columns)
                            {
                                foreach (DataRow row in _dtColumnAlign.Rows)
                                {
                                    if (col.Index == Convert.ToInt32(row[colIndex].ToString()))
                                    {
                                        if (row[colAlign].ToString().ToUpper() == "M")
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        }
                                        else if (row[colAlign].ToString().ToUpper() == "R")
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        }
                                        else
                                        {
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                        }
                                        if (row[colAlign].ToString().ToUpper() == "N2")
                                        {
                                            int colin = col.Index;
                                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                            dgvGeneral.Columns[col.Index].DefaultCellStyle.Format = "N2";
                                        }
                                    }
                                }

                            }
                        }
                    }
                    dgvGeneral.Rows[0].Selected = false;
                }
                lblRecordCount.Text = "Count : " + Convert.ToString(dgvGeneral.RowCount);
                dgvGeneral.Columns[0].Visible = false;
                dgvGeneral.Columns[1].MinimumWidth = 320;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }

        }
        #endregion

    }
}
