namespace CleanCodeExercises.Tests.E11;

public class Tests
{
    [Fact]
    public void Track_units_for_period()
    {
        var repository = new TrackingRepository();

        repository.Add(DateTime.Now.AddMinutes(-15), DateTime.Now, 10);
    }

    [Fact]
    public void Get_units_for_period()
    {
        var repository = new TrackingRepository();

        int[] units = repository.Get(DateTime.Now.AddMinutes(-60), DateTime.Now);
    }
}