using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Models.DTOs
{
    /// <summary>
    /// A DTO of a region with the volcanoes
    /// </summary>
    public class RegionDto
    {
        /// <summary>
        /// The id of the region
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the region
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The number of volcanoes in the region
        /// </summary>
        public int NumberOfVolcanoes
        {
            get
            {
                return Volcanoes.Count;
            }
        }

        /// <summary>
        /// A collection of volcanoes in that region
        /// </summary>
        public ICollection<VolcanoDto> Volcanoes { get; set; } = new List<VolcanoDto>();

    }
}
