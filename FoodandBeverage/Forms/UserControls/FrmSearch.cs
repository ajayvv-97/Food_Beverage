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
using FoodandBeverage.Interfaces;

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
            try
            {
                _frmGridSearch = new FrmDataGridView<T>();
                lblRecordCount.Text = string.Empty;

                if (_dmnSearch == null)
                {
                    throw new InvalidOperationException("Search criteria were not supplied.");
                }

                _lstHeaders = (_dmnSearch.ColumnHeaders ?? string.Empty).Split(',').ToList();
                _lstFields = (_dmnSearch.Select ?? string.Empty).Split(',').ToList();

                ApplyResponsiveScreenSize();
                ConfigureFilterTextBoxes();
                FilterGrid();

                MetroTextBox firstFilter = GetFilterTextBox(1);
                if (firstFilter != null)
                {
                    firstFilter.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        private void ApplyResponsiveScreenSize()
        {
            Screen currentScreen = Owner != null
                ? Screen.FromControl(Owner)
                : Screen.FromPoint(Cursor.Position);

            Rectangle workingArea = currentScreen.WorkingArea;
            int targetWidth = Math.Min(workingArea.Width,
                Math.Max(600, (int)(workingArea.Width * 0.80)));
            int targetHeight = Math.Min(workingArea.Height,
                Math.Max(450, (int)(workingArea.Height * 0.80)));

            int left = workingArea.Left + (workingArea.Width - targetWidth) / 2;
            int top = workingArea.Top + (workingArea.Height - targetHeight) / 2;

            AutoSize = false;
            StartPosition = FormStartPosition.Manual;
            WindowState = FormWindowState.Normal;
            Bounds = new Rectangle(left, top, targetWidth, targetHeight);

            // Keep the existing layout containers responsive to the new client size.
            mpSearch.Dock = DockStyle.Top;
            mpSearch.Height = 58;
            mpGeneralGrid.Dock = DockStyle.Fill;
            dgvGeneral.Dock = DockStyle.Fill;
            dgvGeneral.AutoGenerateColumns = false;

            btnclose.Location = new Point(ClientSize.Width - btnclose.Width - 5, 5);
            btnclose.BringToFront();
        }

        private MetroTextBox GetFilterTextBox(int index)
        {
            Control[] controls = Controls.Find("metroTextBox" + index.ToString(), true);
            if (controls == null || controls.Length == 0)
            {
                return null;
            }

            return controls[0] as MetroTextBox;
        }

        private void ConfigureFilterTextBoxes()
        {
            int filterCount = Math.Min(
                Math.Min(_lstHeaders.Count, _lstFields.Count) - 1, 10);

            int availableWidth = Math.Max(100, mpSearch.ClientSize.Width - 20);
            int filterWidth = filterCount > 0
                ? Math.Max(100, availableWidth / filterCount)
                : availableWidth;

            for (int index = 1; index <= 10; index++)
            {
                MetroTextBox textBox = GetFilterTextBox(index);
                if (textBox == null)
                {
                    continue;
                }

                bool isActiveFilter = index <= filterCount;
                textBox.Visible = isActiveFilter;

                if (!isActiveFilter)
                {
                    textBox.Text = string.Empty;
                    continue;
                }

                string fieldExpression = _lstFields[index].Trim();
                int aliasIndex = fieldExpression.LastIndexOf(" as ",
                    StringComparison.OrdinalIgnoreCase);
                if (aliasIndex >= 0)
                {
                    fieldExpression = fieldExpression.Substring(0, aliasIndex).Trim();
                }

                textBox.Tag = fieldExpression;
                textBox.WaterMark = _lstHeaders[index];
                textBox.Width = filterWidth;
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
            try
            {
                //    if (e.KeyCode == Keys.Enter)
                if (e.KeyCode == Keys.Down)
                {
                    dgvGeneral.Focus();
                    if (dgvGeneral.Rows.Count > 0)
                    {
                        dgvGeneral.Rows[0].Selected=true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSearch_Resize(object sender, EventArgs e)
        {
            btnclose.Location = new Point(ClientSize.Width - btnclose.Width - 5, 5);
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
            try
            {
                if (_dmnSearch == null || _lstFields == null || _lstHeaders == null)
                {
                    return;
                }

                _frmGridSearch = new FrmDataGridView<T>();
                _dmnSearch.WhereNdGroupBy = _defaultWhere;

                int filterCount = Math.Min(_lstFields.Count, _lstHeaders.Count) - 1;
                for (int index = 1; index <= filterCount && index <= 10; index++)
                {
                    MetroTextBox textBox = GetFilterTextBox(index);
                    if (textBox == null || string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        continue;
                    }

                    string fieldExpression = Convert.ToString(textBox.Tag);
                    if (string.IsNullOrWhiteSpace(fieldExpression))
                    {
                        continue;
                    }

                    string filterValue = textBox.Text.Trim().Replace("'", "''");
                    _dmnSearch.WhereNdGroupBy +=
                        " AND " + fieldExpression + " LIKE '%" + filterValue + "%'";
                }

                var gridSource = _frmGridSearch.GetSearchGrid(_dmnSearch);

                dgvGeneral.DataSource = null;
                dgvGeneral.Rows.Clear();
                dgvGeneral.Columns.Clear();
                dgvGeneral.AutoGenerateColumns = false;

                if (gridSource != null)
                {
                    foreach (DataGridViewColumn column in gridSource.Columns)
                    {
                        dgvGeneral.Columns.Add(column.Clone() as DataGridViewColumn);
                    }

                    dgvGeneral.DataSource = gridSource.DataSource;
                }

                if (dgvGeneral.Columns.Count > 0)
                {
                    dgvGeneral.Columns[0].Visible = false;

                    if (dgvGeneral.Columns.Count > 1)
                    {
                        dgvGeneral.Columns[1].MinimumWidth =
                            Math.Min(320, Math.Max(120, dgvGeneral.ClientSize.Width / 3));
                    }

                    if (dgvGeneral.Rows.Count > 0)
                    {
                        if (_dtColumnAlign != null &&
                            _dtColumnAlign.Columns.Count > colAlign &&
                            _dtColumnAlign.Rows.Count > 0)
                        {
                            foreach (DataGridViewColumn column in dgvGeneral.Columns)
                            {
                                foreach (DataRow alignment in _dtColumnAlign.Rows)
                                {
                                    int alignmentIndex;
                                    if (!int.TryParse(Convert.ToString(alignment[colIndex]),
                                        out alignmentIndex) || column.Index != alignmentIndex)
                                    {
                                        continue;
                                    }

                                    string alignmentValue = Convert.ToString(alignment[colAlign]).ToUpperInvariant();
                                    if (alignmentValue == "M")
                                    {
                                        column.DefaultCellStyle.Alignment =
                                            DataGridViewContentAlignment.MiddleCenter;
                                    }
                                    else if (alignmentValue == "R" || alignmentValue == "N2")
                                    {
                                        column.DefaultCellStyle.Alignment =
                                            DataGridViewContentAlignment.MiddleRight;
                                    }

                                    if (alignmentValue == "N2")
                                    {
                                        column.DefaultCellStyle.Format = "N2";
                                    }
                                }
                            }
                        }

                        dgvGeneral.ClearSelection();
                        dgvGeneral.Rows[0].Selected = false;
                    }
                }

                lblRecordCount.Text = "Count : " + Convert.ToString(dgvGeneral.RowCount);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }

        #endregion

    }
}
