using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Dtos
{
    public class PutStudentDto
    {
        public string name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime dateofbirth { get; set; }
        [JsonIgnore]
        public int class_id { get; set; }
    }
}