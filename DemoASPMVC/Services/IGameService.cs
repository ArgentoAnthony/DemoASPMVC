using DemoASPMVC.Models;

namespace DemoASPMVC.Services
{
    public interface IGameService
    {
        void AddFavorite(int id);
        void Create(Game game);
        void Delete(int id);
        Game GetById(int id);
        IEnumerable<Game> GetFavoris();
        IEnumerable<Game> GetGames();
        IEnumerable<Game> GetGamesByGenre(int id);
        bool IsFavoris(int idJeu, int IdUser);
        void RemoveFavorite(int id);
    }
}