using System;
using System.Collections.Generic;
using System.Text;

namespace Cheers.Domain.Entities
{
    public class Rating
    {
        public long Id { get; set; }

        public int Score { get; set; }

        public DateTimeOffset Created { get; set; }

        public string Category { get; set; }

        public Venue Venue { get; set; }
    }
}
