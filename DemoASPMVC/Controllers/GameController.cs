using DemoASPMVC.Models;
using DemoASPMVC.Services;
using DemoASPMVC.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DemoASPMVC_DAL.Interface;
using Microsoft.AspNetCore.Http;

namespace DemoASPMVC.Controllers
{
    public class GameController : Controller
    {
        //Injection de l'instance du service via le constructeur du controller
        //GameService gameService { get; set; }

        private readonly IGenreService _genreService;
        private readonly SessionManager _session;
        private readonly IGameService gameService;
        public GameController(IGameService gs, IGenreService genreService, SessionManager session)
        {
            gameService = gs;
            _genreService = genreService;
            _session = session;
        }
        public IActionResult Index()
        {
            ViewData["ListeGenre"] = _genreService.GetAll("Genre");
            return View(gameService.GetGames());
        }
        [HttpPost]
        public IActionResult Index(int genreId)
        {
            return View(gameService.GetGamesByGenre(genreId));
        }

        [CustomAuthorize]
        public IActionResult Details(int id)
        {
            return View(gameService.GetById(id));
        }
        [AdminAuthorize]
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult GetGameByGenre()
        {
            ViewData["ListeGenre"] = _genreService.GetAll("Genre");
            return View();
        }
        [AdminAuthorize]
        public IActionResult Create()
        {
            ViewData["ListeGenre"] = _genreService.GetAll("Genre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game g)
        {
            gameService.Create(g);
            return RedirectToAction("Index");
        }
        [AdminAuthorize]
        public IActionResult Delete(int id)
        {
            //scoped => on garde l'instance durant tout le traitement de l'action Delete
            //Game game = gameService.GetById(id);
            gameService.Delete(id);
            return RedirectToAction("Index");

            //Transient => on crée une nouvelle instance, à chaque fois que le service est appelé
        }
    }
}
