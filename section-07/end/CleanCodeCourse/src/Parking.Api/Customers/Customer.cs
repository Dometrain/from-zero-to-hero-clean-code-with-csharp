namespace Parking.Api.Customers;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public string[] VehicleLicensePlates { get; set; }
    public bool IsEmployee { get; set; }
    public string MembershipLevel { get; set; } = Parking.Api.Customers.Add.MembershipLevel.Basic.ToString();

    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    
    public bool HasLicensePlates()
    {
        return VehicleLicensePlates?.Any() ?? false;
    }
}