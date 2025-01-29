namespace Parking.Api.Domain.Exceptions;

public class InvalidLicensePlateException : Exception
{
    public string LicensePlate { get; private set; }
    public InvalidLicensePlateException(string licensePlate)
    {
        LicensePlate = licensePlate;
    }
}