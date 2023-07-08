using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Query.GigModelQueries;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Handler.GigHandler
{
    public class GetGigModelByIdHandler : IRequestHandler<GetGigModelByIdQuery, GigModel>
    {
        private readonly IGigModelService _gigModelService;

        public GetGigModelByIdHandler(IGigModelService gigModelService)
        {
            _gigModelService = gigModelService;
        }

        public async Task<GigModel> Handle(GetGigModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _gigModelService.GetGigModelById(request.Id);
        }
    }
}