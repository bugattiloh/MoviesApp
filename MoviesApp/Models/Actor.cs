using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        public Actor()
        {
            //Id?
            Name = "";
            LastName = "";
            //Birthday?

        }
    }
}
