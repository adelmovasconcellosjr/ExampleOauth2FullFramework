using ExampleOauth2FullFramework.Models;
using System.Configuration;

namespace ExampleOauth2FullFramework.Service
{
    public class UserService
    {
        public UserApp GetUserByCredentials(string login, string password)
        {

            string usuario = ConfigurationManager.AppSettings["UserName"];
            string senha = ConfigurationManager.AppSettings["Password"];

            UserApp user = new UserApp();

            if (login.Equals(usuario) && password.Equals(senha))
            {
                user.Id = "1";
                user.Email = usuario;
                user.Name = usuario;
            }
            else
            {
                return null;
            }

            return user;
        }
    }
}