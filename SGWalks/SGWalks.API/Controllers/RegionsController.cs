using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGWalks.API.Models.Domain;
using SGWalks.API.Models.DTO;
using SGWalks.API.Repositories;

namespace SGWalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepositorty regionRepositorty;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepositorty regionRepositorty, IMapper mapper)
        {
            this.regionRepositorty = regionRepositorty;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {

            var regions = await regionRepositorty.GetAllAsync();
            //use of automapper ot map the objects 
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            // return DTo regions 

            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code= region.Code,
            //        Area= region.Area,
            //        Lat= region.Lat,    
            //        Long= region.Long,
            //        Population= region.Population,


            //    };


            //    regionsDTO.Add(regionDTO);
            //});


            //with Dummy data 

            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id =Guid.NewGuid(),
            //        Name="Wellington",
            //        Code="WLG",
            //        Area=227755,
            //        Lat=1.9990,
            //        Long=-1.8822,
            //        Population=500000,
            //    },
            //     new Region
            //    {
            //        Id =Guid.NewGuid(),
            //        Name="India",
            //        Code="IND",
            //        Area=227755,
            //        Lat=1.9990,
            //        Long=-1.8822,
            //        Population=500000,
            //    }

            //};

            return Ok(regions);
        }

        /// <summary>
        /// get region by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepositorty.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            mapper.Map<Models.DTO.Region>(region);
            return Ok(region);
        }

        /// <summary>
        /// add one region 
        /// </summary>
        /// <param name="addRegionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request to the Dpmain Model

            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,

            };
         
            // Pass details to Repositry

            region = await regionRepositorty.AddAsync(region);

            // convert back to DTO 



            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };



            return CreatedAtAction(nameof(GetRegionAsync),new {id=regionDTO.Id},regionDTO);

        }

        /// <summary>
        /// delete the region
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // get the region 
          var  region = await regionRepositorty.DeleteAsync(id);

            // check if null
            if(region == null)
            {
                return NotFound();
            }
            // change into dto 
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return Ok(regionDTO);
        }
        /// <summary>
        /// Update the Region
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdateRegionRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id ,[FromBody] Models.DTO.UpdateRegionRequest UpdateRegionRequest)
        {
            //convert dto to region obj 

            var region = new Models.Domain.Region
            {
               
                Code =      UpdateRegionRequest.Code,
                Area =      UpdateRegionRequest.Area,
                Name =       UpdateRegionRequest.Name,
                Lat =        UpdateRegionRequest.Lat,
                Long =       UpdateRegionRequest.Long,
                Population = UpdateRegionRequest.Population,
            };

            // update in db 


             region = await regionRepositorty.UpdateAsync(id, region);
            // if null 
            if(region == null)
            {
                return NotFound();  
            }

            // Convert Back to DTO 
            var regionDTO = new Models.DTO.Region
            {

                Code =       region.Code,
                Area =       region.Area,
                Name =       region.Name,
                Lat =        region.Lat,
                Long =       region.Long,
                Population = region.Population,
            };
            //var regionsDTO = mapper.Map<List<Models.DTO.Region>>(region);
            //AutoMapper.Mapper(regionupdated,Models.DTO.UpdateRegionRequest);

            return Ok(regionDTO);
        }

        

    }



}
