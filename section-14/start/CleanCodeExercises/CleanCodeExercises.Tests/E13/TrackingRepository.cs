namespace CleanCodeExercises.Tests.E13;

public class TrackingRepository
{
    private readonly List<(DateTime start, DateTime finish, int units)> _data = new();

    public int[] Get(DateTime start, DateTime finish)
    {
        if (start > finish)
            return null;
        
        return _data.Where(d => d.start >= start && d.finish <= finish)
            .Select(d => d.units).ToArray();
    }

    public void Add(DateTime start, DateTime finish, int units)
    {
        _data.Add((start, finish, units));
    }
}