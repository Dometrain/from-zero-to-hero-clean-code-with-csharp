namespace CleanCodeExercises.Tests.E15;

public class ClientService
{
    public bool AddClient(string firstName, string surname, string email, string phone, DateTime dateOfBirth,
        int countryId, string type)
    {
        if (string.IsNullOrEmpty(firstName) || surname == null)
        {
            return false;
        }

        if (!email.Contains('@'))
        {
            return false;
        }

        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;

        if (age < 18)
        {
            return false;
        }

        var countryRepository = new CountryRepository();
        var country = countryRepository.Get(countryId);

        var client = new Client
        {
            Country = country,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            Firstname = firstName,
            Surname = surname,
            Type = type,
            Phone = phone
        };

        if (client.Type == "Premium" || client.Type == "Gold")
        {
            // Needs a Client Success Manager assigned to the Client
            client.RequiresClientSuccessManager = true;

            var csmRepository = new CsmRepository();
            var csmCollection = csmRepository.GetCSMsWithAvailableCapacity();
            // if(csmCollection==null)
            //     // TODO
            client.CSM = csmCollection[0];
        }
        else
        {
            client.RequiresClientSuccessManager = false;
        }


        // Add to db
        var clientRepository = new ClientRepository();
        clientRepository.AddClient(client);

        return true;
    }
}