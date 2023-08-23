using Microsoft.AspNetCore.Identity;
using WeConnectAPI.Data;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace WeConnectAPI.Services.EducationServices
{
    public class EducationService : IEducationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EducationService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Education> CreateEducation(Education education)
        {
            var response = _dbContext.Educations.Add(education);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public Task<Education> DeleteEducation(Education education)
        {
            throw new NotImplementedException();
        }

        public Task<List<Education>> GetEducationByIdList(string Id)
        {
            var eduData = _dbContext.Educations.Where(e => e.ApplicationUserId.ToString() == Id)
                .Select(e => new Education
                {
                    ApplicationUserId = e.ApplicationUserId,
                    ApplicationUser = e.ApplicationUser,
                    School = e.School,
                    Qualification = e.Qualification,
                    Course = e.Course,
                    GraduationYear = e.GraduationYear,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();
            return eduData;
        }

        public async Task<Education> UpdateEducation(Education education)
        {
            var response = _dbContext.Educations.Update(education);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }
    }
}