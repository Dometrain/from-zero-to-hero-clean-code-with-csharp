namespace Parking.Api.Customers;

public record Address(string Street,
    string ZipCode,
    string City,
    string Country);