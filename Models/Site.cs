namespace AlimBio.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string? Siret { get; set; }
        public string? NomSite { get; set; }
        public string? Description { get; set; }
        public string? TypeSite { get; set; }
        public string? Statut { get; set; }
        public string? NombreEmployees { get; set; }
        public string? Capital { get; set; }
        public string? Adresse { get; set; }
        public string? Langitude { get; set; }
        public string? Largitude { get; set; }
        public string? Effectif { get; set; }
        public string? DateCreation { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? SiteWeb { get; set; }

        public int? EntrepriseId { get; set; }
        public Entreprise? Entreprise { get; set; }
        public int? VilleId { get; set; }
        public Ville? Ville { get; set; }
    }
}
