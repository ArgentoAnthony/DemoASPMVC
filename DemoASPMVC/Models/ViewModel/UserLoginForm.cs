using System.ComponentModel.DataAnnotations;
using DemoASPMVC_DAL.Services;

namespace DemoASPMVC.Models.ViewModel
{
    public class UserLoginForm
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")]
        public string Password { get; set; }
        
       
    }
}
