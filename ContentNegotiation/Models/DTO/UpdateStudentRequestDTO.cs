using System.ComponentModel.DataAnnotations;

namespace ContentNegotiation.Models.DTO
{
    public class UpdateStudentRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
