namespace Magaz.Users
{
    public class Admin : IAuthorisedUser, IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Admin(string login, string password)
        {
            Login = login;
            Password = password;
        }
            
        //demoversion надо подумать над таким способом
        // public Admin( User user)
        // {
        //     Login = user.Login;
        //     Password = user.Password;
        // }
    }
}