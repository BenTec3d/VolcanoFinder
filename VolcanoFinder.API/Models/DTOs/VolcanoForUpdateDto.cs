namespace VolcanoFinder.API.Models.DTOs
{
    /// <summary>
    /// A DTO for updating a volcano
    /// </summary>
    public class VolcanoForUpdateDto
    {
        /// <summary>
        /// The name of the volcano
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A link to an online resource of a picture of the volcano
        /// </summary>
        public string Picture { get; set; } = string.Empty;

        /// <summary>
        /// The ISO 3166-1 alpha-2 of the country the volcano is in
        /// </summary>
        public string? CountryAlpha2 { get; set; }

        /// <summary>
        /// A description of the volcano (max. 200 chars)
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The date of the last eruption
        /// </summary>
        public DateTime? LastEruption { get; set; }

        /// <summary>
        /// Whether the volcano is active or not
        /// </summary>
        public bool? Active { get; set; }
    }
}
