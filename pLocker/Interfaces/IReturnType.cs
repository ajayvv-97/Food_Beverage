using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pLocker.Interfaces
{
    public enum DateTimeReturnType
    {
        Date,
        Time,
        DateTime
    };
    public enum SearchMode
    {
        FirstCharacter=0,
        AllCharacters=1
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
            Locker




    };
    public enum ItemTaxMode
    {
        Exclusive = 0,
        Inclusive = 1

    };
    public enum formsenum
    {
        Home = 1,
        Corporate = 2,
        Location = 3,
        Game = 4,
        Game_Category_SubCategory = 5,
        Product = 6,
        Purchase = 7,
        Purchase_Return = 8,
        BlockList = 9,
        TaxSettings = 10,
        ButtonCreation = 11,
        Happy_Hour = 12,
        Offer = 13,
        MemberShip = 14,
        Customer = 15,
        CardSale = 16,
        FamilyCard = 17,
        GroupSale = 18,
        Employee = 19,
        UserRegistration = 20,
        Role_Registartion = 21,
        Designation = 22,
        Department = 23,
        Profile = 24,
        Recharge = 25,
        Report = 26,
        AllLocationAccess = 27

    };
    public enum GameInterface
    {
        Video = 1,
        Redemption = 2,
        Crane = 3,
        Table_Game = 4,
        Attractions = 5,
        Backend_Points = 6,
        Balance_Review = 7,
        Redemption_Center = 8,
        Merchandise = 9,
        Coin_Ticket_Tracking = 10,
        Ticket_Eater = 11,
        Photo_Fix = 12,
        MDB_Vending = 13,
        Benchmark_Ticket_To_Price = 14,
        Deltronics_Serial_Ticket_Eater = 15,
        Entry_Gate_Locker = 16,
        Exit_Gate_Locker = 17,
        Acceess_Control = 18

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
    public enum ProductType
    {
        Game = 1,
        Card = 2,
        FB = 3,
        GateAccess = 4,
        TimePlay = 5,
        RedemptionTicket = 6,
        Trampoline = 7,
        Merchandise = 8,
        GroupSale = 9,
        FamilyCard = 10,
        Bowling = 11
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
        CardSplit = 7
    };

    public enum PosSalProduct
    {
        Card = 1,
        FB = 2,
        TimePlay = 3,
        Reedemption = 4,
        GateAccess = 5,
        Merchandise = 6,
        Trampoline = 7,
        Bowling = 8
    };
    public enum RedererDisplayTabs
    {
        Idle = 0,
        CardSwipe = 1,
        PayOut = 2,
        HappyHour = 3,
        OutOfService = 4
    };
    public enum BillType
    {
        SingleLockerBill = 0,
        BulkLockerBill = 1,
        MultiLockerBill = 2
    }
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
        COMPLEMENTRY = 10
    };
}
