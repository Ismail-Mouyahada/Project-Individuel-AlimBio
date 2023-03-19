namespace AlimBio.Models
{
    public class Ville
    {
        public int Id { get; set; }
        public string? NomVille { get; set; }
        public string? CodePostal { get; set; }

        public ICollection<Entreprise>? Entreprises { get; set; }
        public ICollection<Site>? Sites { get; set; }
        public ICollection<Salarie>? Salaries { get; set; }
    }
}
