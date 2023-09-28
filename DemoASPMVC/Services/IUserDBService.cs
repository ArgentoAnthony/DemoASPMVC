using DemoASPMVC.Models.ViewModel;
using DemoASPMVC_DAL.Models;


namespace DemoASPMVC.Services
{
    public interface IUserDBService
    {
        void Create(UserRegisterForm u);
        IEnumerable<User> GetUsers();
        User GetUsersById(int id);
        void Delete(int id);
        User Update(int id, UserRegisterForm u);
        UserRegisterForm GetUsersByIdForm(int id);
    }
}