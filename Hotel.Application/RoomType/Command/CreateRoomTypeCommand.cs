using Azure;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.CreateRoomType
{
    public class CreateRoomTypeCommand : IRequest<int>
    {
        [Required]
        public string TypeName { get; set; }
        public string Description { get; set; }
        [Required]
        public int? Capacity { get; set; }
    }
}
