namespace API.Dtos
{
    public class StudentDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? dateofbirth { get; set; } = string.Empty;
        public int class_id { get; set; }
        public string? class_name { get; set; }
    }
}