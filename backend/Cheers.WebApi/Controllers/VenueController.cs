using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cheers.Domain.Entities;
using Cheers.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cheers.WebApi.Controllers
{
    [ApiController]
    public class VenueController : ControllerBase
    {
        private CheersDbContext DbContext { get; }

        public VenueController(CheersDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        [Route("api/venues")]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenues()
        {
            return Ok(await DbContext.Venues.ToListAsync());
        }

        [HttpPost]
        [Route("api/venues")]
        public async Task<ActionResult<Venue>> CreateVenue(CreateVenueModel model)
        {
            if (ModelState.IsValid == false)
                BadRequest(ModelState);

            var venue = new Venue
            {
                Created = DateTimeOffset.UtcNow,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Name = model.Name
            };

            DbContext.Venues.Add(venue);
            await DbContext.SaveChangesAsync();
            return Ok(venue);
        }

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
