using product_rating.Repositories.Interfaces;
using System.Collections;
using systemrating.Data.EntityModels;

namespace product_rating.Repositories
{
    public class UserRepository: IUserRepository
    {

        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IEnumerable AddUser(User user)
        {
            var get_user = _context.Users.Where(p => p.Name == user.Name);
            if (get_user == null)
            {
                 _context.Add(user);
                _context.SaveChanges();

                return null;
            }
            else
            {
                string Message = "UserName already exists" + user.Name;
                return Message;
            }
        }

        public IEnumerable Login(User user)
        {
            var get_user = _context.Users.Single(p => p.Name == user.Name && p.password == user.password);
            get_user.Name = user.Name;
            get_user.Id = user.Id;
            yield return get_user;
        }
    }
}
