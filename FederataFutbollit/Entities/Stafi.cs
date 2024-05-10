namespace FederataFutbollit.Entities
{
    public class Stafi
    {
        public int Id { get; set; }
        public string Emri { get; set; } = string.Empty;
        public string Mbiemri { get; set; } = string.Empty;
        public string Pozita { get; set; } = string.Empty;
        public int Paga { get; set; }
        public string Email { get; set; } = string.Empty;   
        public int Telefoni { get; set; }
    }
}
