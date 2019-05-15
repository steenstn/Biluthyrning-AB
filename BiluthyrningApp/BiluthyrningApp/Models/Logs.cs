using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiluthyrningApp.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string Log { get; set; }
    }
}
