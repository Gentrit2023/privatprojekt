namespace FederataFutbollit.Entities
{
    public class Selektori
    {
        public int Id { get; set; }
        public required string Emri { get; set; }
        public required string Mbiemri { get; set; }
        public int Mosha { get; set; }
    }
}