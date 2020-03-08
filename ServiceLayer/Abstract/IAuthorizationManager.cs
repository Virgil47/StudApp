using ServiceLayer.Models;

namespace ServiceLayer.Abstract
{
    public interface IAuthorizationManager
    {
        PersonGetResponse GetAllPeron(string username, string password);
    }
}
