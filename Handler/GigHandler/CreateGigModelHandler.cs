using MediatR;
using WeConnectAPI.Command.GigModelCommand;
using WeConnectAPI.Models;
using WeConnectAPI.Services.GigServices;

namespace WeConnectAPI.Handler.GigHandler
{
    public class CreateGigModelHandler : IRequestHandler<CreateGigModelCommand, GigModel>
    {
        private readonly IGigModelService _gigModelService;

        public CreateGigModelHandler(IGigModelService gigModelService)
        {
            _gigModelService = gigModelService;
        }

        public async Task<GigModel> Handle(CreateGigModelCommand request, CancellationToken cancellationToken)
        {
            GigModel gigModel = new GigModel()
            {
                CategoryId = request.Category.CategoryId,
                Category = request.Category,
                UserId = request.ApplicationUser.Id,
                User = request.ApplicationUser,
                Title = request.GigModel.Title,
                Description = request.GigModel.Description,
                GigPicture = request.GigModel.GigPicture,
                Price = request.GigModel.Price,
                DeliveryTime = request.GigModel.DeliveryTime,
                Status = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            GigModel createdGig = await _gigModelService.CreateGigModel(gigModel);
            return createdGig;
        }
    }
}