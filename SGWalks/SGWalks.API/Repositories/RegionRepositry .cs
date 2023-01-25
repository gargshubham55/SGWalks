using Microsoft.EntityFrameworkCore;
using SGWalks.API.Data;
using SGWalks.API.Models.Domain;

namespace SGWalks.API.Repositories
{
    public class RegionRepositry : IRegionRepositorty
    {
        private readonly SGWalksContext sGWalksContext;
        public RegionRepositry(SGWalksContext _SGWalksContext)
        {
            this.sGWalksContext = _SGWalksContext;
        } 
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await sGWalksContext.Regions.ToListAsync();

        }
    }
}
