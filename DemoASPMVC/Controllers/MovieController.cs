using DemoASPMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoASPMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        public MovieController(IMovieService ms)
        {
            movieService = ms;
        }
        public IActionResult Index()
        {
            return View(movieService.GetAll());
        }
    }
}
