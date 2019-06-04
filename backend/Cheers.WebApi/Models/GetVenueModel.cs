using Cheers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheers.WebApi.Models
{
    public class GetVenueModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }
        
        public IEnumerable<(string, decimal)> Ratings { get; set; }
    }
}
