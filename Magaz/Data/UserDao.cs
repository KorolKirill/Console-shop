using System.Collections.Generic;
using System.Linq;
using Magaz.Users;

namespace Magaz
{
    internal class UserDao : IUserDao
    {
        private readonly DataBase _dataBase;

        List<IAuthorisedUser> Users { get; set; }
        
        public UserDao()
        {
            _dataBase = new DataBase();
            Users = new List<IAuthorisedUser>();
            Initialize();
        }

        private void Initialize()
        {
            Users.Add(new Admin("admin","admin"));
        }

        private bool IsLoginAvailable(string login)
        {
            var user = Users.FirstOrDefault(x => x.Login.Equals(login));
            return user == null;
        }

        private void AddUser(IAuthorisedUser user)
        {
            Users.Add(user);
        }

        public IAuthorisedUser Registrate(string login, string password)
        {
            if (!IsLoginAvailable(login))
            {
                return null;
            }
            var newUser = new User(login, password);
            AddUser(newUser);
            return newUser;

        }
        
        public IAuthorisedUser FindAccount(string login, string password)
        {
            return Users.FirstOrDefault(x => x.Login.Equals(login) &&
                                             x.Password.Equals(password));
        }
    }
}