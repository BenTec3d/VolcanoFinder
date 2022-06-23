namespace VolcanoFinder.API.Models.DTOs
{
    /// <summary>
    /// A DTO of a region without the volcanoes
    /// </summary>
    public class RegionWithoutVolcanoesDto
    {
        /// <summary>
        /// The id of the region
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the region
        /// </summary>
        public string Name { get; set; }

    }
}
