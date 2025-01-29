namespace CleanCodeExercises.Tests.E01;

public class Tests
{
    [Fact]
    public void Should_create_new_course_teacher()
    {
        var nameString = "John";
        ICourseManager course = new MyCourseManager();
        var t = course.CreateTeacher(1, nameString);
        Assert.Equal(1, t.Id);
        Assert.Equal(nameString, t.Name);
    }

    [Fact]
    public void Should_get_current_school()
    {
        var manager = new MyCourseManager();
        manager.AddSchoolToCourseManager(new School { SchoolName = "Dometrain" });
        manager.AddSchoolToCourseManager(new School { SchoolName = "Other" });
        manager.SetCurrentSchool("Dometrain");

        var result = manager.GetSchool();

        Assert.Equal("Dometrain", result.SchoolName);
    }
}