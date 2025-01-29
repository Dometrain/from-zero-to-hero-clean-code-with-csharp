namespace CleanCodeExercises.Tests.E13;

public class Tests
{
    [Fact]
    public void Get_units_for_period()
    {
        var repository = new TrackingRepository();
        repository.Add(DateTime.Now.AddHours(-10), DateTime.Now.AddHours(-9), 10);
        repository.Add(DateTime.Now.AddHours(-9), DateTime.Now.AddHours(-8), 14);
        repository.Add(DateTime.Now.AddHours(-8), DateTime.Now.AddHours(-7), 6);
        var units = repository.Get(DateTime.Now.AddHours(-12), DateTime.Now.AddHours(-8));
        Assert.True(units.Length == 2);
    }

    [Fact]
    public void No_data_if_start_date_gt_finish_date()
    {
        var repository = new TrackingRepository();
        int[] units = repository.Get(DateTime.Now, DateTime.Now.AddDays(-1));
        Assert.True(units is null or { Length: 0 });
    }
}