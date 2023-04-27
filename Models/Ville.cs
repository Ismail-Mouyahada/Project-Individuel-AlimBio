using System.ComponentModel.DataAnnotations;
namespace AlimBio.Models
{
    public class Ville
    {
        public int Id { get; set; }
        [Display(Name = "Nom ville")]
        public string? NomVille { get; set; }
        [Display(Name = "Code Postal")]
        public string? CodePostal { get; set; }

        public ICollection<Entreprise>? Entreprises { get; set; }
        public ICollection<Site>? Sites { get; set; }
        public ICollection<Salarie>? Salaries { get; set; }
    }
}
