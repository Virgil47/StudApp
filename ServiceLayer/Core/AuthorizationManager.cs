using ServiceLayer.Abstract;
using ServiceLayer.Models;
using StudApp.Models;
using System.Linq;

namespace ServiceLayer.Core
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly StudentContext _ctx;
        public AuthorizationManager()
        {
            _ctx = new StudentContext();
        }
        public PersonGetResponse GetAllPeron(string username, string password)
        {
            var person = _ctx.Persons.FirstOrDefault(f =>f.Login == username && f.Password == password);
            if (person == null)
            {
                return new PersonGetResponse { IsSuccess = false, Message = "Invalid username or password" };
            }
            
            var result = new PersonGetResponse
            {
                Login = person.Login,
                Password = person.Password,
                Role = person.Role,
                IsSuccess = true
            };
            return result;
        }
    }
}
