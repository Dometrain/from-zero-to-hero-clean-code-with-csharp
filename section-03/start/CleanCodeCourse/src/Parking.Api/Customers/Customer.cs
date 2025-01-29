namespace Parking.Api.Customers;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CustomerCreatedOn { get; set; }
    public string[] CustomerVLPCollection { get; set; }
    public bool Employee { get; set; }

    public string MembershipLevel { get; set; } = "Basic";
}