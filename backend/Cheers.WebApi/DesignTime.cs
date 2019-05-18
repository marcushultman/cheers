using Cheers.Domain;
using Cheers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cheers.WebApi
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CheersDbContext>
    {
        public CheersDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<CheersDbContext>();

            builder.ConfigureDatabase(configuration);

            return new CheersDbContext(builder.Options);
        }
    }
}
