using MediatR;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Command.EducationCommand
{
    public class CreateEducationCommand : IRequest<Education>
    {
        public CreateEducationCommand(Education education, ApplicationUser applicationUser)
        {
            Education = education;
            ApplicationUser = applicationUser;
        }

        public Education Education { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}