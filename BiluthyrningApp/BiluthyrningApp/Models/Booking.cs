using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        [RegularExpression(@"^[+]?\d+([.]\d+)?$", ErrorMessage = "Du måste ange hur långt kuden har kört (ken ej vara negativt)")]
        [Display(Name = "Antal körda kilometer*")]
        public decimal Mileage { get; set; }
        [Display(Name = "Tid för hömtning*")]
        [Required(ErrorMessage = "Du måste ange när bilen ska hämtas")]
        public DateTime BookingDateAndTime { get; set; }
        [Display(Name = "Tid för återlämning*")]
        [Required(ErrorMessage = "Du måste ange när bilen lämnas")]
        public DateTime ReturnDateAndTime { get; set; } = DateTime.Now.Date;
        [Display(Name = "Pris")]
        public decimal Price { get; set; }
    }
}
