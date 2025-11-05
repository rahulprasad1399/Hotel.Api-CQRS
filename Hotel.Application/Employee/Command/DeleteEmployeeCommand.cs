using MediatR;

namespace Hotel.Application.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
