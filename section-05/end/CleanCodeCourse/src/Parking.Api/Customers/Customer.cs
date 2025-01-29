namespace Parking.Api.Customers;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public string[] VehicleLicensePlates { get; set; }
    public bool IsEmployee { get; set; }
    public string MembershipLevel { get; set; } = "Basic";
}