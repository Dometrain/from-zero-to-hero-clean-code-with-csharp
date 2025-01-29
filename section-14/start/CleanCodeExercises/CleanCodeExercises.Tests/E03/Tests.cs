namespace CleanCodeExercises.Tests.E03;

public class Tests
{
    [Fact]
    public void Should_find_user_by_name()
    {
        var users = new[] { "Gui", "Guilherme" };

        var result = Find("Gui", users);
        Assert.True(result);
    }
    
    [Fact]
    public void Should_return_false_if_cannot_find_user()
    {
        var users = new[] { "Gui", "Guilherme" };

        var result = Find("John", users);
        Assert.False(result);
    }

    private bool Find(string name, string[] collection)
    {
        var flag = false;
        int index = 0;
        while (flag == false && index < collection.Length)
        {
            var nameIsNotEqual = name != collection[index];
            if (!nameIsNotEqual)
                flag = true;
            index++;
        }

        return flag;
    }
}