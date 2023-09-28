using DemoASPMVC.Models;
using DemoASPMVC.Services;
using Microsoft.AspNetCore.Mvc;
using DemoASPMVC.Models.ViewModel;

namespace DemoASPMVC.Controllers
{
    public class UserDBController : Controller
    {
        private readonly IUserDBService userDBService;
        public UserDBController(IUserDBService us)
        {
            userDBService = us;
        }
        public IActionResult Index()
        {
            return View(userDBService.GetUsers());
        }
        public IActionResult Edit(int id)
        {
            return View(userDBService.GetUsersByIdForm(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, UserRegisterForm u)
        {

            userDBService.Update(id, u);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            return View(userDBService.GetUsersById(id));
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterForm u)
        {
            userDBService.Create(u);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            userDBService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
