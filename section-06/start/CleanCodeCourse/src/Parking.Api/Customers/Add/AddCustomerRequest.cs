namespace Parking.Api.Customers.Add;

public record AddCustomerRequest(string Name, string[] LicensePlates);