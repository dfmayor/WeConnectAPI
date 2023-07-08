using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.GigModelCommand
{
    public class UpdateGigModelCommand : IRequest<GigModel>
    {
        public UpdateGigModelCommand(GigModel gigModel, ApplicationUser applicationUser, Category category)
        {
            GigModel = gigModel;
            ApplicationUser = applicationUser;
            Category = category;
        }

        public GigModel GigModel { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Category Category { get; set; }
    }
}