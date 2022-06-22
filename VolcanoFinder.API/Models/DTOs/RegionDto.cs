using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Models.DTOs
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfVolcanoes
        {
            get
            {
                return Volcanoes.Count;
            }
        }

        public ICollection<Volcano> Volcanoes { get; set; } = new List<Volcano>();

    }
}
