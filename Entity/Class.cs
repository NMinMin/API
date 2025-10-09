using System.Text.Json.Serialization;

public class Class
{
        [JsonIgnore]
        public int id { get; set; }

        public string name { get; set; }

        [JsonIgnore]
        public ICollection<Student> lst_student{ get; set; } = new List<Student>();
  
}