namespace CleanCodeExercises.Tests.E01;

public class MyCourseManager : ICourseManager
{
    
    private readonly List<School> _schoolsCollection = new List<School>();
    private int? MySchool;


    public void AddSchoolToCourseManager(School school)
        => _schoolsCollection.Add(school);
    
    public CT CreateTeacher(int id, string name)
    {
        return new CT
        {
            Id = id,
            Name = name
        };
    }

    public void SetCurrentSchool(string name)
    {
        MySchool = _schoolsCollection.FindIndex(s => s.SchoolName == name);
    }
    public School GetSchool()
    {
        if (MySchool ==null)
            return _schoolsCollection[0];
        
        var myScool = _schoolsCollection[MySchool.Value];
        return myScool;
    }
}