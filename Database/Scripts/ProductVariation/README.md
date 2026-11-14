# Product variation database update

Run these scripts in order against the FoodBeverage database:

1. `001_AddVariationIdColumns.sql`
   - Adds nullable `VariationId` columns to ingredient details, inventory request details, inventory request draft details, and sales details.
   - Creates supporting indexes.
   - Existing records remain unchanged; NULL is treated as the legacy/default variation (ID 0).

2. `003_UpdateInventoryRequestDetailProcedures.sql`
   - Updates inventory request and draft detail insert/update procedures to save `VariationId`.

3. `002_UpdateVariationStockProcedures.sql`
   - Updates `prdUpdate_InventoryRequestApproval` to validate and deduct only the selected variation's raw-material recipe.
   - Updates `prdInsert_Upd_SalesDetails` to save the sale's `VariationId` and deduct only that variation's recipe.
   - Stock quantities continue to be deducted from active `tblProductRate.QOH` rows in FIFO order. `tblItem` stores item/unit/conversion metadata; it is not the stock-balance table in the supplied schema.

The scripts are written for the `FoodBeverage` database. If your target database has a different name, select the correct database and adjust the `USE [FoodBeverage]` statement before execution.

Back up the database before running the scripts. Run them in the stated order before deploying the updated application.
