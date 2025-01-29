namespace CleanCodeExercises.Tests.E04;

public class Tests
{
    private readonly LogInManager _logInManager = new LogInManager();

    [Fact]
    public void Valid_user_should_log_in()
    {
        var result = _logInManager.ProcessLogInRequest(new User("random", "random"));
        Assert.True(result);
        Assert.True(_logInManager.IsLoggedIn);
    }
    
    [Fact]
    public void Invalid_user_should_not_log_in()
    {
        var result = _logInManager.ProcessLogInRequest(new User("", "random"));
        Assert.False(result);
        Assert.False(_logInManager.IsLoggedIn);
    }
}

public record User(string Email, string Password);