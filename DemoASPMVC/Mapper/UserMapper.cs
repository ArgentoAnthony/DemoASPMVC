using DemoASPMVC_DAL.Models;
using DemoASPMVC.Models.ViewModel;

namespace DemoASPMVC.Mapper
{
    public static class UserMapper
    {

        public static User ToUser (this UserRegisterForm form){

            return new User
            {
                Nickname = form.Nickname,
                Email = form.Email,
                
            };
        }

        public static UserRegisterForm ToRegisterDTO (this User u)
        {

            return new UserRegisterForm
            {
                Nickname = u.Nickname,
                Email = u.Email
            };
        }
    }
}
