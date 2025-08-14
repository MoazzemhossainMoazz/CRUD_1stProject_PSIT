using System.ComponentModel.DataAnnotations.Schema;

namespace FirstProject.Models
{
    public class EmployeeModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Contact { get; set; }

        public string? PicturePath { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }

    }
}
