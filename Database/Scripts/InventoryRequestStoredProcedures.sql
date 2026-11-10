USE [FoodBeverage];
GO

/*
    QOH remains in Purchase Units. The approval procedure converts QOH to
    Base Units for consumption calculations, then saves the remainder back
    in Purchase Units. QOH must support fractional purchase quantities.
*/
IF OBJECT_ID(N'dbo.tblProductRate', N'U') IS NOT NULL
   AND COL_LENGTH(N'dbo.tblProductRate', N'QOH') IS NOT NULL
   AND EXISTS
   (
       SELECT 1
       FROM sys.columns
       WHERE object_id = OBJECT_ID(N'dbo.tblProductRate')
         AND name = N'QOH'
         AND
         (
             system_type_id NOT IN (106, 108)
             OR precision <> 18
             OR scale <> 4
         )
   )
BEGIN
    ALTER TABLE dbo.tblProductRate
        ALTER COLUMN QOH DECIMAL(18,4) NULL;
END;
GO

/* Ensure approval audit columns exist on databases created from earlier request scripts. */
IF OBJECT_ID(N'dbo.tblInventoryRequestMaster', N'U') IS NULL
    THROW 50200, 'tblInventoryRequestMaster must be created before installing the approval workflow.', 1;
GO

IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovalStatus') IS NULL
BEGIN
    ALTER TABLE dbo.tblInventoryRequestMaster
        ADD ApprovalStatus INT NOT NULL
            CONSTRAINT DF_tblInventoryRequestMaster_ApprovalStatus DEFAULT (0) WITH VALUES;
END;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovedBy') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovedBy INT NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovedOn') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovedOn DATETIME NULL;
GO
IF COL_LENGTH(N'dbo.tblInventoryRequestMaster', N'ApprovalRemarks') IS NULL
    ALTER TABLE dbo.tblInventoryRequestMaster ADD ApprovalRemarks VARCHAR(MAX) NULL;
GO

/* One shared table-valued parameter for main requests and saved drafts. */
IF TYPE_ID(N'dbo.TVP_InventoryRequestDetail') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.TVP_InventoryRequestDetail AS TABLE
        (
            RequestType INT NOT NULL,
            SlNo INT NULL,
            ProductId INT NULL,
            PlannedQty DECIMAL(18,4) NULL,
            RecipeItemId BIGINT NULL,
            PerPortionQty DECIMAL(18,4) NULL,
            TotalRequiredQty DECIMAL(18,4) NULL,
            ItemId BIGINT NULL,
            ProductType INT NULL,
            CurrentStock DECIMAL(18,4) NULL,
            IssuedQty DECIMAL(18,4) NULL,
            ProjectedBalance DECIMAL(18,4) NULL,
            Unit INT NULL,
            PurposeRemarks VARCHAR(MAX) NULL
        );');
END;
GO

/* Save the final inventory request master and all its details atomically.
   When @DraftId is supplied, the source draft is also marked inactive. */
CREATE OR ALTER PROCEDURE dbo.prdSave_InventoryRequest
    @InventoryRequestId INT,
    @RequestNo VARCHAR(40),
    @RequestDate DATETIME,
    @RequiredDate DATETIME,
    @DepartmentId INT = NULL,
    @EmployeeId INT = NULL,
    @Remarks VARCHAR(MAX) = NULL,
    @RequestType INT,
    @CreatedBy INT,
    @ModifiedBy INT = NULL,
    @BranchId INT,
    @Status INT = 1,
    @DraftId INT = NULL,
    @Details dbo.TVP_InventoryRequestDetail READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedRequestId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@InventoryRequestId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestMaster
            SET RequestNo = @RequestNo,
                RequestDate = @RequestDate,
                RequiredDate = @RequiredDate,
                DepartmentId = @DepartmentId,
                EmployeeId = @EmployeeId,
                Remarks = @Remarks,
                RequestType = @RequestType,
                ModifiedBy = ISNULL(@ModifiedBy, @CreatedBy),
                ModifiedOn = GETDATE(),
                BranchId = @BranchId,
                Status = @Status
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId;

            IF @@ROWCOUNT = 0
                THROW 50101, 'Inventory request was not found.', 1;

            SET @SavedRequestId = @InventoryRequestId;

            UPDATE dbo.tblInventoryRequestDetails
            SET Status = 0,
                ModifiedBy = @CreatedBy,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @SavedRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblInventoryRequestMaster
            (
                RequestNo, RequestDate, RequiredDate, DepartmentId,
                EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @RequestNo, @RequestDate, @RequiredDate, @DepartmentId,
                @EmployeeId, @Remarks, @RequestType, @CreatedBy, GETDATE(),
                @BranchId, @Status
            );

            SET @SavedRequestId = CONVERT(INT, SCOPE_IDENTITY());
        END;

        INSERT INTO dbo.tblInventoryRequestDetails
        (
            InventoryRequestId, RequestType, SlNo, ProductId, PlannedQty,
            RecipeItemId, PerPortionQty, TotalRequiredQty, ItemId, ProductType,
            CurrentStock, IssuedQty, ProjectedBalance, Unit, PurposeRemarks,
            CreatedBy, CreatedOn, BranchId, Status
        )
        SELECT
            @SavedRequestId, d.RequestType, d.SlNo, d.ProductId, d.PlannedQty,
            d.RecipeItemId, d.PerPortionQty, d.TotalRequiredQty, d.ItemId, d.ProductType,
            d.CurrentStock, d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
            @CreatedBy, GETDATE(), @BranchId, 1
        FROM @Details d;

        IF @DraftId IS NOT NULL AND @DraftId > 0
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1
                FROM dbo.tblInventoryRequestDraftMaster
                WHERE DraftId = @DraftId
                  AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50102, 'The source draft was not found or is inactive.', 1;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId AND ISNULL(Status, 1) = 1;

            UPDATE dbo.tblInventoryRequestDraftMaster
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;
        END;

        COMMIT TRANSACTION;
        SELECT @SavedRequestId AS InventoryRequestId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Save or update one draft and replace its active detail snapshot atomically. */
CREATE OR ALTER PROCEDURE dbo.prdSave_InventoryRequestDraft
    @DraftId INT,
    @DraftNo VARCHAR(40) = NULL,
    @RequestNo VARCHAR(40) = NULL,
    @RequestDate DATETIME,
    @RequiredDate DATETIME,
    @DepartmentId INT = NULL,
    @EmployeeId INT = NULL,
    @Remarks VARCHAR(MAX) = NULL,
    @RequestType INT,
    @CreatedBy INT,
    @BranchId INT,
    @Status INT = 1,
    @Details dbo.TVP_InventoryRequestDetail READONLY
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedDraftId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@DraftId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestDraftMaster
            SET RequestNo = @RequestNo,
                RequestDate = @RequestDate,
                RequiredDate = @RequiredDate,
                DepartmentId = @DepartmentId,
                EmployeeId = @EmployeeId,
                Remarks = @Remarks,
                RequestType = @RequestType,
                ModifiedBy = @CreatedBy,
                ModifiedOn = GETDATE(),
                BranchId = @BranchId,
                Status = @Status
            WHERE DraftId = @DraftId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1;

            IF @@ROWCOUNT = 0
                THROW 50103, 'The selected draft was not found or is inactive.', 1;

            SET @SavedDraftId = @DraftId;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @CreatedBy, ModifiedOn = GETDATE()
            WHERE DraftId = @SavedDraftId AND ISNULL(Status, 1) = 1;
        END
        ELSE
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@DraftNo)), '') IS NULL
                SET @DraftNo = 'DR-' + CONVERT(VARCHAR(8), GETDATE(), 112)
                    + '-' + REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(12), GETDATE(), 114), ':', ''), '.', ''), ' ', '');

            INSERT INTO dbo.tblInventoryRequestDraftMaster
            (
                DraftNo, RequestNo, RequestDate, RequiredDate, DepartmentId,
                EmployeeId, Remarks, RequestType, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @DraftNo, @RequestNo, @RequestDate, @RequiredDate, @DepartmentId,
                @EmployeeId, @Remarks, @RequestType, @CreatedBy, GETDATE(),
                @BranchId, @Status
            );

            SET @SavedDraftId = CONVERT(INT, SCOPE_IDENTITY());
        END;

        INSERT INTO dbo.tblInventoryRequestDraftDetails
        (
            DraftId, RequestType, SlNo, ProductId, PlannedQty,
            RecipeItemId, PerPortionQty, TotalRequiredQty, ItemId, ProductType,
            CurrentStock, IssuedQty, ProjectedBalance, Unit, PurposeRemarks,
            CreatedBy, CreatedOn, BranchId, Status
        )
        SELECT
            @SavedDraftId, d.RequestType, d.SlNo, d.ProductId, d.PlannedQty,
            d.RecipeItemId, d.PerPortionQty, d.TotalRequiredQty, d.ItemId, d.ProductType,
            d.CurrentStock, d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks,
            @CreatedBy, GETDATE(), @BranchId, 1
        FROM @Details d;

        COMMIT TRANSACTION;
        SELECT @SavedDraftId AS DraftId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Soft-delete a complete saved draft. */
CREATE OR ALTER PROCEDURE dbo.prdDelete_InventoryRequestDraft
    @DraftId INT,
    @EmployeeId INT,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.tblInventoryRequestDraftMaster
            WHERE DraftId = @DraftId AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
        )
            THROW 50104, 'The selected draft was not found or is already inactive.', 1;

        UPDATE dbo.tblInventoryRequestDraftDetails
        SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
        WHERE DraftId = @DraftId AND ISNULL(Status, 1) = 1;

        UPDATE dbo.tblInventoryRequestDraftMaster
        SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
        WHERE DraftId = @DraftId AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/* Soft-delete one product group or one raw-material row from a draft/request.
   Draft rows require confirmation in the form; normal request rows do not. */
CREATE OR ALTER PROCEDURE dbo.prdDelete_InventoryRequestRow
    @IsDraft BIT,
    @DraftId INT = NULL,
    @InventoryRequestId INT = NULL,
    @RequestType INT,
    @ProductId INT = NULL,
    @ItemId BIGINT = NULL,
    @EmployeeId INT,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @RequestType = 1 AND ISNULL(@ProductId, 0) <= 0
            THROW 50105, 'Product ID is required to delete a batch product.', 1;

        IF @RequestType = 2 AND ISNULL(@ItemId, 0) <= 0
            THROW 50106, 'Item ID is required to delete a raw material.', 1;

        IF @IsDraft = 1
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1 FROM dbo.tblInventoryRequestDraftMaster
                WHERE DraftId = @DraftId AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50107, 'The draft is not available for editing.', 1;

            UPDATE dbo.tblInventoryRequestDraftDetails
            SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE DraftId = @DraftId
              AND RequestType = @RequestType
              AND ISNULL(Status, 1) = 1
              AND
              (
                  (@RequestType = 1 AND ProductId = @ProductId)
                  OR (@RequestType = 2 AND ItemId = @ItemId)
              );
        END
        ELSE
        BEGIN
            IF NOT EXISTS
            (
                SELECT 1 FROM dbo.tblInventoryRequestMaster
                WHERE InventoryRequestId = @InventoryRequestId
                  AND BranchId = @BranchId
                  AND ISNULL(Status, 1) = 1
            )
                THROW 50108, 'The inventory request is not available for editing.', 1;

            UPDATE dbo.tblInventoryRequestDetails
            SET Status = 0, ModifiedBy = @EmployeeId, ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND RequestType = @RequestType
              AND ISNULL(Status, 1) = 1
              AND
              (
                  (@RequestType = 1 AND ProductId = @ProductId)
                  OR (@RequestType = 2 AND ItemId = @ItemId)
              );
        END;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

/*
    Inventory request approval workflow:
      0 = Pending
      1 = Approved
      2 = Rejected
      3 = Sent Back

    Only Pending requests can receive a manager decision.
    Only Sent Back requests can be resubmitted by the requesting employee.
*/
CREATE OR ALTER PROCEDURE dbo.prdUpdate_InventoryRequestApproval
    @InventoryRequestId INT,
    @ApprovalStatus INT,
    @EmployeeId INT,
    @ApprovalRemarks VARCHAR(MAX) = NULL,
    @BranchId INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @CurrentApprovalStatus INT = NULL;

    DECLARE @StockToDeduct TABLE
    (
        ItemId BIGINT NOT NULL PRIMARY KEY,
        RequiredQty DECIMAL(18,4) NOT NULL,
        ConversionFactor DECIMAL(18,4) NOT NULL,
        ActiveRateCount INT NULL,
        AvailableBaseQty DECIMAL(18,4) NULL
    );

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@InventoryRequestId, 0) <= 0
            THROW 50204, 'A valid inventory request ID is required.', 1;

        IF ISNULL(@EmployeeId, 0) <= 0
            THROW 50205, 'A valid employee ID is required for this action.', 1;

        SELECT @CurrentApprovalStatus = ISNULL(ApprovalStatus, 0)
        FROM dbo.tblInventoryRequestMaster WITH (UPDLOCK, HOLDLOCK)
        WHERE InventoryRequestId = @InventoryRequestId
          AND BranchId = @BranchId
          AND ISNULL(Status, 1) = 1;

        IF @CurrentApprovalStatus IS NULL
            THROW 50207, 'The active inventory request was not found for this branch.', 1;

        /* Resubmit a request previously sent back. */
        IF @ApprovalStatus = 0
        BEGIN
            IF @CurrentApprovalStatus <> 3
                THROW 50201, 'Only an active Sent Back request can be resubmitted.', 1;

            UPDATE dbo.tblInventoryRequestMaster
            SET ApprovalStatus = 0,
                ApprovedBy = NULL,
                ApprovedOn = NULL,
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
              AND ApprovalStatus = 3;

            IF @@ROWCOUNT = 0
                THROW 50201, 'Only an active Sent Back request can be resubmitted.', 1;
        END
        ELSE IF @ApprovalStatus IN (1, 2, 3)
        BEGIN
            IF @CurrentApprovalStatus <> 0
                THROW 50202, 'Only an active Pending request can be approved, rejected, or sent back.', 1;

            IF @ApprovalStatus IN (2, 3)
               AND NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), '') IS NULL
                THROW 50206, 'Remarks are required when rejecting or sending back a request.', 1;

            IF @ApprovalStatus = 1
            BEGIN
                /* Validate product quantities. */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails d WITH (UPDLOCK, HOLDLOCK)
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 1
                      AND (ISNULL(d.ProductId, 0) <= 0 OR ISNULL(d.PlannedQty, 0) < 0)
                )
                    THROW 50208, 'A product request contains an invalid product or negative planned quantity.', 1;

                /*
                   FoodPreparationType must be configured before a product
                   request is approved. Unknown values must not silently skip
                   stock deduction.
                */
                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT ProductId
                        FROM dbo.tblInventoryRequestDetails
                        WHERE InventoryRequestId = @InventoryRequestId
                          AND ISNULL(Status, 1) = 1
                          AND RequestType = 1
                          AND ISNULL(PlannedQty, 0) > 0
                    ) requested
                    LEFT JOIN dbo.tblProduct p
                        ON p.ProductId = requested.ProductId
                    WHERE p.ProductId IS NULL
                       OR ISNULL(p.FoodPreparationType, 0) NOT IN (1, 2, 3, 4)
                )
                    THROW 50219, 'A requested product has an invalid or missing FoodPreparationType (1-4). Approval was cancelled.', 1;

                /*
                   Validate only recipe ingredients that are deducted at
                   batch approval:
                     - BatchPrepared (2): all raw-material rows.
                     - PartiallyPrepared (3): stage 2 rows only.
                   MadeToOrder (1), NotApplicable (4), and Packing (2)
                   ingredients are intentionally excluded.
                */
                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT ProductId, PlannedQty
                        FROM dbo.tblInventoryRequestDetails
                        WHERE InventoryRequestId = @InventoryRequestId
                          AND ISNULL(Status, 1) = 1
                          AND RequestType = 1
                          AND ISNULL(PlannedQty, 0) > 0
                    ) requested
                    INNER JOIN dbo.tblProduct p
                        ON p.ProductId = requested.ProductId
                    INNER JOIN dbo.tblIngredientDetails ing
                        ON ing.ProductId = requested.ProductId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                       AND
                       (
                           p.FoodPreparationType = 2
                           OR
                           (
                               p.FoodPreparationType = 3
                               AND ISNULL(ing.Deductionstage, 0) = 2
                           )
                       )
                    LEFT JOIN dbo.tblItem i
                        ON i.ItemId = ing.ItemId
                    WHERE ISNULL(ing.Quantity, 0) <= 0
                       OR i.ItemId IS NULL
                       OR i.Baseunit IS NULL
                       OR ISNULL(i.Baseunit, '') = ''
                )
                    THROW 50215, 'A batch-deduction recipe ingredient has an invalid quantity, ItemId, or Baseunit.', 1;

                /* PartiallyPrepared ingredients must use a known deduction stage. */
                IF EXISTS
                (
                    SELECT 1
                    FROM
                    (
                        SELECT DISTINCT ProductId
                        FROM dbo.tblInventoryRequestDetails
                        WHERE InventoryRequestId = @InventoryRequestId
                          AND ISNULL(Status, 1) = 1
                          AND RequestType = 1
                          AND ISNULL(PlannedQty, 0) > 0
                    ) requested
                    INNER JOIN dbo.tblProduct p
                        ON p.ProductId = requested.ProductId
                       AND p.FoodPreparationType = 3
                    INNER JOIN dbo.tblIngredientDetails ing
                        ON ing.ProductId = requested.ProductId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    WHERE ISNULL(ing.Deductionstage, 0) NOT IN (1, 2)
                )
                    THROW 50220, 'A PartiallyPrepared raw-material ingredient has an invalid Deductionstage. Use 1 (OrderTime) or 2 (BatchPreparation).', 1;

                /* Validate direct raw-material request quantities and units. */
                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails d WITH (UPDLOCK, HOLDLOCK)
                    LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) < 0
                )
                    THROW 50218, 'A direct raw-material issued quantity cannot be negative.', 1;

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails d WITH (UPDLOCK, HOLDLOCK)
                    LEFT JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) > 0
                      AND
                      (
                          i.ItemId IS NULL
                          OR d.Unit IS NULL
                          OR
                          (
                              d.Unit <> i.unit
                              AND (i.Baseunit IS NULL OR d.Unit <> i.Baseunit)
                          )
                      )
                )
                    THROW 50211, 'A direct raw-material request has an invalid unit. Use the purchase unit or Baseunit.', 1;

                IF EXISTS
                (
                    SELECT 1
                    FROM dbo.tblInventoryRequestDetails d
                    INNER JOIN dbo.tblItem i ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) > 0
                      AND d.Unit = i.unit
                      AND i.Baseunit IS NOT NULL
                      AND i.Baseunit <> i.unit
                      AND ISNULL(i.Conversionfactor, 0) <= 0
                )
                    THROW 50212, 'A direct raw material has an invalid Conversionfactor.', 1;

                /*
                   The form stores a RequestType=1 detail row for every recipe
                   ingredient, repeating ProductId and PlannedQty. Deduplicate
                   ProductId + PlannedQty before expanding the ingredient list.

                   Batch deduction matrix:
                     FoodPreparationType 2: all ProductType=1 ingredients.
                     FoodPreparationType 3: ProductType=1, Deductionstage 2 only.
                     FoodPreparationType 1 or 4: none at batch approval.
                     ProductType=2 (Packing): always excluded.
                */
                ;WITH ProductRequests AS
                (
                    SELECT ProductId, PlannedQty
                    FROM dbo.tblInventoryRequestDetails
                    WHERE InventoryRequestId = @InventoryRequestId
                      AND ISNULL(Status, 1) = 1
                      AND RequestType = 1
                      AND ISNULL(PlannedQty, 0) > 0
                    GROUP BY ProductId, PlannedQty
                ),
                RequiredRawMaterials AS
                (
                    /* Batch request: include only ingredients due at batch stage. */
                    SELECT
                        ing.ItemId,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            requested.PlannedQty * ing.Quantity *
                            CASE
                                WHEN ing.Unit = i.unit
                                 AND ISNULL(i.Baseunit, i.unit) <> i.unit
                                    THEN i.Conversionfactor
                                ELSE 1
                            END
                        ) AS RequiredBaseQty
                    FROM ProductRequests requested
                    INNER JOIN dbo.tblProduct product
                        ON product.ProductId = requested.ProductId
                    INNER JOIN dbo.tblIngredientDetails ing
                        ON ing.ProductId = requested.ProductId
                       AND ing.ProductType = 1
                       AND ISNULL(ing.Status, 1) = 1
                    INNER JOIN dbo.tblItem i
                        ON i.ItemId = ing.ItemId
                    WHERE
                        product.FoodPreparationType = 2
                        OR
                        (
                            product.FoodPreparationType = 3
                            AND ISNULL(ing.Deductionstage, 0) = 2
                        )

                    UNION ALL

                    /* Direct raw-material issue: always deduct on approval. */
                    SELECT
                        d.ItemId,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            d.IssuedQty *
                            CASE
                                WHEN d.Unit = i.unit
                                 AND ISNULL(i.Baseunit, i.unit) <> i.unit
                                    THEN i.Conversionfactor
                                ELSE 1
                            END
                        ) AS RequiredBaseQty
                    FROM dbo.tblInventoryRequestDetails d
                    INNER JOIN dbo.tblItem i
                        ON i.ItemId = d.ItemId
                    WHERE d.InventoryRequestId = @InventoryRequestId
                      AND ISNULL(d.Status, 1) = 1
                      AND d.RequestType = 2
                      AND ISNULL(d.IssuedQty, 0) > 0
                )
                INSERT INTO @StockToDeduct
                (
                    ItemId,
                    RequiredQty,
                    ConversionFactor
                )
                SELECT
                    r.ItemId,
                    CONVERT(DECIMAL(18,4), SUM(r.RequiredBaseQty)),
                    MAX
                    (
                        CASE
                            WHEN ISNULL(i.Baseunit, i.unit) = i.unit THEN 1
                            ELSE i.Conversionfactor
                        END
                    )
                FROM RequiredRawMaterials r
                INNER JOIN dbo.tblItem i ON i.ItemId = r.ItemId
                GROUP BY r.ItemId
                HAVING SUM(r.RequiredBaseQty) > 0;

                IF EXISTS
                (
                    SELECT 1
                    FROM @StockToDeduct
                    WHERE ISNULL(ConversionFactor, 0) <= 0
                )
                    THROW 50217, 'A raw material has an invalid stock conversion factor.', 1;

                /* Sum all active purchase-unit QOH and convert to Baseunit. */
                UPDATE stock
                SET ActiveRateCount = rates.RateCount,
                    AvailableBaseQty =
                        CONVERT(DECIMAL(18,4), ISNULL(rates.AvailablePurchaseQty, 0)
                            * stock.ConversionFactor)
                FROM @StockToDeduct stock
                OUTER APPLY
                (
                    SELECT
                        COUNT(*) AS RateCount,
                        SUM
                        (
                            CASE WHEN ISNULL(pr.QOH, 0) > 0
                                 THEN CONVERT(DECIMAL(18,4), pr.QOH)
                                 ELSE CONVERT(DECIMAL(18,4), 0)
                            END
                        ) AS AvailablePurchaseQty
                    FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
                    WHERE pr.ProductId = stock.ItemId
                      AND pr.Status = 1
                ) rates;

                IF EXISTS
                (
                    SELECT 1 FROM @StockToDeduct
                    WHERE ISNULL(ActiveRateCount, 0) = 0
                )
                    THROW 50209, 'No active stock records were found for one or more raw materials.', 1;

                IF EXISTS
                (
                    SELECT 1 FROM @StockToDeduct
                    WHERE ISNULL(AvailableBaseQty, 0) < RequiredQty
                )
                    THROW 50210, 'Insufficient raw-material stock. Approval was cancelled.', 1;

                /* FIFO deduction: oldest tblProductRate.Id first. */
                ;WITH RateRows AS
                (
                    SELECT
                        pr.Id,
                        stock.ConversionFactor,
                        CONVERT
                        (
                            DECIMAL(18,4),
                            CASE WHEN ISNULL(pr.QOH, 0) > 0
                                 THEN pr.QOH * stock.ConversionFactor
                                 ELSE 0
                            END
                        ) AS RowQtyBase,
                        stock.RequiredQty,
                        ISNULL
                        (
                            SUM
                            (
                                CONVERT
                                (
                                    DECIMAL(18,4),
                                    CASE WHEN ISNULL(pr.QOH, 0) > 0
                                         THEN pr.QOH * stock.ConversionFactor
                                         ELSE 0
                                    END
                                )
                            ) OVER
                            (
                                PARTITION BY pr.ProductId
                                ORDER BY pr.Id
                                ROWS BETWEEN UNBOUNDED PRECEDING AND 1 PRECEDING
                            ),
                            0
                        ) AS QtyBeforeBase
                    FROM dbo.tblProductRate pr WITH (UPDLOCK, HOLDLOCK)
                    INNER JOIN @StockToDeduct stock
                        ON stock.ItemId = pr.ProductId
                    WHERE pr.Status = 1
                ),
                RateDeductions AS
                (
                    SELECT
                        Id,
                        ConversionFactor,
                        CASE
                            WHEN RowQtyBase <= 0 OR QtyBeforeBase >= RequiredQty
                                THEN CONVERT(DECIMAL(18,4), 0)
                            WHEN QtyBeforeBase + RowQtyBase <= RequiredQty
                                THEN RowQtyBase
                            ELSE RequiredQty - QtyBeforeBase
                        END AS DeductQtyBase
                    FROM RateRows
                )
                UPDATE pr
                SET pr.QOH =
                    CONVERT
                    (
                        DECIMAL(18,4),
                        (
                            CONVERT(DECIMAL(18,8), ISNULL(pr.QOH, 0))
                            * deduction.ConversionFactor
                            - deduction.DeductQtyBase
                        ) / NULLIF(deduction.ConversionFactor, 0)
                    )
                FROM dbo.tblProductRate pr
                INNER JOIN RateDeductions deduction
                    ON deduction.Id = pr.Id
                WHERE deduction.DeductQtyBase > 0;
            END;

            UPDATE dbo.tblInventoryRequestMaster
            SET ApprovalStatus = @ApprovalStatus,
                ApprovedBy = @EmployeeId,
                ApprovedOn = GETDATE(),
                ApprovalRemarks = ISNULL(NULLIF(LTRIM(RTRIM(@ApprovalRemarks)), ''), ''),
                ModifiedBy = @EmployeeId,
                ModifiedOn = GETDATE()
            WHERE InventoryRequestId = @InventoryRequestId
              AND BranchId = @BranchId
              AND ISNULL(Status, 1) = 1
              AND ISNULL(ApprovalStatus, 0) = 0;

            IF @@ROWCOUNT = 0
                THROW 50202, 'Only an active Pending request can be approved, rejected, or sent back.', 1;
        END
        ELSE
            THROW 50203, 'Invalid inventory request approval status.', 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
