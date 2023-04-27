using System.ComponentModel.DataAnnotations;

namespace AlimBio.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string? Siret { get; set; }
        [Display(Name = "Nom de site")]
        public string? NomSite { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Type de site")]
        public string? TypeSite { get; set; }
        public string? Statut { get; set; }

        [Display(Name = "Nombres des salariés")]
        public string? NombreEmployees { get; set; }
        public string? Capital { get; set; }
        public string? Adresse { get; set; }
        public string? Langitude { get; set; }
        public string? Largitude { get; set; }
        public string? Effectif { get; set; }

        [Display(Name = "Date de création")]
        public string? DateCreation { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }

        [Display(Name = "Site web")]
        public string? SiteWeb { get; set; }

        [Display(Name = "Entreprise assigné")]
        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }
        [Display(Name = "Ville assigné")]
        public int? VilleId { get; set; }
        public Ville? Ville { get; set; }

        public ICollection<Salarie>? Salaries { get; set; }
    }
}
