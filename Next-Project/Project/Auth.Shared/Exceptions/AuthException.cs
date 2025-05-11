namespace Auth.Shared.Exceptions;

public class AuthException : Exception
{
    public AuthErrorTypes AuthErrorType { get; set; }

    public AuthException(AuthErrorTypes authErrorType, string message) : base(message)
    {
        AuthErrorType = authErrorType;
    }
}