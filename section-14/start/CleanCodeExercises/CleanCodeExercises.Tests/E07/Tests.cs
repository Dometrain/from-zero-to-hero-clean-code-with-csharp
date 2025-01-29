namespace CleanCodeExercises.Tests.E07;

public class Tests
{
    [Fact]
    public void Should_handle_new_employee()
    {
        CreateEmployee("Gui",
            1,
            new DateOnly(1988, 1, 1),
            "gui@guiferreira.me",
            "271 Aviation Way",
            "1B",
            "90071",
            "Los Angeles"
        );
    }

    private void CreateEmployee(string name, int employeeNumber, DateOnly birthday, string email,
        string address, string address2, string postcode, string city)
    {
        //
    }
}