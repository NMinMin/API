using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;

namespace API.Models
{
    public class StudentModel
    {
        public int id { get; set; }

        public string? name { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateofbirth { get; set; }

        public int class_id { get; set; }
        
        [JsonIgnore]//Không hiện
        public Class? _class { get; set; }
    }
}