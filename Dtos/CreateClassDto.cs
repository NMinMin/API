namespace API.Dtos
{
    public class CreateClassDto
    {
        public string name { get; set; } = string.Empty;
        public ICollection<Student> lst_student{ get; set; } = new List<Student>();
    }
}