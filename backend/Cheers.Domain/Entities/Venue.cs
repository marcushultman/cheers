using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cheers.Domain.Entities
{
    public class Venue
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset Created { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public class Configuration : IEntityTypeConfiguration<Venue>
        {
            public void Configure(EntityTypeBuilder<Venue> builder)
            {
                builder.HasIndex(x => x.Name)
                    .IsUnique();
            }
        }

    }
}
