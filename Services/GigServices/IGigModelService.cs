using WeConnectAPI.Models;

namespace WeConnectAPI.Services.GigServices
{
    public interface IGigModelService
    {
        Task<List<GigModel>> GetGigModelList();
        Task<GigModel> GetGigModelById(Guid Id);
        Task<GigModel> CreateGigModel(GigModel gigModel);
        Task<GigModel> UpdateGigModel(GigModel gigModel);
        Task<GigModel> DeleteGigModel(GigModel gigModel);

    }
}