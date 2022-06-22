using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VolcanoFinder.API.Models.Entities
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Volcano> Volcanoes { get; set; } = new List<Volcano>();

        public Region(string name)
        {
            Name = name;
        }
    }
}
