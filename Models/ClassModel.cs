using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace API.Models
{
    public class ClassModel
    {
        public int id { get; set; }
        
        [Required]
        public string? name { get; set; }

        public ICollection<Student> lst_student { get; set; } = new List<Student>();
    }
}