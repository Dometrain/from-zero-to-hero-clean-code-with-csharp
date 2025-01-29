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

        if(!Enum.TryParse<MembershipLevel>(customer.MembershipLevel, out var membershipLevel))
            return false;

        var limit = customer.IsEmployee ? GetMaxAllowedLicencePlatesForEmployee()
                : GetMaxAllowedLicencePlatesForRegularCustomer(membershipLevel);
        if (customer.VehicleLicensePlates.Length > limit)
        {
            return false;
        }

        var licensePlateRegex =
            new Regex(
                @"[0-9]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}|[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}[\s-]{0,1}[0-9]{2}|[A-IK-PR-WYZ]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-WYZ]{2}");
        
        foreach (var licensePlate in customer.VehicleLicensePlates)
        {
            if (!licensePlateRegex.Match(licensePlate).Success)
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