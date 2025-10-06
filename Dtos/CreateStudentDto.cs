using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateStudentDto
    {
        public string name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime dateofbirth { get; set; }
        public int class_id { get; set; }
    }
}