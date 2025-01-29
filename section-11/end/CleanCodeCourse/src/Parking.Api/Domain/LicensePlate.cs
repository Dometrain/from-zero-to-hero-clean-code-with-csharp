using System.Text.RegularExpressions;
using Parking.Api.Customers;

namespace Parking.Api.Domain;

public partial class LicensePlate
{
    private static readonly Regex LicensePlateRegex = PTLicensePlateRegex();
    
    public LicensePlate(string text)
    {
        if (!IsValidLicensePlate(text))
            throw new InvalidLicensePlateException(text);
        Text = text;
    }

    public string Text { get; }

    private static bool IsValidLicensePlate(string licensePlate)
    {
        if (!LicensePlateRegex.Match(licensePlate).Success)
            return false;

        return true;
    }

    [GeneratedRegex(@"[0-9]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}|[0-9]{2}[\s-]{0,1}[A-IK-PR-VZ]{2}[\s-]{0,1}[0-9]{2}|[A-IK-PR-WYZ]{2}[\s-]{0,1}[0-9]{2}[\s-]{0,1}[A-IK-PR-WYZ]{2}")]
    private static partial Regex PTLicensePlateRegex();
}