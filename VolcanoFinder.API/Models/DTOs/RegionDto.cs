using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Models.DTOs
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int NumberOfVolcanoes
        {
            get
            {
                return Volcanoes.Count;
            }
        }

        public ICollection<VolcanoDto> Volcanoes { get; set; } = new List<VolcanoDto>();

    }
}
