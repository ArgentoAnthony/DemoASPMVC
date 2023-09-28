namespace DemoASPMVC_DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Nickname { get; set; } = null!;
        public int RoleId { get; set; }
    }
}