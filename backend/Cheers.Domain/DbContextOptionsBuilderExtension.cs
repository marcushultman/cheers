using Cheers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cheers.Domain
{
    public static class DbContextOptionsBuilderExtension
    {
        public static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            return builder.ConfigureDatabase(configuration.GetConnectionString(nameof(CheersDbContext)));
        }

        public static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.CommandTimeout(300);
                npgsql.MigrationsAssembly("Cheers.Domain");
            });

            return builder;
        }
    }
}
