using DemoASPMVC.Models.ViewModel;
using DemoASPMVC.Services;
using Microsoft.AspNetCore.Mvc;
using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using DemoASPMVC_DAL.Services;

namespace DemoASPMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetAll("UsersDB"));
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(_userService.GetById("UsersDB",id));
        }

        [HttpPost]
        public IActionResult Register(UserRegisterForm u)
        {
            if (!ModelState.IsValid)
            {
                return View(u);
            }

            if (_userService.Register(u.Email, u.Password, u.Nickname))
            {
                return RedirectToAction("Index", "User");
            }

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginForm u)
        {
            try
            {
                User user = _userService.Login(u.Email, u.Password);
                return RedirectToAction("Index", "User");
            }
            catch (Exception ex) 
            {
                ViewBag.error = ex.Message;
                return View();
            }                 
        }
    }
}
