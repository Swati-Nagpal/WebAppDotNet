using InsuranceService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InsuranceService.Infrastructure.Repository
{
    public class DbConRepository : IDbContextFactory<SqlServerContext>
    {
        #region properties


        #endregion

        #region
        public DbConRepository()
        {

        }

        public SqlServerContext CreateDbContext()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            SqlServerContext obj = new SqlServerContext(connectionString);
            return obj;
        }


        #endregion



    }
}
