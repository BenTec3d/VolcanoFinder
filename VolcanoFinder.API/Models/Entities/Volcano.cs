using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VolcanoFinder.API.Models.Entities
{
    public class Volcano
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Picture { get; set; }

        [Required]
        [MaxLength(2)]
        public string CountryAlpha2 { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public DateTime? LastEruption { get; set; }

        public bool? Active { get; set; }

        [Required]
        [ForeignKey("RegionId")]
        public Region Region { get; set; }
        public int RegionId { get; set; }


        public Volcano(string name, string picture, int regionId)
        {
            Name = name;
            Picture = picture;
            RegionId = regionId;
        }

    }
}
