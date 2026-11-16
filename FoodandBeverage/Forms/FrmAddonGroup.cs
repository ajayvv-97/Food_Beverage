using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmAddonGroup : Form
    {
        #region Grid column indexes
        // Available products grid
        private const int iproduct_slno = 0;
        private const int iproduct_name = 1;
        private const int iproduct_itemcode = 2;
        private const int iproduct_price = 3;
        private const int iproduct_category = 4;
        private const int iproduct_id = 5;

        // Selected products grid
        private const int iselected_slno = 0;
        private const int iselected_name = 1;
        private const int iselected_itemcode = 2;
        private const int iselected_price = 3;
        private const int iselected_displayorder = 4;
        private const int iselected_productid = 5;
        private const int iselected_groupid = 6;
        private const int iselected_remove = 7;
        #endregion

        private readonly BllAddonGroup _addonGroupBll = new BllAddonGroup();
        private readonly BllSearch<DmnAddonGroup> _groupSearch = new BllSearch<DmnAddonGroup>();
        private readonly BllSearch<DmnAddonGroupProduct> _productSearch = new BllSearch<DmnAddonGroupProduct>();
        private int _selectedGroupId;
        private bool _loading;

        public FrmAddonGroup()
        {
            InitializeComponent();
            txtsearch_groups.TextChanged += txtsearch_groups_TextChanged;
            txtsearch_products.TextChanged += txtsearch_products_TextChanged;
            dgv_products.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_products.MultiSelect = true;
            dgv_addongroup_products.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_addongroup_products.MultiSelect = false;
        }

        private void FrmAddonGroup_Load(object sender, EventArgs e)
        {
            try
            {
                _loading = true;
                if (cmb_categtype.Items.Count > 0 && cmb_categtype.SelectedIndex < 0)
                    cmb_categtype.SelectedIndex = 0;

                chk_req_selection.Visible = true;
                if (num_maxselection.Value < 1)
                    num_maxselection.Value = 1;

                ConfigureSelectedProductsGrid();
                _loading = false;
                LoadGroups();
                LoadAvailableProducts();
                SetSaveInfo("Create a new add-on group or select an existing group.", false);
            }
            catch (Exception ex)
            {
                _loading = false;
                SetSaveInfo(ex.Message, true);
                MessageBox.Show(ex.Message, "Add-on Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureSelectedProductsGrid()
        {
            if (dgv_addongroup_products.Columns.Count <= iselected_remove)
            {
                DataGridViewButtonColumn removeColumn = new DataGridViewButtonColumn();
                removeColumn.Name = "colRemoveAddonProduct";
                removeColumn.HeaderText = "Action";
                removeColumn.Text = "Remove";
                removeColumn.UseColumnTextForButtonValue = true;
                removeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv_addongroup_products.Columns.Add(removeColumn);
            }

            dgv_addongroup_products.Columns[iselected_productid].Visible = false;
            dgv_addongroup_products.Columns[iselected_groupid].Visible = false;
            dgv_addongroup_products.Columns[iselected_remove].ReadOnly = true;
        }

        private int CurrentBranchId
        {
            get { return FrmDashboard.BranchId; }
        }

        private int CurrentEmployeeId
        {
            get { return FrmDashboard.UserId; }
        }

        private void LoadGroups()
        {
            DmnSearch search = new DmnSearch();
            search.Select = @"g.GroupId, g.GroupName, ISNULL(g.Description,'') AS Description,
                g.SelectionMode, g.MinSelection, g.MaxSelection, g.IsRequired, g.Status, g.BranchId,
                (SELECT COUNT(1) FROM dbo.tblAddonGroupDetail d WHERE d.GroupId=g.GroupId AND d.Status=1) AS ProductCount";
            search.Table = "dbo.tblAddonGroup g";
            search.WhereNdGroupBy = "WHERE g.BranchId=" + CurrentBranchId +
                " AND ('" + txtsearch_groups.Text.Trim().Replace("'", "''") + "'='' OR g.GroupName LIKE '%" +
                txtsearch_groups.Text.Trim().Replace("'", "''") + "%')";
            search.OrderBy = "ORDER BY g.GroupName";
            List<DmnAddonGroup> groups = _groupSearch.GetSearchData("prdComboFill", search);
            flp_groups.SuspendLayout();
            try
            {
                flp_groups.Controls.Clear();
                foreach (DmnAddonGroup group in groups)
                {
                    Button groupButton = new Button();
                    groupButton.Width = Math.Max(150, flp_groups.ClientSize.Width - 12);
                    groupButton.Height = 58;
                    groupButton.Margin = new Padding(3, 3, 3, 5);
                    groupButton.TextAlign = ContentAlignment.MiddleLeft;
                    groupButton.FlatStyle = FlatStyle.Flat;
                    groupButton.FlatAppearance.BorderColor = Color.FromArgb(150, 120, 210);
                    groupButton.BackColor = group.GroupId == _selectedGroupId
                        ? Color.FromArgb(215, 194, 247)
                        : Color.FromArgb(246, 241, 252);
                    groupButton.ForeColor = Color.FromArgb(48, 30, 95);
                    groupButton.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Bold);
                    groupButton.Text = group.GroupName + Environment.NewLine +
                        group.ProductCount + " products  |  " +
                        (group.SelectionMode == 1 ? "Single Select" : "Multiple Select") +
                        (group.Status ? "  |  Active" : "  |  Inactive");
                    groupButton.Tag = group.GroupId;
                    groupButton.Click += GroupButton_Click;
                    flp_groups.Controls.Add(groupButton);
                }

                lbl_groups_info.Text = "Total Groups: " + groups.Count;
            }
            finally
            {
                flp_groups.ResumeLayout();
            }
        }

        private void GroupButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null || button.Tag == null)
                return;

            try
            {
                LoadGroup(Convert.ToInt32(button.Tag));
                LoadGroups();
            }
            catch (Exception ex)
            {
                SetSaveInfo(ex.Message, true);
                MessageBox.Show(ex.Message, "Add-on Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGroup(int groupId)
        {
            DmnSearch search = new DmnSearch();
            search.Select = "GroupId, GroupName, ISNULL(Description,'') AS Description, SelectionMode, MinSelection, MaxSelection, IsRequired, Status, BranchId";
            search.Table = "dbo.tblAddonGroup";
            search.WhereNdGroupBy = "WHERE GroupId=" + groupId + " AND BranchId=" + CurrentBranchId;
            search.OrderBy = "";
            DmnAddonGroup group = _groupSearch.GetSearchData("prdComboFill", search).FirstOrDefault();
            if (group == null)
            {
                MessageBox.Show("Selected group was not found.", "Add-on Group",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _loading = true;
            try
            {
                _selectedGroupId = group.GroupId;
                txtgroupname.Text = group.GroupName;
                txtgroupdesc.Text = group.Description ?? string.Empty;
                chk_status.Checked = group.Status;
                rd_singleselect.Checked = group.SelectionMode == 1;
                rb_multiselect.Checked = group.SelectionMode == 2;
                num_miniselection.Value = Math.Min(num_miniselection.Maximum,
                    Math.Max(num_miniselection.Minimum, group.MinSelection));
                num_maxselection.Value = Math.Min(num_maxselection.Maximum,
                    Math.Max(num_maxselection.Minimum, group.MaxSelection));
                chk_req_selection.Checked = group.IsRequired;

                dgv_addongroup_products.Rows.Clear();
                DmnSearch productSearch = new DmnSearch();
                productSearch.Select = @"p.ProductId, p.Product AS ProductName, ISNULL(p.ItemCode,'') AS ItemCode,
                    ISNULL(p.Price,0) AS Price, ISNULL(c.CategoryName,'') AS CategoryName,
                    d.DisplayOrder, d.GroupId AS AddonGroupID";
                productSearch.Table = "dbo.tblAddonGroupDetail d INNER JOIN dbo.tblAddonGroup g ON g.GroupId=d.GroupId INNER JOIN dbo.tblProduct p ON p.ProductId=d.ProductId LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid=p.Fcid";
                productSearch.WhereNdGroupBy = "WHERE d.GroupId=" + group.GroupId + " AND g.BranchId=" + CurrentBranchId + " AND d.Status=1 AND p.Status=1";
                productSearch.OrderBy = "ORDER BY d.DisplayOrder, p.Product";
                List<DmnAddonGroupProduct> products = new BllSearch<DmnAddonGroupProduct>().GetSearchData("prdComboFill", productSearch);

                foreach (DmnAddonGroupProduct product in products)
                    AddSelectedProduct(product, false);

                SetSaveInfo("Editing group: " + group.GroupName, false);
            }
            finally
            {
                _loading = false;
            }
        }

        private void LoadAvailableProducts()
        {
            if (_loading)
                return;

            int categoryType = cmb_categtype.SelectedIndex;
            string searchText = txtsearch_products.Text.Trim().Replace("'", "''");
            DmnSearch productSearch = new DmnSearch();
            productSearch.Select = @"p.ProductId, p.Product AS ProductName, ISNULL(p.ItemCode,'') AS ItemCode,
                ISNULL(p.Price,0) AS Price, ISNULL(c.CategoryName,'') AS CategoryName";
            productSearch.Table = "dbo.tblProduct p LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid=p.Fcid";
            productSearch.WhereNdGroupBy = "WHERE p.BranchId=" + CurrentBranchId + " AND p.Status=1 AND ISNULL(c.CategoryType,0)=" +
                categoryType + " AND ('" + searchText + "'='' OR p.Product LIKE '%" + searchText +
                "%' OR ISNULL(p.ItemCode,'') LIKE '%" + searchText + "%')";
            productSearch.OrderBy = "ORDER BY p.Product";
            List<DmnAddonGroupProduct> products = _productSearch.GetSearchData("prdComboFill", productSearch);

            dgv_products.Rows.Clear();
            int rowNumber = 1;
            foreach (DmnAddonGroupProduct product in products)
            {
                dgv_products.Rows.Add(rowNumber++, product.ProductName, product.ItemCode,
                    product.Price, product.CategoryName, product.ProductId);
            }
        }

        private void AddSelectedProduct(DmnAddonGroupProduct product, bool showMessage)
        {
            if (product == null || product.ProductId <= 0)
                return;

            foreach (DataGridViewRow row in dgv_addongroup_products.Rows)
            {
                if (Convert.ToInt32(row.Cells[iselected_productid].Value) == product.ProductId)
                {
                    if (showMessage)
                        SetSaveInfo("This product is already included in the group.", true);
                    return;
                }
            }

            int displayOrder = dgv_addongroup_products.Rows.Count + 1;
            int rowIndex = dgv_addongroup_products.Rows.Add(
                displayOrder,
                product.ProductName,
                product.ItemCode,
                product.Price,
                displayOrder,
                product.ProductId,
                _selectedGroupId);

            dgv_addongroup_products.Rows[rowIndex].Cells[iselected_displayorder].Value = displayOrder;
            if (showMessage)
                SetSaveInfo(product.ProductName + " added to this group.", false);
        }

        private DmnAddonGroupProduct GetProductFromAvailableRow(DataGridViewRow row)
        {
            if (row == null || row.IsNewRow)
                return null;

            return new DmnAddonGroupProduct
            {
                ProductId = Convert.ToInt32(row.Cells[iproduct_id].Value),
                ProductName = Convert.ToString(row.Cells[iproduct_name].Value),
                ItemCode = Convert.ToString(row.Cells[iproduct_itemcode].Value),
                Price = Convert.ToDecimal(row.Cells[iproduct_price].Value),
                CategoryName = Convert.ToString(row.Cells[iproduct_category].Value)
            };
        }

        private List<DmnAddonGroupProduct> CollectSelectedProducts()
        {
            List<DmnAddonGroupProduct> products = new List<DmnAddonGroupProduct>();
            int displayOrder = 1;

            foreach (DataGridViewRow row in dgv_addongroup_products.Rows)
            {
                if (row.IsNewRow)
                    continue;

                products.Add(new DmnAddonGroupProduct
                {
                    ProductId = Convert.ToInt32(row.Cells[iselected_productid].Value),
                    ProductName = Convert.ToString(row.Cells[iselected_name].Value),
                    ItemCode = Convert.ToString(row.Cells[iselected_itemcode].Value),
                    Price = Convert.ToDecimal(row.Cells[iselected_price].Value),
                    DisplayOrder = Convert.ToInt32(row.Cells[iselected_displayorder].Value),
                    AddonGroupID = _selectedGroupId
                });
                displayOrder++;
            }

            return products;
        }

        private bool ValidateGroup()
        {
            if (string.IsNullOrWhiteSpace(txtgroupname.Text))
            {
                MessageBox.Show("Enter the add-on group name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtgroupname.Focus();
                return false;
            }

            if (dgv_addongroup_products.Rows.Count == 0)
            {
                MessageBox.Show("Select at least one product for this group.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int minimum = Convert.ToInt32(num_miniselection.Value);
            int maximum = Convert.ToInt32(num_maxselection.Value);
            int selectionMode = rd_singleselect.Checked ? 1 : 2;

            if (selectionMode == 1 && maximum != 1)
            {
                MessageBox.Show("Single Select groups must have a maximum selection of 1.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_maxselection.Value = 1;
                return false;
            }

            if (chk_req_selection.Checked && minimum < 1)
            {
                MessageBox.Show("Required Selection must have a minimum selection of at least 1.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                num_miniselection.Value = 1;
                return false;
            }

            if (minimum > maximum || maximum > dgv_addongroup_products.Rows.Count)
            {
                MessageBox.Show("Check the minimum and maximum selections. Maximum cannot exceed the products in this group.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (selectionMode == 1 && dgv_addongroup_products.Rows.Count < 1)
                return false;

            return true;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            SaveGroup();
        }

        private void SaveGroup()
        {
            if (!ValidateGroup())
                return;

            try
            {
                DmnAddonGroup group = new DmnAddonGroup
                {
                    GroupId = _selectedGroupId,
                    GroupName = txtgroupname.Text.Trim(),
                    Description = txtgroupdesc.Text.Trim(),
                    SelectionMode = rd_singleselect.Checked ? 1 : 2,
                    MinSelection = Convert.ToInt32(num_miniselection.Value),
                    MaxSelection = Convert.ToInt32(num_maxselection.Value),
                    IsRequired = chk_req_selection.Checked,
                    Status = chk_status.Checked,
                    BranchId = CurrentBranchId,
                    CreatedBy = CurrentEmployeeId,
                    ModifiedBy = CurrentEmployeeId
                };

                XElement productsXml = new XElement("Products",
                    CollectSelectedProducts().Select(p => new XElement("Product",
                        new XAttribute("ProductId", p.ProductId),
                        new XAttribute("DisplayOrder", p.DisplayOrder))));
                group.ProductsXml = productsXml.ToString(SaveOptions.DisableFormatting);
                DmnAddonGroup savedGroup = _addonGroupBll.AddEditData("prdInsert_upd_AddonGroup", group);
                if (savedGroup == null || savedGroup.GroupId <= 0)
                    throw new InvalidOperationException("Add-on group could not be saved.");
                int savedGroupId = savedGroup.GroupId;
                _selectedGroupId = savedGroupId;
                LoadGroups();
                LoadGroup(savedGroupId);
                SetSaveInfo("Add-on group saved successfully.", false);
                MessageBox.Show("Add-on group saved successfully.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SetSaveInfo(ex.Message, true);
                MessageBox.Show(ex.Message, "Add-on Group",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetSaveInfo(string message, bool isError)
        {
            lbl_saveinfo_group.Text = message;
            lbl_saveinfo_group.ForeColor = isError ? Color.Firebrick : Color.DarkGreen;
        }

        private void ClearGroup()
        {
            _selectedGroupId = 0;
            _loading = true;
            try
            {
                txtgroupname.Clear();
                txtgroupdesc.Clear();
                chk_status.Checked = true;
                rd_singleselect.Checked = true;
                rb_multiselect.Checked = false;
                num_miniselection.Value = 0;
                num_maxselection.Value = 1;
                chk_req_selection.Checked = false;
                dgv_addongroup_products.Rows.Clear();
                LoadGroups();
                SetSaveInfo("New add-on group. Select products and save.", false);
            }
            finally
            {
                _loading = false;
            }
        }

        private void btn_addgroup_Click(object sender, EventArgs e)
        {
            ClearGroup();
            txtgroupname.Focus();
        }

        private void btn_addproductsgroup_Click(object sender, EventArgs e)
        {
            if (dgv_products.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgv_products.SelectedRows
                    .Cast<DataGridViewRow>().OrderBy(r => r.Index).ToList())
                    AddSelectedProduct(GetProductFromAvailableRow(row), true);
            }
            else if (dgv_products.CurrentRow != null)
            {
                AddSelectedProduct(GetProductFromAvailableRow(dgv_products.CurrentRow), true);
            }
        }

        private void dgv_products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_products.Rows.Count)
                return;

            // Clicking a product adds it directly. The Add button also supports
            // adding multiple selected rows together.
            AddSelectedProduct(GetProductFromAvailableRow(dgv_products.Rows[e.RowIndex]), true);
        }

        private void dgv_addongroup_products_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != iselected_remove)
                return;

            dgv_addongroup_products.Rows.RemoveAt(e.RowIndex);
            for (int i = 0; i < dgv_addongroup_products.Rows.Count; i++)
            {
                dgv_addongroup_products.Rows[i].Cells[iselected_slno].Value = i + 1;
                dgv_addongroup_products.Rows[i].Cells[iselected_displayorder].Value = i + 1;
            }
            SetSaveInfo("Product removed from this group. Save to apply changes.", false);
        }

        private void txtsearch_groups_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
                LoadGroups();
        }

        private void txtsearch_products_TextChanged(object sender, EventArgs e)
        {
            if (!_loading)
                LoadAvailableProducts();
        }

        private void cmb_categtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadAvailableProducts();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            _loading = true;
            try
            {
                txtsearch_groups.Clear();
                txtsearch_products.Clear();
                if (cmb_categtype.Items.Count > 0)
                    cmb_categtype.SelectedIndex = 0;
            }
            finally
            {
                _loading = false;
            }
            ClearGroup();
            LoadAvailableProducts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGroups();
            LoadAvailableProducts();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmAddonGroup_Resize(object sender, EventArgs e)
        {
            foreach (Control control in flp_groups.Controls)
                control.Width = Math.Max(150, flp_groups.ClientSize.Width - 12);
        }
    }
}
