namespace Auth.Application.Validators;

public class RegexPatterns
{
    public const string NameSurnamePattern = @"^[A-Za-zА-Яа-яЁё]{2,20}$";
    public const string UsernamePattern = @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{5,20}";
    public const string PasswordPattern = @"(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).{8,20}";
}