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
    public class RatingController : ControllerBase
    {
        private CheersDbContext DbContext { get; }

        public RatingController(CheersDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet]
        [Route("api/ratings")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetVenues()
        {
            return Ok(await DbContext.Ratings.Include(x => x.Venue).ToListAsync());
        }

        [HttpPost]
        [Route("api/ratings")]
        public async Task<ActionResult<Rating>> AddRating(AddRatingModel model)
        {
            if (ModelState.IsValid == false)
                BadRequest(ModelState);

            var rating = DbContext.Ratings.Add(new Rating
            {
                Category = model.Category,
                Score = model.Score,
                Timestamp = DateTimeOffset.UtcNow,
                Venue = await DbContext.Venues.FindAsync(model.VenueId)
            });

            await DbContext.SaveChangesAsync();

            return Ok(rating);
        }

        //[HttpPost]
        //[Route("api/ratings")]
        //public async Task<ActionResult<Venue>> CreateRa(CreateVenueModel model)
        //{
        //    if (ModelState.IsValid == false)
        //        BadRequest(ModelState);

        //    var venue = new Venue
        //    {
        //        Created = DateTimeOffset.UtcNow,
        //        Latitude = model.Latitude,
        //        Longitude = model.Longitude,
        //        Name = model.Name
        //    };

        //    DbContext.Venues.Add(venue);
        //    await DbContext.SaveChangesAsync();
        //    return Ok(venue);
        //}

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
