using SGWalks.API.Models.Domain;

namespace SGWalks.API.Repositories
{
    public interface IRegionRepositorty
    {

        Task<IEnumerable<Region>> GetAllAsync();


    }
}
