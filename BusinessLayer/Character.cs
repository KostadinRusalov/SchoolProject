using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Anime")]
        public int AnimeId { get; set; }

        public Anime Anime { get; set; }

        public Character()
        {
        }
    }
}
