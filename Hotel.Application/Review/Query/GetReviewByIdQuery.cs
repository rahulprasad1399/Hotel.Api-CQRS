using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetReviewById
{
    public class GetReviewByIdQuery : IRequest<Review>
    {
        public int Id { get; set; }
    }
}
