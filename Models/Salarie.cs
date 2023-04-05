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
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Pays { get; set; }
        public string? Image { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }
        public int? SiteId { get; set; }
        public Site? Site { get; set; }
    }

}
