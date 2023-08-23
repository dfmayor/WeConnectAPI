using MediatR;
using WeConnectAPI.Command.EducationCommand;
using WeConnectAPI.Models;
using WeConnectAPI.Services.EducationServices;

namespace WeConnectAPI.Handler.EducationHandler
{
    public class CreateEducationHandler : IRequestHandler<CreateEducationCommand, Education>
    {
        private readonly IEducationService _educationService;

        public CreateEducationHandler(IEducationService educationService)
        {
            _educationService = educationService;
        }

        public async Task<Education> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            Education education = new()
            {
                ApplicationUserId = request.ApplicationUser.Id,
                School = request.Education.School,
                Qualification = request.Education.Qualification,
                Course = request.Education.Course,
                GraduationYear = request.Education.GraduationYear,
                CreatedAt = DateTime.Now
            };
            Education createdEducation = await _educationService.CreateEducation(education);
            return createdEducation;
        }
    }
}