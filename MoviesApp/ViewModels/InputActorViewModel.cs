using MoviesApp.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.ViewModels
{
    public class InputActorViewModel
    {
        [LengthFilters]
        public string Name { get; set; }

        [LengthFilters]
        public string LastName { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}