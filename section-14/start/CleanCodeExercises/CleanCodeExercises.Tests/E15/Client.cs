namespace CleanCodeExercises.Tests.E15;

public class Client
{
    public ClientSuccessManager CSM { get; set; }
    public Country Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Type { get; set; }
    public bool RequiresClientSuccessManager { get; set; }
    public string Phone { get; set; }
}