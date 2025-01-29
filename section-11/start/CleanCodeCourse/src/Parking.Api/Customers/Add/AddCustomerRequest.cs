namespace Parking.Api.Customers.Add;

public record AddCustomerRequest(string Name, string[] LicensePlates,
    string Street,
    string ZipCode,
    string City,
    string Country);