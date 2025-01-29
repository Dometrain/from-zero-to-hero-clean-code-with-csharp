namespace CleanCodeExercises.Tests.E14;

public class TrackingRepository : ITrackingRepository
{
    private readonly List<(DateTime start, DateTime finish, int units)> _data = new();

    public int[] Get(DateTime start, DateTime finish)
    {
        return _data.Where(d => d.start >= start && d.finish <= finish)
            .Select(d => d.units).ToArray();
    }

    public void Add(DateTime start, DateTime finish, int units)
    {
        _data.Add((start, finish, units));
    }
}