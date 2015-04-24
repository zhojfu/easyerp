namespace Doamin.Service.Users
{
    /// <summary>
    /// Represents the customer login result enumeration
    /// </summary>
    public enum UserLoginResults
    {
        Successful = 1,

        UserNotExist = 2,

        WrongPassword = 3,

        NotActive = 4,

        Deleted = 5
    }
}