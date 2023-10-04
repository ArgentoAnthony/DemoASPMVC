using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DemoASPMVC_DAL.Models;

namespace DemoASPMVC.Models
{
    public class Game : Genre
    {

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("Titre du jeu")]
        [MinLength(5, ErrorMessage = "Le titre doit faire minimum 5 caractères")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Genre_Id { get; set; }
        public string Label { get; set; }
    }
}
