using Magaz.Users;

namespace Magaz
{
    public interface IUserDao
    {
        public IAuthorisedUser Registrate(string login, string password);

        public IAuthorisedUser FindAccount(string login, string password);
    }
}