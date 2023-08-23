using MediatR;
using WeConnectAPI.Command.GigModelCommand;
using WeConnectAPI.Data;
using WeConnectAPI.Models;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Handler.GigHandler
{
    public class DeleteGigModelHandler : IRequestHandler<DeleteGigModelCommand, bool>
    {
        private readonly IGigModelService _gigModelService;
        private readonly ApplicationDbContext _dbContext;

        public DeleteGigModelHandler(IGigModelService gigModelService, ApplicationDbContext dbContext)
        {
            _gigModelService = gigModelService;
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteGigModelCommand request, CancellationToken cancellationToken)
        {
            var model = _dbContext.GigModels.Where(m => m.GigId.ToString() == request.Id).FirstOrDefault();
            if (model != null)
            {
                await _gigModelService.DeleteGigModel(model);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}