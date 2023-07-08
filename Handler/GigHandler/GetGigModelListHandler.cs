using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Query.GigModelQueries;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Handler.GigHandler
{
    public class GetGigModelListHandler : IRequestHandler<GetGigModelListQuery, List<GigModel>>
    {
        private readonly IGigModelService _gigService;

        public GetGigModelListHandler(IGigModelService gigService)
        {
            _gigService = gigService;
        }

        public async Task<List<GigModel>> Handle(GetGigModelListQuery request, CancellationToken cancellationToken)
        {
            return await _gigService.GetGigModelList();
        }
    }
}