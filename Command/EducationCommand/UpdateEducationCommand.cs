using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.EducationCommand
{
    public class UpdateEducationCommand : IRequest<Education>
    {
        public UpdateEducationCommand(Education education, ApplicationUser applicationUser)
        {
            Education = education;
            ApplicationUser = applicationUser;
        }
        public Education Education { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
