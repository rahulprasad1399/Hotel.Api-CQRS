using Hotel.Application.ReviewGetAll;
using Hotel.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.GetAllReviews
{
    public class GetAllReviewQuery : IRequest<List<ReviewGetAllDto>>
    {

    }
}
