namespace CleanCodeExercises.Tests.E14;

public class TrackingQuery
{
    private readonly ITrackingRepository _repository;

    public TrackingQuery(ITrackingRepository repository)
    {
        _repository = repository;
    }

    public int[] GetUnits(DateTime start, DateTime finish)
    {
        return _repository.Get(start, finish);
    }
}