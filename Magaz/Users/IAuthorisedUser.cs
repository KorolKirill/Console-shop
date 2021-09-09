namespace Magaz.Users
{
    public interface IAuthorisedUser
    {
        public string Login { get;  set; }
        public string Password { get;  set; }
    }
}