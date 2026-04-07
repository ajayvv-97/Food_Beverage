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
        public int OrderStatustype = -1;
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
                "S.SalesMasterId AS Id, " +
                "S.EntryNo AS Field1, " +
                "S.EntryDate AS Field2, " +
                "S.NetAmount AS Field3";

                _objSearch.Table =
                    "tblsalesmaster S " +
                    "LEFT JOIN tblSalesDetails SD ON S.SalesMasterId = SD.SalesMasterId " +
                    "LEFT JOIN tblOrderMaster om ON om.SalesMasterId = S.SalesMasterId";

                _objSearch.WhereNdGroupBy =
                    "WHERE S.Status = 1 " +// "AND S.Tid = 0 " +     "AND om.Status = 1 " +
                    "AND S.saletype in(1,2) " +
                    "GROUP BY S.SalesMasterId, S.EntryNo, S.EntryDate, S.NetAmount";

                _objSearch.OrderBy =
                    "ORDER BY Id DESC";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty",MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        objSearch.Select =
                            "SD.SalesMasterId AS ID, " +
                            "SD.NetAmount, " +
                            "SD.Quantity, " +
                            "SD.ItemName, " +
                            "CASE " +
                            "   WHEN SUM(CASE WHEN od.Status = 2 THEN 1 ELSE 0 END) > 0 THEN 2 " + // Cooking Started
                            "   WHEN SUM(CASE WHEN od.Status = 1 THEN 1 ELSE 0 END) > 0 THEN 1 " + // Not Started
                            "   WHEN COUNT(od.OrderDetailsId) = SUM(CASE WHEN od.Status = 3 THEN 1 ELSE 0 END) THEN 3 " + // Completed
                            "   WHEN COUNT(od.OrderDetailsId) = SUM(CASE WHEN od.Status = 0 THEN 1 ELSE 0 END) THEN 0 " + // Cancelled
                            "   ELSE -1 " +
                            "END AS OrderStatus";

                        objSearch.Table =
                            "tblSalesDetails SD " +
                            "LEFT JOIN tblOrderMaster om ON om.SalesMasterId = SD.SalesMasterId " +
                            "LEFT JOIN tblOrderDetails od ON om.OrderMasterId = od.OrderMasterId";

                        objSearch.WhereNdGroupBy =
                            "WHERE SD.Status = 1 " +
                            "AND SD.SalesMasterId = " + aid + " " +
                            "GROUP BY SD.SalesMasterId, SD.NetAmount, SD.Quantity, SD.ItemName";

                        objSearch.OrderBy = "";

                        var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);

                        int SL_Main = 0;
                        if (itemSource.Count > 0)
                        {
                            int OrderStatustype = itemSource[0].OrderStatus;
                            lblorderstatus.Visible = true;
                            switch (OrderStatustype)
                            {
                                case 1:
                                    lblorderstatus.Text = "Cooking Not Started";
                                    lblorderstatus.ForeColor = Color.Gray;
                                    break;

                                case 2:
                                    lblorderstatus.Text = "Cooking Started";
                                    lblorderstatus.ForeColor = Color.Orange;
                                    break;

                                case 3:
                                    lblorderstatus.Text = "Completed / Served";
                                    lblorderstatus.ForeColor = Color.Green;
                                    break;

                                case 0:
                                    lblorderstatus.Text = "Cancelled";
                                    lblorderstatus.ForeColor = Color.Red;
                                    break;

                                default:
                                    lblorderstatus.Text = "Partial / Mixed";
                                    lblorderstatus.ForeColor = Color.Blue;
                                    break;
                            }

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
                        else
                            lblorderstatus.Visible = false;
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
                DialogResult result = CMessageBox.Show("Do you want to void this sale?","Void Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    BllProducts bllfav = new BllProducts();
                    DmnGridFields dmn = new DmnGridFields();

                    dmn.Field1 = Convert.ToString(dgv_productlist.Rows[0].Cells[sid].Value);

                    bllfav.AddEditData_voidsale("prd_VoidSale", dmn);
                    CMessageBox.Show("Void Sale Successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv_productlist.Rows.Clear();
                    lblorderstatus.Visible = false;
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
            lblorderstatus.Visible = false;
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
                "S.EntryNo as Field1," +
                "S.EntryDate as Field2," +
                "S.NetAmount as Field3";

                _objSearch.Table =
                "tblsalesmaster S LEFT JOIN tblSalesDetails SD " +
                "ON S.SalesMasterId = SD.SalesMasterId"+
                " LEFT JOIN tblOrderMaster Om ON S.SalesMasterId=Om.SalesMasterId";

                _objSearch.WhereNdGroupBy =
                "WHERE S.Status = 1 AND S.Tid = 0 AND Om.Status=1 " +
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
                        objSearch.WhereNdGroupBy = "where status = 1 and SalesMasterId=" + aid;
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
