using System.ComponentModel.DataAnnotations;

namespace AlimBio.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string? Siren { get; set; }
        [Display(Name = "Nom de l'entreprise")]
        public string? NomEntreprise { get; set; }
        [Display(Name = "Description d'activité")]
        public string? DescriptionActivite { get; set; }
        [Display(Name = "Personne Morale")]
        public bool? PersonneMoral { get; set; }
        [Display(Name = "SIRET de Siege")]
        public string? SiretSiege { get; set; }
        public string? Nic { get; set; }
        [Display(Name = "N° de voie")]
        public string? NumeroVoie { get; set; }
        [Display(Name = "Libelle de voie")]
        public string? LibelleVoie { get; set; }
        [Display(Name = "Complement d'adresse")]
        public string? ComplementAdresse { get; set; }
        [Display(Name = "Adresse complet")]
        public string? AdresseComplet { get; set; }
        [Display(Name = "Code postal")]
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public string? Region { get; set; }
        public string? Lattitude { get; set; }
        public string? Langitude { get; set; }
        [Display(Name = "Date de création")]
        public DateOnly DateCreation { get; set; }
        [Display(Name = "Recrute actuellement")]
        public bool? EntrepriseEmployeuse { get; set; }
        public float? Capital { get; set; }
        public int? Effectif { get; set; }
        [Display(Name = "Inscrit dans registre commercial (RC)")]
        public string? InscritRcs { get; set; }
        [Display(Name = "Site web")]
        public string? SiteWeb { get; set; }
        [Display(Name = "Immatriculation TVA")]
        public string? ImmatriculationTva { get; set; }

        public ICollection<Salarie>? Salaries { get; set; }
        public ICollection<Service>? Services { get; set; }
        public ICollection<Site>? Sites { get; set; }
    }
}
