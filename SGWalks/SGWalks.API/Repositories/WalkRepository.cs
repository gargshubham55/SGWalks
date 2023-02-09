using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGWalks.API.Data;
using SGWalks.API.Models.Domain;

namespace SGWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly SGWalksContext sGWalksContext;
        public WalkRepository(SGWalksContext _sGWalksContext)
        {
            this.sGWalksContext = _sGWalksContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
          walk.ID = Guid.NewGuid();
          this.sGWalksContext.Add(walk);
          await  this.sGWalksContext.SaveChangesAsync();
          return walk;

        }

        public async Task<Walk>  DeleteWalkAsync(Guid id)
        {
            var resultWalk = await this.sGWalksContext.Walks.FirstOrDefaultAsync(x => x.ID == id);
            if(resultWalk==null)
            {
                return null;
            }
            this.sGWalksContext.Remove(resultWalk);
           await this.sGWalksContext.SaveChangesAsync();
            return resultWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
        return await sGWalksContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();

        }

        public async Task<Walk> GetAsync(Guid Id)
        {

            return await sGWalksContext.Walks.Include(x => x.Region)
                .Include(x=>x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x=> x.ID == Id);

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
           var walkdomain = await sGWalksContext.Walks.FirstOrDefaultAsync( x => x.ID == id);

            if(walkdomain==null) 
            
            { return null; }

            walkdomain.Name = walk.Name;
            walkdomain.Length = walk.Length;
            walkdomain.RegionId = walk.RegionId;
            walkdomain.WalkDifficultyId = walk.WalkDifficultyId;

            sGWalksContext.SaveChangesAsync();

            return walkdomain;


        }
    }
}
