using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rentalbackend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rentalbackend.Entities
{
    public class AuthDbContext : IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            var DbInfo = ConfigurationHelper.GetAppSettings<DbInfo>("DbConnection");

            string server = DbInfo.Server;
            string databaseName = DbInfo.DbName;
            string databaseUser = DbInfo.DbUser;
            string databasePass = DbInfo.DbPassword;
            string pooling = DbInfo.Pooling;

            return $"Server={server};" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling={pooling};";

        }
    }

    class DbInfo {
        public string Server { get; set; }
        public string DbName { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string Pooling { get; set; }
    }
}
