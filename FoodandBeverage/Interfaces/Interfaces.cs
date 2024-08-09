using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodandBeverage.Interfaces
{
    public enum DateTimeReturnType
    {
        Date,
        Time,
        DateTime
    };
    public enum SearchMode
    {
        FirstCharacter,
        AllCharacters
    };
    public enum StatusValue
    {
        SuperAdmin = -1,
        Active = 0,
        InActive = 1,
        Locked = 2
    };

    public enum DocumentType
    {
        CustomerPhoto,
        EmployeePhoto,
        EmployeeIdentityProof1,
        EmployeeIdentityProof2,
        ItemPhoto,
        CompanyLogo,
        BranchLogo,
        Game,
        ReaderImages,
        FoodPhoto,
        HotelRegistrationProofPics,
        DocumentType
    };
    public enum ItemTaxMode
    {
        Exclusive = 0,
        Inclusive = 1
    };

    public enum ButtonConfig
    {
        NoStartbutton = 0,
        Startbutton = 1
    };
    public enum CurrencyDecimalConfig
    {
        NoDecimal = 0,
        TwoDecimal = 2,
        ThreeDecimal = 3
    };
    public enum DebitOrder
    {
        CashFirst = 0,
        CashBonusFirst = 1
    };
    public enum ReaderModel
    {
        Version1 = 0,
        Version2 = 1
    };
    public enum TicketDispenser
    {
        Enable = 0,
        Disable = 1
    };
    public enum TicketDispenserMechine
    {
        DL_1245 = 0,
        DL_4_p_s = 1
    };
    public enum EnablePulse
    {
        HighPulse = 0,
        LowPulse = 1
    };
    public enum EnableTicketPulse
    {
        HighPulse = 0,
        LowPulse = 1
    };
    public enum DisplaySettings
    {
        ShowTotal = 0,
        ShowSessionTotal = 1
    };
    public enum TapType
    {
        In = 1,
        Out = 0,
        Recharge = 2,
        Redemption = 3,
        Refund = 4,
        ClearCard = 5,
        Consolidation = 6,
        CardSplit = 7,
        FandBPayment = 8
    };

    public enum RedererDisplayTabs
    {
        Idle = 0,
        CardSwipe = 1,
        PayOut = 2,
        HappyHour = 3,
        OutOfService = 4
    };
    public enum PaymentType
    {
        CASH = 1,
        CHEQUE = 2,
        CREDITCARD = 3,
        UPI = 4,
        DEBITCARD = 5,
        BANKINGONLINE = 6,
        OTHERS = 7,
        CREDIT = 8,
        NEFTRTGS = 9,
        COMPLEMENTRY = 10,
        PART = 11,
        SPLIT = 12
    };
    public enum Reservation
    {
        TableBooking = 1,
        Restaurant = 2
    };
    public enum TableMenu
    {
        Rid = 0,
        Mid = 1
    };
    public enum formsenum
    {
        Order = 1,
        Billing = 2,
        Settings = 3,
        DashboardCharts = 4,

        Company = 5,
        Kitchen_View = 6,
        OrderView = 7,
        Category = 8,
        Product = 9,
        Menu = 10,
        Unit = 11,
        Table = 12,
        Purchase = 13,
        Purchase_Return = 14,
        Tax = 15,
        Vendor = 16,
        Discount_And_Offer = 17,
        Expence = 18,
        PromoCode = 19,
        Item = 20,
        Reservation = 21,
        Addon = 22,

        Employee = 23,
        Department = 24,
        Designation = 25,
        User_Account = 26,
        User_Permission = 27,
    };
    public enum DashboardTableType
    {
        Normalorder = 0,
        Reverse_overtime_Cancel = 1,
        Reverse_Table = 2,
        CombinedOrderBill = 3,
        PickupOrder=4,
    };
    public enum Tabletype
    {    
        Product = 1,
        DirectSale = 2,
        ComboProduct = 3,
        Variation_Addon = 4

    };
    public enum Food_Status
    {
        Cancel = 0,
        Pending = 1,
        FoodProcessing = 2,
        Complete = 3,
        Booking_order = 4
    };
    public enum ShowButtonType
    {
        save = 1,
        Print = 2,
        Nnone = 3
    };
    public enum ReportType
    {
        salesReportItem = 1,
        inventoryReportItem = 2,
        expenseReportItem = 3,
        reservationSeatingItem = 4,
        employeePerformanceItem = 5,
        promotionsDiscountsItem = 6,
        loyaltyProgramItem = 7,
        Purchasereportitem = 8,
        MenuAnalysisReports = 9,
        TableTurnoverReports = 10,
        VendorReports = 11
    };
    public enum Rev_Cancel_Type
    {
        Normalcancel = 1,
        reservation_Order = 2
    };
    public enum Product_Addon_Variation_Type
    {
        Variation = 1,
        Addon = 2
    };
    public enum AddonType
    {
        AddonType = 1,
        None = 0
    };
    public enum SubofferStatus
    {
        Enable = 1,
        None = 0
    };

    public enum MainProductStatus
    {
        Primary = 1,
        Secondary = 0
    }
    public enum SplitType
    {
        Number = 1,
        Item = 2,
        Normal=3
    }
    public enum BillSelect
    {
        Selected = 1,
        Unselect = 0
    }
    public enum DMY
    {
        DAY = 0,
        MONTH = 1,
        YEAR = 2,
    }
    public enum SerachType_orderview
    {
        OrderID = 0,
        Tablecode = 1,
    }
    public enum SerachType_Purchase
    {
        SaleProduct = 1,
        NonSaleProduct = 2,
        Category = 3
    }
    public enum Saletype
    {
        Sale = 1,
        NonSaleProduct = 0,
    }
    public enum ControlType
    {
        Label = 1,
        TextBox = 2,
        DataGridView = 3,
        Panel = 4,
        Button = 5,
        CheckBox = 6,
        ComboBox = 7,
        DateTimePicker = 8,
        ListBox = 9,
        PictureBox = 10,
        ProgressBar = 11,
        RadioButton = 12,
        RichTextBox = 13,
        TreeView = 14,
        Tablelayoutpanel=15
    }
    public enum FormType
    {
        Order = 1,
        Kitchen_View = 2,
        Category = 3,
        Item = 4,
        Product = 5,
        Menu = 6,
        Unit = 7,
        Table = 8,
        Purchase = 9,
        Purchase_Return = 10,
        Tax = 11,
        Vendor = 12,
        Discount_And_Offer = 13,
        Expence = 14,
        PromoCode = 15,
        Company = 16,
        Reservation = 17,
        Employee = 18,
        Department = 19,
        Designation = 20,
        User_Account = 21,
        User_Permission = 22,
        OrderView = 23,
        Addon = 24,
        Login=25,
        homepage=26,
        company = 27,
        Keypad = 28,
        DashboardChart = 29,
        TableView = 30,
        Report = 31,
        CombinedBillpage = 32,
        Pickuppage = 33
    };
    public enum Menubutton
    {
        Menuhome = 1,
        Menutable = 2,
        Menuoperation = 3,
        MenuEmployee = 4,
        Menubilling = 5,
        Menureport = 6,
        Menusetting = 7,
        Menulogout = 8,
        Pickup=9
    }
    public enum BillType
    {
        NormalBill = 1,
        BillCopy = 2,
    }
    public enum PageType
    {
        OrderPage = 1,
        Billpage = 2,
    }
    public enum PickupType
    {
        AddNew = 0,
        Existing = 1,
    }
    public enum PickupOrderOrNot
    {
        Pickuporder = 1,
        None = 0,
    }
    public enum SourcepageType
    {
        Tableviewpage = 1,
        Billingpage = 2,
        Pickuppage = 3
    }
    public enum CombinedBillType
    {
        CombinedBill = 1,
        Normal = 2,
        BookingBill = 3,
        PickupBill = 4
    }

    public enum OrderType
    {
        Booking_NormalOrder = 0,
        PickupOrder = 1,
        Combined_pick_normal_Order=2
    }

}

