namespace CleanCodeExercises.Tests.E01;

public interface ICourseManager
{
    CT CreateTeacher(int id, string name);
    void SetCurrentSchool(string name);
    School GetSchool();
}