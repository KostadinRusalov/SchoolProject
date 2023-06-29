using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Studio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Headquarters { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Founded { get; set; }

        public List<Anime> Animes { get; set; } = new List<Anime>();

        public Studio()
        {
        }
    }
}
