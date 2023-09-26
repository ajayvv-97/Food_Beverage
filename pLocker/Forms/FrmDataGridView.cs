using BLL;
using Domain;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace MarginPOSNew.Forms.UserControls
{
    public partial class FrmDataGridView<T> : Form where T:class
    {
        public FrmDataGridView()
        {
            InitializeComponent();
        }
        public DataGridView GetSearchGrid(DmnSearch dmnSearch)
        {
            BllSearch<T> bllSearch = new BllSearch<T>();
            List<string> lstHeaders = dmnSearch.ColumnHeaders.Split(',').ToList();
            List<string> lstFields = dmnSearch.Select.Split(',').ToList();

            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            dgvSearchGrid.DataSource = gridSource;
            
            if (dgvSearchGrid.Rows.Count > 0)
            {

                int index = 0;
                foreach (DataGridViewColumn col in dgvSearchGrid.Columns)
                {
                    if (index < lstFields.Count)
                    {
                     col.HeaderText = lstHeaders[index];
                     index++;
                    }
                    else
                    {
                        col.Visible = false;
                    }
                }
                dgvSearchGrid.Columns[0].Visible = false;
            }
            else
            {
                foreach (DataGridViewColumn col in dgvSearchGrid.Columns)
                {
                    col.Visible = false;
                }
            }
            return dgvSearchGrid;
        }

    }
}
