using MediatR;
using WeConnectAPI.Models;

namespace WeConnectAPI.Query.GigModelQueries
{
    public class GetGigModelByIdQuery : IRequest<GigModel>
    {
        public Guid Id { get; set; }
    }
}