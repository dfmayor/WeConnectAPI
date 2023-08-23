using WeConnectAPI.Models;

namespace WeConnectAPI.Services.EducationServices
{
    public interface IEducationService
    {
        Task<Education> CreateEducation(Education education);
        Task<List<Education>> GetEducationByIdList(string Id);
        Task<Education> UpdateEducation(Education education);
        Task<Education> DeleteEducation(Education education);
    }
}