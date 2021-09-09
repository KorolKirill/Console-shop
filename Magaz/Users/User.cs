namespace Magaz.Users
{
    public class User : IAuthorisedUser, IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}