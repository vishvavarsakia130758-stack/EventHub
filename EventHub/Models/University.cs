namespace EventHub.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }          // IIT / Central / State / Private / Deemed
        public int Established { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
    }
}
