namespace Parking.Api.Domain;

public record Address(string Street,
    string ZipCode,
    string City,
    string Country);