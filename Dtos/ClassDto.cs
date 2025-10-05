namespace API.Dtos
{
    public class ClassDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public List<string>? students { get; set; }
    }
}