using DemoASPMVC.Models;

namespace DemoASPMVC.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAll();
    }
}
