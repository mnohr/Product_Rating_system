using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using product_rating.Repositories.Interfaces;
using systemrating.Data.EntityModels;


namespace product_rating.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        public UserController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var get_user = _userRepository.AddUser(user);
                    if (get_user != null)
                    {
                        ModelState.AddModelError(String.Empty, get_user.ToString());
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(String.Empty, "Successfully Registered Mr. " + user.Name);
                        return RedirectToAction("Login");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(String.Empty, "UserName already exists " + user.Name);
                }

            }
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        //const string UserId = "Id";
        //const string UserName = "Name";
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var get_user = _context.Users.Single(p => p.Name == user.Name && p.password == user.password);
                    if (get_user != null)
                    {
                        HttpContext.Session.SetInt32("userId", get_user.Id);
                        HttpContext.Session.SetString("Username", get_user.Name);

                        return RedirectToAction(actionName: "Index", controllerName: "Product");

                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "UserName or Password does not match.");
                        return View(user);
                    }

                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(String.Empty, "UserName or Password does not match in the system" );
                }
              
            }
            return View(user);
        }


        public ActionResult LoggedIn()
        {
            object obj = HttpContext.Session.GetString("userId");
            if (obj != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
