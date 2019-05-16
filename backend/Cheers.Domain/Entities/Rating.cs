using System;
using System.Collections.Generic;
using System.Text;

namespace Cheers.Domain.Entities
{
    public class Rating
    {
        public long Id { get; set; }

        public Score Score { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Category { get; set; }

        public Venue Venue { get; set; }
    }

    public enum Score
    {
        Negative = -1,
        Neutral = 0,
        Positive = 1
    }
}
