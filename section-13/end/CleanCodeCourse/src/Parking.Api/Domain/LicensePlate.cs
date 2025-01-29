using System.Text.RegularExpressions;
using Parking.Api.Customers;
using Parking.Api.Domain.Exceptions;

namespace Parking.Api.Domain;

public partial class LicensePlate
{
    private static readonly Regex LicensePlateRegex = Should_HavePTLicensePlateFormat();
    
    public LicensePlate(string text)
    {
        if (!IsValidLicensePlate(text))
            throw new InvalidLicensePlateException(text);
        Text = text;
    }

    public string Text { get; }

    private static bool IsValidLicensePlate(string licensePlate) 
	    => LicensePlateRegex.Match(licensePlate).Success;

    [GeneratedRegex(@"[0-9]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}|[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}[\s-]{0,1}[0-9]{2}|[A-IK-PR-WYZ]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-WYZ]{2}")]
    private static partial Regex Should_HavePTLicensePlateFormat();
}