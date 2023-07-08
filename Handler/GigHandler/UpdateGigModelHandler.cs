using MediatR;
using WeConnectAPI.Command.GigModelCommand;
using WeConnectAPI.Data;
using WeConnectAPI.Models;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Handler.GigHandler
{
    public class UpdateGigModelHandler : IRequestHandler<UpdateGigModelCommand, GigModel>
    {
        private readonly IGigModelService _gigModelService;
        private readonly ApplicationDbContext _dbContext;

        public UpdateGigModelHandler(IGigModelService gigModelService, ApplicationDbContext dbContext)
        {
            _gigModelService = gigModelService;
            _dbContext = dbContext;
        }
        public async Task<GigModel> Handle(UpdateGigModelCommand request, CancellationToken cancellationToken)
        {
            var gigModel = _dbContext.GigModels.Where(g => g.GigId == request.GigModel.GigId).FirstOrDefault();
            
            if (gigModel != null)
            {
                gigModel.CategoryId = request.Category.CategoryId;
                gigModel.Category = request.Category;
                gigModel.UserId = request.ApplicationUser.Id;
                gigModel.User = request.ApplicationUser;
                gigModel.Title = request.GigModel.Title;
                gigModel.Description = request.GigModel.Description;
                gigModel.GigPicture = request.GigModel.GigPicture;
                gigModel.DeliveryTime = request.GigModel.DeliveryTime;
                gigModel.Status = request.GigModel.Status;
                gigModel.UpdatedAt = DateTime.Now;
                GigModel updatedGig = await _gigModelService.UpdateGigModel(gigModel);
                return updatedGig;
            }
            else
                return null;

        }
    }
}