using Parking.Api.Customers.Add;

namespace Parking.Tests.Builders;

public class AddCustomerRequestBuilder
{
    private string _name;
    private readonly List<string> _licensePlates;

    public AddCustomerRequestBuilder()
    {
        _licensePlates = new List<string>();
    }

    public AddCustomerRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public AddCustomerRequestBuilder AddLicensePlate(string licensePlate)
    {
        _licensePlates.Add(licensePlate);
        return this;
    }

    public AddCustomerRequest Build()
    {
        return new AddCustomerRequest(_name, _licensePlates.ToArray(),
            string.Empty, string.Empty, string.Empty, string.Empty);
    }
}