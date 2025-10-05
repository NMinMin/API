public class Class
{
        public int id { get; set; }

        public string name { get; set; }

        public ICollection<Student> lst_student{ get; set; } = new List<Student>();
  
}