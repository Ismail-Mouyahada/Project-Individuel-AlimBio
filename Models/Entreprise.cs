namespace AlimBio.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string? Siren { get; set; }
        public string? NomEntreprise { get; set; }
        public string? DescriptionActivite { get; set; }
        public bool? PersonneMoral { get; set; }
        public string? SiretSiege { get; set; }
        public string? Nic { get; set; }
        public string? NumeroVoie { get; set; }
        public string? LibelleVoie { get; set; }
        public string? ComplementAdresse { get; set; }
        public string? AdresseComplet { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Region { get; set; }
        public string? Lattitude { get; set; }
        public string? Langitude { get; set; }
        public DateOnly DateCreation { get; set; }
        public bool? EntrepriseEmployeuse { get; set; }
        public float? Capital { get; set; }
        public int? Effectif { get; set; }
        public string? InscritRcs { get; set; }
        public string? SiteWeb { get; set; }
        public string? ImmatriculationTva { get; set; }

        public ICollection<Salarie>? Salaries { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Site>? Sites { get; set; }
    }
}
