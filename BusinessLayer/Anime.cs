using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Anime
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Genre { get; set; }

        [Display(Name = "Studio")]
        public int StudioId { get; set; }

        public Studio Studio { get; set; }

        public List<Character> Characters { get; set; } = new List<Character>();

        public Anime()
        {
        }
    }
}
