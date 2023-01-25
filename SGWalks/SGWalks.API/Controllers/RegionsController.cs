using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SGWalks.API.Models.Domain;
using SGWalks.API.Repositories;

namespace SGWalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepositorty regionRepositorty;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepositorty regionRepositorty , IMapper mapper)
        {
            this.regionRepositorty = regionRepositorty;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

           var regions = await regionRepositorty.GetAllAsync();
            //use of automapper ot map the objects 
            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);

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



    }



}
