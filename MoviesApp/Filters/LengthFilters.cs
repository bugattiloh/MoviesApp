using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Filters
{
    public class LengthFilters : ValidationAttribute
    {

        public LengthFilters()  {}

        public string OurError() => "Must be more 4 letters!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = (string)value;
            if (str.Length < 4)
            {
                return new ValidationResult(OurError());
            }

            return ValidationResult.Success;
        }
    }
}
