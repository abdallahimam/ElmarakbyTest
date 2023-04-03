using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElmarakbyTest.Models
{
    public class EmployerModel
    {
        public int? Id { get; set; }

        [Required , StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required]
        public float Salary { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? Image { get; set; }
    }
}
