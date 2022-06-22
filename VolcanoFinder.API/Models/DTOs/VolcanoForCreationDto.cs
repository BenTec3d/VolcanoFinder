namespace VolcanoFinder.API.Models.DTOs
{
    public class VolcanoForCreationDto
    {
        public string Name { get; set; }

        public string Picture { get; set; }

        public string CountryAlpha2 { get; set; }

        public string? Description { get; set; }

        public DateTime? LastEruption { get; set; }
    }
}
