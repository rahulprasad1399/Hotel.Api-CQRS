using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string Image {  get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
