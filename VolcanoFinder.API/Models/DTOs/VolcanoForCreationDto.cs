namespace VolcanoFinder.API.Models.DTOs
{
    public class VolcanoForCreationDto
    {
        public string Name { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public string? CountryAlpha2 { get; set; }

        public string? Description { get; set; }

        public DateTime? LastEruption { get; set; }
        public bool? Active { get; set; }
    }
}
