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

        if (customer.VehicleLicensePlates == null || !customer.VehicleLicensePlates.Any())
        {
            return false;
        }

        if (customer.VehicleLicensePlates.Length > GetMax(customer.MembershipLevel, customer.IsEmployee))
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

    private int GetMax(string membershipLevel, bool isEmployee)
    {
        switch (membershipLevel)
        {
            case "Premium":
                return 2;
            case "Gold":
                return 5;
            default:
                return isEmployee ? 2 : 1;
        }
    }
}