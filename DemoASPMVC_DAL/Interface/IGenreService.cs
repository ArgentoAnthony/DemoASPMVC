using DemoASPMVC_DAL.Models;

namespace DemoASPMVC_DAL.Interface
{
    public interface IGenreService : IBaseRepository<Genre>
    {
        void Create(Genre genre);
        void Delete(int id);
    }
}