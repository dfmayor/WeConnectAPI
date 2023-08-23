using MediatR;
using WeConnectAPI.Command.EducationCommand;
using WeConnectAPI.Data;
using WeConnectAPI.Models;
using WeConnectAPI.Services.EducationServices;

namespace WeConnectAPI.Handler.EducationHandler;

public class UpdateEducationHandler : IRequestHandler<UpdateEducationCommand, Education>
{
    private readonly IEducationService _educationService;
    private readonly ApplicationDbContext _context;

    public UpdateEducationHandler(IEducationService educationService, ApplicationDbContext context)
    {
        _educationService = educationService;
        _context = context;
    }

    public async Task<Education> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        var education = _context.Educations.Where(u => u.ApplicationUserId == request.ApplicationUser.Id).FirstOrDefault();
        if (education != null)
        {
            education.School = request.Education.School;
            education.Qualification = request.Education.Qualification;
            education.Course = request.Education.Course;
            education.GraduationYear = request.Education.GraduationYear;
            Education updatedEdu = await _educationService.UpdateEducation(education);
            return updatedEdu;
        }
        else
        {
            return null;
        }
    }
}