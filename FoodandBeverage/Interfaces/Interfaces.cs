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
            CardSplit = 7
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
        AllLocationAccess = 24

        };
    
}
