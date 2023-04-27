using System.ComponentModel.DataAnnotations;
namespace AlimBio.Models
{
    public class Salarie
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Poste { get; set; }
        public string? Email { set; get; }
        public string? Mobile { get; set; }
        public string? Fix { get; set; }
        public string? Adresse { set; get; }
        [Display(Name = "Code Postal")]
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Pays { get; set; }
        public string? Image { get; set; }

        [Display(Name = "Service Associé")]
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        [Display(Name = "Entreprise Associé")]
        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }
        [Display(Name = "Site Associé")]
        public int? SiteId { get; set; }
        public Site? Site { get; set; }
    }

}
