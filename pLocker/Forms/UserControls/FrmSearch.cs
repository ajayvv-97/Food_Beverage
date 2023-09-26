using BLL;
using Domain;
using pLocker.Interfaces;
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
//using pLocker.Forms.UserControls;

namespace pLocker.Forms.UserControls
{
    public partial class FrmSearch<T> : MetroFramework.Forms.MetroForm where T : class
    {
        #region Declarations
        public string _retValue { get; set; }
        DmnSearch _dmnSearch;
        List<string> _lstHeaders;
        List<string> _lstFields;
        BllSearch<T> bllSearch = new BllSearch<T>();
        int _formWidth, _formHeight, _formTop, _formLeft;
        SearchMode _searchType;
        FrmDataGridView<T> _frmGridSearch;
        string _defaultWhere = string.Empty;
        DataTable _dtColumnAlign;
        const int colIndex = 0;
        const int colAlign = 1;

        #endregion

        #region Functions
        public FrmSearch()
        {
            InitializeComponent();
        }
        public FrmSearch(DmnSearch dmnSearch, SearchMode searchType, int formWidth = 0, int formHeight = 0, int formTop = 0, int formLeft = 0, DataTable dtColumnAlign = null)
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
            _frmGridSearch = new FrmDataGridView<T>();

            try
            {

                this.Top = _formTop;
                this.Left = _formLeft;
                mpSearch.Top = 25;
                mpSearch.Left = 5;
                mpGeneralGrid.Top = 70;
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
                foreach (DataGridViewColumn col in gridSource.Columns)
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
                    Control[] ctrl = this.Controls.Find("metroTextBox" + index.ToString(), true);
                    MetroTextBox textBox = (MetroTextBox)ctrl[0];
                    textBox.Tag = ((_lstFields[index].Replace("as", string.Empty)).Replace("Field" + index.ToString(), string.Empty)).Trim();
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
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void MetroTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //    if (e.KeyCode == Keys.Enter)
                if (e.KeyCode == Keys.Down)
                {
                    dgvGeneral.Focus();
                    if (dgvGeneral.Rows.Count > 0)
                    {
                        dgvGeneral.Rows[0].Selected = true;
                    }

                }
                else
                {
                    FilterGrid();

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void metroTextBox10_Click(object sender, EventArgs e)
        {

        }

        private void DgvGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvGeneral.Rows.Count > 0)
                {
                    if (e.KeyCode == Keys.Enter)

                    {
                        this.DialogResult = DialogResult.OK;
                        this._retValue = Convert.ToString(dgvGeneral.CurrentRow.Cells[0].Value);
                        this.Close();
                    }
                    else if (e.KeyCode == Keys.Tab)
                    {
                        metroTextBox1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void DgvGeneral_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvGeneral.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this._retValue = Convert.ToString(dgvGeneral.CurrentRow.Cells[0].Value);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
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
                        if ((textBox.Text.Length) > 0)
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
