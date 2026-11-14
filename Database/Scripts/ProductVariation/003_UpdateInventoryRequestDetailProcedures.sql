USE [FoodBeverage];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/* Recreate the two inventory request detail procedures with variation support. */
CREATE   PROCEDURE [dbo].[prdInsert_Upd_InventoryRequestDetails]
    @InventoryRequestDetailId INT,
    @InventoryRequestId INT,
    @RequestType INT,
    @SlNo INT = NULL,
    @ProductId INT = NULL,
    @VariationId BIGINT = 0,
@PlannedQty DECIMAL(18,4) = NULL,
    @RecipeItemId BIGINT = NULL,
    @PerPortionQty DECIMAL(18,4) = NULL,
    @TotalRequiredQty DECIMAL(18,4) = NULL,
    @ItemId BIGINT = NULL,
    @ProductType INT = NULL,
    @CurrentStock DECIMAL(18,4) = NULL,
    @IssuedQty DECIMAL(18,4) = NULL,
    @ProjectedBalance DECIMAL(18,4) = NULL,
    @Unit INT = NULL,
    @PurposeRemarks VARCHAR(MAX) = NULL,
    @CreatedBy INT = NULL,
    @CreatedOn DATETIME = NULL,
    @ModifiedBy INT = NULL,
    @ModifiedOn DATETIME = NULL,
    @BranchId INT = NULL,
    @Status INT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @DetailId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@InventoryRequestDetailId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestDetails
            SET RequestType = @RequestType,
                SlNo = @SlNo,
                ProductId = @ProductId,
                VariationId = ISNULL(@VariationId, 0),
                PlannedQty = @PlannedQty,
                RecipeItemId = @RecipeItemId,
                PerPortionQty = @PerPortionQty,
                TotalRequiredQty = @TotalRequiredQty,
                ItemId = @ItemId,
                ProductType = @ProductType,
                CurrentStock = @CurrentStock,
                IssuedQty = @IssuedQty,
                ProjectedBalance = @ProjectedBalance,
                Unit = @Unit,
                PurposeRemarks = @PurposeRemarks,
                ModifiedBy = ISNULL(@ModifiedBy, @CreatedBy),
                ModifiedOn = ISNULL(@ModifiedOn, GETDATE()),
                BranchId = @BranchId,
                Status = @Status
            WHERE InventoryRequestDetailId = @InventoryRequestDetailId
              AND InventoryRequestId = @InventoryRequestId;

            IF @@ROWCOUNT = 0
                THROW 50002, 'Inventory request detail was not found.', 1;

            SET @DetailId = @InventoryRequestDetailId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblInventoryRequestDetails
            (
                InventoryRequestId, RequestType, SlNo,
                ProductId, VariationId, PlannedQty, RecipeItemId,
                PerPortionQty, TotalRequiredQty,
                ItemId, ProductType, CurrentStock,
                IssuedQty, ProjectedBalance, Unit,
                PurposeRemarks, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @InventoryRequestId, @RequestType, @SlNo,
                @ProductId, ISNULL(@VariationId, 0), @PlannedQty, @RecipeItemId,
                @PerPortionQty, @TotalRequiredQty,
                @ItemId, @ProductType, @CurrentStock,
                @IssuedQty, @ProjectedBalance, @Unit,
                @PurposeRemarks, @CreatedBy,
                ISNULL(@CreatedOn, GETDATE()),
                @BranchId, @Status
            );

            SET @DetailId = CONVERT(INT, SCOPE_IDENTITY());
        END

        COMMIT TRANSACTION;

        SELECT @DetailId AS InventoryRequestDetailId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[prdInsert_Upd_InventoryRequestDraftDetails]    Script Date: 11/11/2026 9:48:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* ============================================================
   4. INVENTORY REQUEST DRAFT DETAILS - INSERT / UPDATE
   Procedure: prdInsert_Upd_InventoryRequestDraftDetails
   ============================================================ */
CREATE   PROCEDURE [dbo].[prdInsert_Upd_InventoryRequestDraftDetails]
    @DraftDetailId INT,
    @DraftId INT,
    @RequestType INT,
    @SlNo INT = NULL,
    @ProductId INT = NULL,
    @VariationId BIGINT = 0,
@PlannedQty DECIMAL(18,4) = NULL,
    @RecipeItemId BIGINT = NULL,
    @PerPortionQty DECIMAL(18,4) = NULL,
    @TotalRequiredQty DECIMAL(18,4) = NULL,
    @ItemId BIGINT = NULL,
    @ProductType INT = NULL,
    @CurrentStock DECIMAL(18,4) = NULL,
    @IssuedQty DECIMAL(18,4) = NULL,
    @ProjectedBalance DECIMAL(18,4) = NULL,
    @Unit INT = NULL,
    @PurposeRemarks VARCHAR(MAX) = NULL,
    @CreatedBy INT = NULL,
    @CreatedOn DATETIME = NULL,
    @ModifiedBy INT = NULL,
    @ModifiedOn DATETIME = NULL,
    @BranchId INT = NULL,
    @Status INT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @SavedDraftDetailId INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF ISNULL(@DraftDetailId, 0) > 0
        BEGIN
            UPDATE dbo.tblInventoryRequestDraftDetails
            SET RequestType = @RequestType,
                SlNo = @SlNo,
                ProductId = @ProductId,
                VariationId = ISNULL(@VariationId, 0),
                PlannedQty = @PlannedQty,
                RecipeItemId = @RecipeItemId,
                PerPortionQty = @PerPortionQty,
                TotalRequiredQty = @TotalRequiredQty,
                ItemId = @ItemId,
                ProductType = @ProductType,
                CurrentStock = @CurrentStock,
                IssuedQty = @IssuedQty,
                ProjectedBalance = @ProjectedBalance,
                Unit = @Unit,
                PurposeRemarks = @PurposeRemarks,
                ModifiedBy = ISNULL(@ModifiedBy, @CreatedBy),
                ModifiedOn = ISNULL(@ModifiedOn, GETDATE()),
                BranchId = @BranchId,
                Status = @Status
            WHERE DraftDetailId = @DraftDetailId
              AND DraftId = @DraftId;

            IF @@ROWCOUNT = 0
                THROW 50004, 'Inventory request draft detail was not found.', 1;

            SET @SavedDraftDetailId = @DraftDetailId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.tblInventoryRequestDraftDetails
            (
                DraftId, RequestType, SlNo,
                ProductId, VariationId, PlannedQty, RecipeItemId,
                PerPortionQty, TotalRequiredQty,
                ItemId, ProductType, CurrentStock,
                IssuedQty, ProjectedBalance, Unit,
                PurposeRemarks, CreatedBy, CreatedOn,
                BranchId, Status
            )
            VALUES
            (
                @DraftId, @RequestType, @SlNo,
                @ProductId, ISNULL(@VariationId, 0), @PlannedQty, @RecipeItemId,
                @PerPortionQty, @TotalRequiredQty,
                @ItemId, @ProductType, @CurrentStock,
                @IssuedQty, @ProjectedBalance, @Unit,
                @PurposeRemarks, @CreatedBy,
                ISNULL(@CreatedOn, GETDATE()),
                @BranchId, @Status
            );

            SET @SavedDraftDetailId = CONVERT(INT, SCOPE_IDENTITY());
        END

        COMMIT TRANSACTION;

        SELECT @SavedDraftDetailId AS DraftDetailId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
