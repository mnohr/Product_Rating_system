using System.Collections;
using systemrating.Data.EntityModels;

namespace product_rating.Repositories.Interfaces
{
    public interface IUserRepository
    {

        IEnumerable AddUser(User user);

        IEnumerable Login(User user);
      
    }
}
