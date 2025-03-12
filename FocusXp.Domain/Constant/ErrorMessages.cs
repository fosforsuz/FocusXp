namespace FocusXp.Domain.Constant;

public static class ErrorMessages
{
    public static class Authentication
    {
        public const string InvalidLogin = "Invalid email or password";
    }

    public static class Invalid 
    {
        public const string OldPassword = "Old password is invalid";
        public const string PasswordNotMatch = "Password and confirm password must match";
    }

    public static class Required
    {
        public const string UserId = "User ID is required";
        public const string Username = "Username is required";
        public const string Email = "Email is required";
        public const string OldPassword = "Old password is required";
        public const string Password = "Password is required";
        public const string NewPassword = "New password is required";
        public const string ConfirmPassword = "Confirm password is required";
        public const string Fullname = "Fullname is required";
        public const string Role = "Role is required";
    }

    public static class StringLength
    {
        public const string Username = "Username must be between 3 and 100 characters";
        public const string Email = "Email must be at most 100 characters long";
        public const string PasswordHash = "Password hash must be between 8 and 255 characters";
        public const string Fullname = "Fullname must be between 3 and 100 characters";
        public const string MinSixLength = "Password must be at least 6 characters long";
    }

    public static class InvalidFormat
    {
        public const string Email = "Invalid email format";
        public const string PasswordNotMatch = "Password and confirm password must match";
        public const string Url = "Invalid URL format";
    }

    public static class NotFound
    {
        public const string UserNotFound = "User not found";
    }

    public static class Exists
    {
        public const string UsernameExists = "Username already exists";
        public const string EmailExists = "Email already exists";
    }
}