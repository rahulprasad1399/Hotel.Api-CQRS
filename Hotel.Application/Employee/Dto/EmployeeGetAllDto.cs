using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.EmployeeGetAll
{
    public class EmployeeGetAllDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role {  get; set; }
        public int? HotelId { get; set; }
        public string HotelName { get; set; }
    }
}
