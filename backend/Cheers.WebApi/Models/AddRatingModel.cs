using Cheers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cheers.WebApi.Models
{
    public class AddRatingModel
    {
        public long VenueId { get; set; }

        public Score Score { get; set; }

        public string Category { get; set; }
    }
}
