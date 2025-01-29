namespace CleanCodeExercises.Tests.E14;

public interface ITrackingRepository
{
    int[] Get(DateTime start, DateTime finish);
    void Add(DateTime start, DateTime finish, int units);
}