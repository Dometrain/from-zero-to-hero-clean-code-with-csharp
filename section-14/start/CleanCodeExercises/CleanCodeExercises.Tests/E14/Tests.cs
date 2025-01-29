using NSubstitute;

namespace CleanCodeExercises.Tests.E14;

public class Tests
{
    [Fact]
    public void Get_units_for_period()
    {
        var repository = Substitute.For<ITrackingRepository>();
        repository
            .Get(Arg.Any<DateTime>(), Arg.Any<DateTime>())
            .Returns(new[] { 1, 2 })
            ;
        var query = new TrackingQuery(repository);

        var units = query.GetUnits(DateTime.Now.AddHours(-12), DateTime.Now.AddHours(-8));

        Assert.Equal(2, units.Length);
    }
}