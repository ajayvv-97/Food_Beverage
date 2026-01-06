using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class Frmvoidsale : Form
    {
        ClsGeneral clsgen = new ClsGeneral();
        public Frmvoidsale()
        {
            InitializeComponent();
        }
        int sSlno = 0;
        int sPName = 1;
        int sQty = 3;
        int sPrice = 2;
        int sid = 4;

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsearchbill_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Entry No,EntryDate,Net Amount";

                _objSearch.Select =
                "S.SalesMasterId as Id," +
                "EntryNo as Field1," +
                "EntryDate as Field2," +
                "S.NetAmount as Field3";

                _objSearch.Table =
                "tblsalesmaster S LEFT JOIN tblSalesDetails SD " +
                "ON S.SalesMasterId = SD.SalesMasterId";

                _objSearch.WhereNdGroupBy =
                "WHERE S.Status = 1 AND Tid = 0 " +
                "GROUP BY S.SalesMasterId, S.EntryNo, S.EntryDate, S.NetAmount";

                _objSearch.OrderBy = "ORDER BY Id DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(
                        _objSearch,
                        SearchMode.FirstCharacter,
                        this.Width - 150,
                        this.Height - 150,
                        this.Location.Y + 50,
                        this.Location.X + 20,
                        dtColumnAlign))
                    {
                        if (search.ShowDialog() == DialogResult.OK)
                        {
                            returnValue = Convert.ToInt32(search._retValue);
                        }
                    }
                    if (returnValue > 0)
                    {
                        int aid = Convert.ToInt32(returnValue);
                        BllSearch<DmnSaledetails> bllSearch1 = new BllSearch<DmnSaledetails>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "SalesMasterId as ID ,NetAmount,Quantity,ItemName";
                        objSearch.Table = "tblSalesDetails ";
                        objSearch.WhereNdGroupBy = "where status = 1 and SalesMasterId=" + aid ;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                        int SL_Main = 0;
                        if (itemSource.Count > 0)
                        {
                            dgv_productlist.Rows.Clear();
                            
                            for (int i = 0; i < itemSource.Count; i++)
                            {
                                dgv_productlist.Rows.Add();
                                SL_Main++;
                                dgv_productlist.Rows[i].Cells[sSlno].Value = SL_Main;
                                dgv_productlist.Rows[i].Cells[sPName].Value = Convert.ToString(itemSource[i].ItemName);
                                dgv_productlist.Rows[i].Cells[sQty].Value = Convert.ToDecimal(itemSource[i].Quantity);
                                dgv_productlist.Rows[i].Cells[sPrice].Value = Convert.ToDecimal(itemSource[i].NetAmount);
                                dgv_productlist.Rows[i].Cells[sid].Value = Convert.ToString(aid);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Void Sale page btnsearchbill_Click()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Voidsale));
            }
        }

        private void btnvoid_Click(object sender, EventArgs e)
        {
            try
            {
                #region Void Order Items
                DialogResult result = MessageBox.Show("Do you want to void this sale?","Void Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    BllProducts bllfav = new BllProducts();
                    DmnGridFields dmn = new DmnGridFields();

                    dmn.Field1 = Convert.ToString(dgv_productlist.Rows[0].Cells[sid].Value);

                    bllfav.AddEditData_voidsale("prd_VoidSale", dmn);
                    MessageBox.Show("Void Sale Successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv_productlist.Rows.Clear();
                    return;

                }
                else
                {
                    return;      
                }


            }
                #endregion

            catch (Exception ex)
            {
                clsgen.LogException("Void Sale page btnsearchbill_Click()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Voidsale));
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {

            dgv_productlist.Rows.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Entry No,EntryDate,Net Amount";

                _objSearch.Select =
                "S.SalesMasterId as Id," +
                "EntryNo as Field1," +
                "EntryDate as Field2," +
                "S.NetAmount as Field3";

                _objSearch.Table =
                "tblsalesmaster S LEFT JOIN tblSalesDetails SD " +
                "ON S.SalesMasterId = SD.SalesMasterId";

                _objSearch.WhereNdGroupBy =
                "WHERE S.Status = 0 AND Tid = 0 " +
                "GROUP BY S.SalesMasterId, S.EntryNo, S.EntryDate, S.NetAmount";

                _objSearch.OrderBy = "ORDER BY Id DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(
                        _objSearch,
                        SearchMode.FirstCharacter,
                        this.Width - 150,
                        this.Height - 150,
                        this.Location.Y + 50,
                        this.Location.X + 20,
                        dtColumnAlign))
                    {
                        if (search.ShowDialog() == DialogResult.OK)
                        {
                            returnValue = Convert.ToInt32(search._retValue);
                        }
                    }
                    if (returnValue > 0)
                    {
                        int aid = Convert.ToInt32(returnValue);
                        btnvoid.Visible = false;
                        BllSearch<DmnSaledetails> bllSearch1 = new BllSearch<DmnSaledetails>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "SalesMasterId as ID ,NetAmount,Quantity,ItemName";
                        objSearch.Table = "tblSalesDetails ";
                        objSearch.WhereNdGroupBy = "where status = 0 and SalesMasterId=" + aid;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                        int SL_Main = 0;
                        if (itemSource.Count > 0)
                        {
                            dgv_productlist.Rows.Clear();

                            for (int i = 0; i < itemSource.Count; i++)
                            {
                                dgv_productlist.Rows.Add();
                                SL_Main++;
                                dgv_productlist.Rows[i].Cells[sSlno].Value = SL_Main;
                                dgv_productlist.Rows[i].Cells[sPName].Value = Convert.ToString(itemSource[i].ItemName);
                                dgv_productlist.Rows[i].Cells[sQty].Value = Convert.ToDecimal(itemSource[i].Quantity);
                                dgv_productlist.Rows[i].Cells[sPrice].Value = Convert.ToDecimal(itemSource[i].NetAmount);
                                dgv_productlist.Rows[i].Cells[sid].Value = Convert.ToString(aid);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Void Sale page btnsearchbill_Click()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Voidsale));
            }

        }
    }
}
