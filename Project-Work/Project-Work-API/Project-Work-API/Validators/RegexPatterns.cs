
namespace Project_Work_API.Validators;

public class RegexPatterns
{
    public const string usernamePattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[A-Za-z0-9]{5,15}$";
    public const string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[A-Za-z0-9]{8,20}$";
}