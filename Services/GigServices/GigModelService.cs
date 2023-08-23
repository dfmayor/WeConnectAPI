using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeConnectAPI.Data;
using WeConnectAPI.Models;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Services.GigServices
{
    public class GigModelService : IGigModelService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public GigModelService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<GigModel> CreateGigModel(GigModel gigModel)
        {
            var response = _dbContext.GigModels.Add(gigModel);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<bool> DeleteGigModel(GigModel gigModel)
        {
            try
            {
                var result = _dbContext.GigModels.Remove(gigModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<GigModel> GetGigModelById(Guid Id)
        {
            var gig = await _dbContext.GigModels.Where(g => g.GigId == Id)
                .Select(g => new GigModel
                {
                    CategoryId = g.CategoryId,
                    Category = g.Category,
                    GigReviews = g.GigReviews,
                    UserId = g.UserId,
                    User = g.User,
                    Title = g.Title,
                    Description = g.Description,
                    GigPicture = g.GigPicture,
                    Price = g.Price,
                    DeliveryTime = g.DeliveryTime,
                    Status = g.Status,
                    CreatedAt = g.CreatedAt,
                    UpdatedAt = g.UpdatedAt
                })
                .FirstOrDefaultAsync();
            if (gig != null)
            {
                return gig;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GigModel>> GetGigModelList()
        {
            var gigData = await _dbContext.GigModels.ToListAsync();
            foreach (var gig in gigData)
            {
                //var categoryId = gig.CategoryId;
                var category = await _dbContext.Categories.Where(g => g.CategoryId == gig.CategoryId).FirstOrDefaultAsync();
                var applicationUser = await _userManager.FindByIdAsync(gig.UserId);
                gig.Category = category;
                gig.User = applicationUser;
            }
            return gigData;
        }

        public async Task<GigModel> UpdateGigModel(GigModel gigModel)
        {
            var response = _dbContext.GigModels.Update(gigModel);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }
    }
}