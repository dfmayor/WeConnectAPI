using MediatR;
using WeConnectAPI.Models;

namespace WeConnectAPI.Query.EducationQueries
{
    public class GetEducationListByIdQuery : IRequest<List<Education>>
    {
        public string Id { get; set; }
    }
}