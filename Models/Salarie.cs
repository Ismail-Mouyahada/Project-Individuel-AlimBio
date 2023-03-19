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
    }
}
