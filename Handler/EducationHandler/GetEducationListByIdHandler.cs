using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Query.EducationQueries;
using WeConnectAPI.Services.EducationServices;

namespace WeConnectAPI.Handler.EducationHandler
{
    public class GetEducationListByIdHandler : IRequestHandler<GetEducationListByIdQuery, List<Education>>
    {
        private readonly IEducationService _educationService;

        public GetEducationListByIdHandler(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<List<Education>> Handle(GetEducationListByIdQuery request, CancellationToken cancellationToken)
        {
            List<Education> educationList = await _educationService.GetEducationByIdList(request.Id);
            return educationList;
        }
    }
}