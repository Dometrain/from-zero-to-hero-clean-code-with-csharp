using Parking.Api.Customers.Add;

namespace Parking.Api.Domain;

public class Customer
{
	private readonly List<LicensePlate> _vehicleLicensePlates;

	public Customer(string name, bool isEmployee,
		Address address,
		MembershipLevel membershipLevel = MembershipLevel.Basic)
	{
		Id = Guid.NewGuid();
		CreatedOn = DateTime.UtcNow;
		Name = name;
		IsEmployee = isEmployee;
		MembershipLevel = membershipLevel;
		Address = address;
		_vehicleLicensePlates = new();
	}

	public Guid Id { get; private set; }
	public string Name { get; private set; }
	public DateTime CreatedOn { get; private set; }

	public IReadOnlyCollection<LicensePlate> VehicleLicensePlates
		=> _vehicleLicensePlates.ToArray();

	public bool IsEmployee { get; private set; }
	public MembershipLevel MembershipLevel { get; private set; }
	public Address Address { get; private set; }

	public void AddLicensePlate(LicensePlate licensePlate)
	{
		_vehicleLicensePlates.Add(licensePlate);
	}

	public bool HasLicensePlates() 
		=> VehicleLicensePlates.Count > 0;
}