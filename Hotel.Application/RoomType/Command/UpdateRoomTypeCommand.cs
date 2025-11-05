using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Application.UpdateRoomType
{
    public class UpdateRoomTypeCommand : IRequest<RoomType>
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        public string Description { get; set; }
        [Required]
        public int? Capacity { get; set; }
    }
}
