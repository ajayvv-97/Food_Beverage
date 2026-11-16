using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class BllAddonGroup
    {
        public List<DmnAddonGroup> GetGroups(int branchId, string searchText)
        {
            const string sql = @"
SELECT g.GroupId, g.GroupName, g.Description, g.SelectionMode,
       g.MinSelection, g.MaxSelection, g.IsRequired, g.Status, g.BranchId,
       COUNT(d.GroupDetailId) AS ProductCount
FROM dbo.tblAddonGroup g
LEFT JOIN dbo.tblAddonGroupDetail d
       ON d.GroupId = g.GroupId AND d.Status = 1
WHERE g.BranchId = @BranchId
  AND (@SearchText = '' OR g.GroupName LIKE '%' + @SearchText + '%')
GROUP BY g.GroupId, g.GroupName, g.Description, g.SelectionMode,
         g.MinSelection, g.MaxSelection, g.IsRequired, g.Status, g.BranchId
ORDER BY g.GroupName;";
            using (IDbConnection connection = DAL.DapperRepository<DmnAddonGroup>.OpenConnection())
                return connection.Query<DmnAddonGroup>(sql, new { BranchId = branchId, SearchText = searchText ?? string.Empty }).ToList();
        }

        public DmnAddonGroup GetGroup(int groupId, int branchId)
        {
            const string sql = @"
SELECT GroupId, GroupName, Description, SelectionMode, MinSelection,
       MaxSelection, IsRequired, Status, BranchId
FROM dbo.tblAddonGroup
WHERE GroupId = @GroupId AND BranchId = @BranchId;";
            using (IDbConnection connection = DAL.DapperRepository<DmnAddonGroup>.OpenConnection())
                return connection.QueryFirstOrDefault<DmnAddonGroup>(sql, new { GroupId = groupId, BranchId = branchId });
        }

        public List<DmnAddonGroupProduct> SearchProducts(int branchId, int categoryType, string searchText)
        {
            const string sql = @"
SELECT p.ProductId, p.Product AS ProductName, ISNULL(p.ItemCode, '') AS ItemCode,
       ISNULL(p.Price, 0) AS Price, ISNULL(c.CategoryName, '') AS CategoryName
FROM dbo.tblProduct p
LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid = p.Fcid
WHERE p.BranchId = @BranchId
  AND p.Status = 1
  AND (@CategoryType < 0 OR ISNULL(c.CategoryType, 0) = @CategoryType)
  AND (@SearchText = '' OR p.Product LIKE '%' + @SearchText + '%'
       OR ISNULL(p.ItemCode, '') LIKE '%' + @SearchText + '%')
ORDER BY p.Product;";
            using (IDbConnection connection = DAL.DapperRepository<DmnAddonGroupProduct>.OpenConnection())
                return connection.Query<DmnAddonGroupProduct>(sql, new
                {
                    BranchId = branchId,
                    CategoryType = categoryType,
                    SearchText = searchText ?? string.Empty
                }).ToList();
        }

        public List<DmnAddonGroupProduct> GetGroupProducts(int groupId, int branchId)
        {
            const string sql = @"
SELECT p.ProductId, p.Product AS ProductName, ISNULL(p.ItemCode, '') AS ItemCode,
       ISNULL(p.Price, 0) AS Price, ISNULL(c.CategoryName, '') AS CategoryName,
       d.DisplayOrder, d.GroupId AS AddonGroupID
FROM dbo.tblAddonGroupDetail d
INNER JOIN dbo.tblAddonGroup g ON g.GroupId = d.GroupId
INNER JOIN dbo.tblProduct p ON p.ProductId = d.ProductId
LEFT JOIN dbo.tblCategory_FTI c ON c.Fcid = p.Fcid
WHERE d.GroupId = @GroupId AND g.BranchId = @BranchId
  AND d.Status = 1 AND p.Status = 1
ORDER BY d.DisplayOrder, p.Product;";
            using (IDbConnection connection = DAL.DapperRepository<DmnAddonGroupProduct>.OpenConnection())
                return connection.Query<DmnAddonGroupProduct>(sql, new { GroupId = groupId, BranchId = branchId }).ToList();
        }

        public int SaveGroup(DmnAddonGroup group, IList<DmnAddonGroupProduct> products)
        {
            if (group == null)
                throw new ArgumentNullException("group");
            if (products == null || products.Count == 0)
                throw new InvalidOperationException("Select at least one product for this group.");

            using (IDbConnection connection = DAL.DapperRepository<DmnAddonGroup>.OpenConnection())
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int groupId = group.GroupId;
                    if (groupId <= 0)
                    {
                        const string insertGroup = @"
INSERT INTO dbo.tblAddonGroup
    (GroupName, Description, SelectionMode, MinSelection, MaxSelection,
     IsRequired, Status, BranchId, CreatedBy, CreatedDateTime)
OUTPUT INSERTED.GroupId
VALUES
    (@GroupName, @Description, @SelectionMode, @MinSelection, @MaxSelection,
     @IsRequired, @Status, @BranchId, @CreatedBy, GETDATE());";
                        groupId = connection.Query<int>(insertGroup, group, transaction).Single();
                    }
                    else
                    {
                        const string updateGroup = @"
UPDATE dbo.tblAddonGroup
SET GroupName = @GroupName,
    Description = @Description,
    SelectionMode = @SelectionMode,
    MinSelection = @MinSelection,
    MaxSelection = @MaxSelection,
    IsRequired = @IsRequired,
    Status = @Status,
    ModifiedBy = @ModifiedBy,
    ModifiedDateTime = GETDATE()
WHERE GroupId = @GroupId AND BranchId = @BranchId;";
                        int affected = connection.Execute(updateGroup, new
                        {
                            group.GroupName, group.Description, group.SelectionMode,
                            group.MinSelection, group.MaxSelection, group.IsRequired,
                            group.Status, group.ModifiedBy, GroupId = groupId, group.BranchId
                        }, transaction);
                        if (affected == 0)
                            throw new InvalidOperationException("The selected add-on group was not found for this branch.");
                    }

                    connection.Execute(
                        "DELETE FROM dbo.tblAddonGroupDetail WHERE GroupId = @GroupId;",
                        new { GroupId = groupId }, transaction);

                    const string insertDetail = @"
INSERT INTO dbo.tblAddonGroupDetail
    (GroupId, ProductId, DisplayOrder, Status, CreatedBy, CreatedDateTime)
VALUES
    (@GroupId, @ProductId, @DisplayOrder, 1, @CreatedBy, GETDATE());";

                    int displayOrder = 1;
                    foreach (DmnAddonGroupProduct product in products)
                    {
                        connection.Execute(insertDetail, new
                        {
                            GroupId = groupId,
                            ProductId = product.ProductId,
                            DisplayOrder = displayOrder++,
                            CreatedBy = group.ModifiedBy > 0 ? group.ModifiedBy : group.CreatedBy
                        }, transaction);
                    }

                    transaction.Commit();
                    return groupId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
