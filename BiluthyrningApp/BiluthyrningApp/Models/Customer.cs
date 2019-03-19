using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models
{
    public class Customer
    {
        public int Id { get; set; }        
        [Display(Name = "Personummer*")]
        [RegularExpression(@"^(?<date>\d{6}|\d{8})[-\s]?\d{4}$", ErrorMessage = "Personnummer anges med 10 siffror (yymmddnnnn)")]
        [Required(ErrorMessage = "Du måste ange personnummer")]
        [ValidateOfAge]
        public string SSN { get; set; }
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        public List<Booking> Bookings { get; set; }
    }

    internal class ValidateOfAge : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string RegExForValidation = @"^(?<date>\d{6}|\d{8})[-\s]?\d{4}$";
            string date = Regex.Match((string)value, RegExForValidation).Groups["date"].Value;
            DateTime dt;
            if (DateTime.TryParseExact(date, new[] { "yyMMdd", "yyyyMMdd" }, new CultureInfo("sv-SE"), DateTimeStyles.None, out dt))
                if (IsOfAge(dt))
                    return ValidationResult.Success;
                else
                {
                    return new ValidationResult("Du får endast boka en bil om du är över 18 år");

                }
            return new ValidationResult("Personnummer anges med 10 siffror (yymmddnnnn)");
        }

        public bool IsOfAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;      
            int age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age))
                age--;
            return age < 18 ? false : true;  
        }
    }
}
