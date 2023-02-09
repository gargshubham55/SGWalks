using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGWalks.API.Data;
using SGWalks.API.Models.DTO;
using SGWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace SGWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper autoMapper;
        public WalksController(IWalkRepository _walkRepository, IMapper _autoMapper)
        {
            walkRepository = _walkRepository;
            autoMapper = _autoMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {

            //fetc data from db 

            var walksDomain = await walkRepository.GetAllAsync();

            // Convert  to DTO walks 

            //var walkDTO = new Models.DTO.Walk()
            //{
            //    ID = walksDomain.Name;

            //};
            var walkDTO = autoMapper.Map<List<Models.DTO.Walk>>(walksDomain);


            return Ok(walkDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {

            var walkDomain = await walkRepository.GetAsync(id);

            // convert into DTO 
            var WalkDTO = autoMapper.Map<Models.DTO.Walk>(walkDomain);


            // return response 
            return Ok(WalkDTO);

        }

        // Add walk async 
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(Models.DTO.AddWalkRequest walk)
        {
            // convert into domain from DTO
            try
            {
                //var DomainWalk = autoMapper.Map<Models.Domain.Walk>(walk);
                var DomainWalk = new Models.Domain.Walk()
                {
                    Name = walk.Name,
                    Length = walk.Length,
                    RegionId = walk.RegionId,
                    WalkDifficultyId = walk.WalkDifficultyId

                };

                var walksresult = await walkRepository.AddWalkAsync(DomainWalk);

                var WalkDTO = autoMapper.Map<Models.DTO.Walk>(walksresult);
                return CreatedAtAction(nameof(GetWalkAsync), new { id = WalkDTO.ID }, WalkDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
            // add the alk in db 
            // var walksresult = await walkRepository.AddWalkAsync(DomainWalk);

            // convert back to dto 
            //  var WalkDTO = autoMapper.Map<Models.DTO.Walk>(walksresult);


            //   return CreatedAtAction(AddWalkAsync,);


        }


        [HttpPut]
        [Route("{id=guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // convert dto to domain 
            var domainwalk = new Models.Domain.Walk()

            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId

            };


            //fetch data from db 
            domainwalk = await walkRepository.UpdateAsync(id, domainwalk);

            if (domainwalk == null)
            {
                return NotFound();
            }
            else
            {
                var DTODomain = new Models.Domain.Walk()
                {
                    Name = domainwalk.Name,
                    Length = domainwalk.Length,
                    RegionId = domainwalk.RegionId,
                    WalkDifficultyId = domainwalk.WalkDifficultyId
                };



                return Ok(domainwalk);
            }





        }


        [HttpDelete]
        [Route("{id=guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
          

            // delete from db 
         var  WalkDomain = await  walkRepository.DeleteWalkAsync(id);

            


            if (WalkDomain == null)
            {
                return NotFound();  
            }
            else
            {
                // convert back to dto and return 
                var WalkDTO = autoMapper.Map<Models.DTO.Walk>(WalkDomain);
                return Ok(WalkDTO);
            }
          


        }
    
    
    }
}
