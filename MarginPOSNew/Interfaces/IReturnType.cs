using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarginPOSNew.Interfaces
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
        Locker
    };
    public enum ItemTaxMode
    {
        TaxExclusive = 0,
        TaxInclusive = 1
      
    };
}
