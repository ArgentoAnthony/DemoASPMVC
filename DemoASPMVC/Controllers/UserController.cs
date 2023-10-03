using DemoASPMVC.Models.ViewModel;
using DemoASPMVC.Services;
using Microsoft.AspNetCore.Mvc;
using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Models;
using DemoASPMVC_DAL.Services;
using Newtonsoft.Json;
using DemoASPMVC.Tools;

namespace DemoASPMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SessionManager _session;

        public UserController(IUserService userService, SessionManager session)
        {
            _userService = userService;
            _session = session;
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
                User connectedUser = _userService.Login(u.Email, u.Password);
                _session.ConnectedUser = connectedUser;
                return RedirectToAction("Index", "Game");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            _session.Logout();
            return RedirectToAction("Index", "Game");
        }
    }
}
