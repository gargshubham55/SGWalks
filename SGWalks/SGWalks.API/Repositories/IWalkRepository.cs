using SGWalks.API.Models.Domain;

namespace SGWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk>   UpdateAsync(Guid id , Walk walk);   

        Task<Walk> DeleteWalkAsync(Guid id);


    }
}
