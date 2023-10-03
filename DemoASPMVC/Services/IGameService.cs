using DemoASPMVC.Models;

namespace DemoASPMVC.Services
{
    public interface IGameService
    {
        void Create(Game game);
        void Delete(int id);
        Game GetById(int id);
        IEnumerable<Game> GetGames();
        IEnumerable<Game> GetGamesByGenre(int id);
    }
}