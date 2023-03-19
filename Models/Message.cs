namespace AlimBio.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Sujet { get; set; }
        public string? Details { get; set; }

        public int? SalarieId { get; set; }
        public  Salarie? Salarie { get; set; }
    }
}
