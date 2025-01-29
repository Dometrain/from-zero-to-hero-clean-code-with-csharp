namespace CleanCodeExercises.Tests.E04;

public class LogInManager
{
    public bool IsLoggedIn = false;
    public bool ProcessLogInRequest(User user)
    {
        return UserIsValid(user) && LogUserIn(user);
    }

    private bool UserIsValid(User user)
    {
        if (string.IsNullOrEmpty(user.Email))
            return false;
        return true;
    }

    private bool LogUserIn(User user)
    {
        IsLoggedIn = true;
        return true;
    }
}