using BLL;
using Domain;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmTable : Form
    {
        public FrmTable()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool Isvalid()
        {          
            if (string.IsNullOrWhiteSpace(txttblcode.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Table Name must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txttblcode.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtcapacity.Text.Trim()))
            {
                MetroMessageBox.Show(this, "", "Table Capacity must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtcapacity.Focus();
                return false;
            }            
            else if(cmbtbltype.SelectedIndex == 0)
            {
                MetroMessageBox.Show(this, "", "Please Select Table Type", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                cmbtbltype.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //bool flag = false;
                //int BulkId = 0;
                if (Isvalid() == false)
                    return;
                Dmntable dmntable = new Dmntable();
                BllTable blltable = new BllTable();
                if (string.IsNullOrEmpty(txtTid.Text))
                    txtTid.Text = "0";
                dmntable.@Tid = Convert.ToInt32(txtTid.Text); ;
                dmntable.TableCode = Convert.ToString(txttblcode.Text);
                dmntable.Capcity = Convert.ToDecimal(txtcapacity.Text);
                dmntable.TCid = Convert.ToInt32(cmbtbltype.SelectedValue);
                dmntable.RevStatus = 1;
                dmntable.Decription = Convert.ToString(txtdesc.Text);
                dmntable.CreatedBy = 1;
                dmntable.BranchId = 1;
                dmntable.Status = 1;
                var InsertStatus = blltable.AddEditData("prdInsert_table_info", dmntable);
                if (InsertStatus.Tid == 0)
                    MetroMessageBox.Show(this,"Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MetroMessageBox.Show(this, "Save Sucess", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                //MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
