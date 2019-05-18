using Cheers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cheers.WebApi.Models
{
    public class AddRatingModel
    {
        [Required]
        public long VenueId { get; set; }

        public IEnumerable<ScoreModel> Ratings { get; set; }

        public class ScoreModel
        {
            public int Score { get; set; }

            [Required]
            public string Category { get; set; }
        }
    }
}