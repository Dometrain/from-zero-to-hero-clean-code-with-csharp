using System.Text.RegularExpressions;

namespace Parking.Api.Customers.Add;

internal class CustomerValidator
{
    private const int DefaultLimitBasicCustomers = 1;
    private const int TwoLicensePlates = 2;
    private const int FiveLicensePlates = 5;
    
    public bool IsValid(Customer customer)
    {
        if (customer.Id == Guid.Empty)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(customer.Name))
        {
            return false;
        }

        if (!customer.HasLicensePlates())
        {
            return false;
        }
        
        var limit = customer.IsEmployee ? GetMaxAllowedLicencePlatesForEmployee()
                : GetMaxAllowedLicencePlatesForRegularCustomer(customer.MembershipLevel);
        if (customer.VehicleLicensePlates.Count > limit)
        {
            return false;
        }

    

        return true;
    }
    
    private int GetMaxAllowedLicencePlatesForEmployee()
    {
        return TwoLicensePlates;
    }
    private int GetMaxAllowedLicencePlatesForRegularCustomer(MembershipLevel membershipLevel)
    {
        return membershipLevel switch
        {
            MembershipLevel.Premium => TwoLicensePlates,
            MembershipLevel.Gold => FiveLicensePlates,
            MembershipLevel.Silver => TwoLicensePlates,
            MembershipLevel.Basic => DefaultLimitBasicCustomers,
            _ => throw new ArgumentOutOfRangeException(nameof(membershipLevel), membershipLevel, null)
        }; 
    }
}