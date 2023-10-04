using DemoASPMVC.Tools;
using DemoASPMVC_DAL.Interface;
using Microsoft.AspNetCore.Mvc;
using DemoASPMVC_DAL.Services;
using DemoASPMVC_DAL.Models;
using DemoASPMVC.Services;
using DemoASPMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoASPMVC.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly SessionManager _session;

        public GenreController(IGenreService genreService, SessionManager session)
        {
            _genreService = genreService;
            _session = session;
        }
        public IActionResult Index()
        {
            return View(_genreService.GetAll("Genre"));
        }
        [AdminAuthorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre g)
        {
            _genreService.Create(g);
            return RedirectToAction("Index");
        }
        [AdminAuthorize]
        public IActionResult Details(int id)
        {
            return View(_genreService.GetById("Genre", id));
        }
        [AdminAuthorize]
        public IActionResult Delete(int id) 
        {
            _genreService.Delete(id);
            return RedirectToAction("Index");
        }
    }   
}
