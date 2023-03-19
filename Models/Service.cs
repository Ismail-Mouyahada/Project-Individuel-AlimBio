namespace AlimBio.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string? Couleur { get; set; }
        public string? Icon { get; set; }
        public string? NomService { get; set; }
        public string? Description { get; set; }

        public int? SiteId { get; set; }
        public Site? Site { get; set; }
        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }

        public ICollection<Salarie>? Salaries { get; set; }

    }
}
