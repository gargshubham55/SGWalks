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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await sGWalksContext.Regions.AddAsync(region);
            await sGWalksContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await sGWalksContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }
            //delete 

            sGWalksContext.Regions.Remove(region);
            await sGWalksContext.SaveChangesAsync();
           
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await sGWalksContext.Regions.ToListAsync();

        }

        public async Task<Region> GetAsync(Guid id)
        {
             return  await sGWalksContext.Regions.FirstOrDefaultAsync(x=> x.Id == id); 
            
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await sGWalksContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code =region.Code;
            existingRegion.Name =region.Name;
            existingRegion.Area =region.Area;
            existingRegion.Lat=region.Lat;  
            existingRegion.Long= region.Long;
            existingRegion.Population= region.Population;

            sGWalksContext.SaveChangesAsync(); 
            
                return existingRegion;


            


        }
    }
}
