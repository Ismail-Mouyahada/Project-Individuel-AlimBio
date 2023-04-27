using System.ComponentModel.DataAnnotations;
namespace AlimBio.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string? Couleur { get; set; }
        public string? Icon { get; set; }
        [Display(Name = "Nom service")]
        public string? NomService { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Site associé")]
        public int? SiteId { get; set; }
        public Site? Site { get; set; }

        [Display(Name = "Entreprise associé")]
        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }

        public ICollection<Salarie>? Salaries { get; set; }

    }
}
