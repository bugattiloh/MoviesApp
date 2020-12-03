using System;
using System.ComponentModel.DataAnnotations;


namespace MoviesApp.Models
{
    public class Actor
    {
       public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

    }
}
