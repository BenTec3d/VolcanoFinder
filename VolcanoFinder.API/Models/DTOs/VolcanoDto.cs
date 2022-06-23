using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Models.DTOs
{
    public class VolcanoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string CountryAlpha2 { get; set; }

        public string? Description { get; set; }

        public DateTime? LastEruption { get; set; }
        public bool? Active { get; set; }
    }
}
