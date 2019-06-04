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
        public async Task<ActionResult<IEnumerable<GetVenueModel>>> GetVenues()
        {
            var response = await DbContext.Venues
                .Select(v => new GetVenueModel
                {
                    Created = v.Created,
                    Id = v.Id,
                    Latitude = v.Latitude,
                    Longitude = v.Longitude,
                    Name = v.Name,
                    Ratings = v.Ratings.GroupBy(c => c.Category)
                                .Select(g => ValueTuple.Create<string, decimal>(g.Key, (decimal) g.Average(y => y.Score)))
                })
                .ToListAsync();

            return Ok(response);
        }

        [HttpGet]
        [Route("api/venues/current")]
        public async Task<ActionResult<Venue>> GetCurrentVenue()
        {
            var backDate = DateTimeOffset.UtcNow.AddHours(-2);

            var latestRating = await DbContext.Ratings
                .Where(x => x.Created > backDate)
                .Include(x => x.Venue)
                .OrderByDescending(x => x.Created)
                .FirstOrDefaultAsync();
            
            return Ok(latestRating?.Venue);
        }

        [HttpPost]
        [Route("api/venues")]
        public async Task<ActionResult<Venue>> CreateVenue(CreateVenueModel model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

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

        [HttpGet]
        [Route("api/venues/{id}/ratings")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetVenueRatings(long id)
        {
            return Ok(await DbContext.Ratings.Where(x => x.Venue.Id == id).ToListAsync());
        }
    }
}
