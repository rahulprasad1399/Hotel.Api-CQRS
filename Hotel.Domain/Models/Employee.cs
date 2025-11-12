using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(8)]
        public string? Password { get; set; }
        public int? HotelId { get; set; }
        public Hotel Hotel { get; set; }

    }
}
