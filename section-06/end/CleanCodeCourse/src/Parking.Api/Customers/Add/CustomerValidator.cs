using System.Text.RegularExpressions;

namespace Parking.Api.Customers.Add;

internal class CustomerValidator
{
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

        var membershipLevel = Enum.Parse<MembershipLevel>(customer.MembershipLevel);
        if (customer.VehicleLicensePlates.Length > GetMaxAllowedLicencePlates(membershipLevel, customer.IsEmployee))
        {
            return false;
        }

        var licensePlateRegex =
            new Regex(
                @"[0-9]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}|[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}[\s-]{0,1}[0-9]{2}|[A-IK-PR-WYZ]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-WYZ]{2}");
        for (int i = 0; i < customer.VehicleLicensePlates.Length; i++)
        {
            if (!licensePlateRegex.Match(customer.VehicleLicensePlates[i]).Success)
                return false;
        }

        return true;
    }


    private int GetMaxAllowedLicencePlates(MembershipLevel membershipLevel, bool isEmployee)
    {
        const int defaultLimitBasicCustomers = 1;
        const int defaultLimitEmployees = 2;
        const int twoLicensePlates = 2;
        const int fiveLicensePlates = 5;

        return membershipLevel switch
        {
            MembershipLevel.Premium => twoLicensePlates,
            MembershipLevel.Gold => fiveLicensePlates,
            MembershipLevel.Silver => twoLicensePlates,
            MembershipLevel.Basic when isEmployee => defaultLimitEmployees,
            MembershipLevel.Basic => defaultLimitBasicCustomers,
            _ => throw new ArgumentOutOfRangeException(nameof(membershipLevel), membershipLevel, null)
        };
    }
}