using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFDataAccess.DataAccess
{


    public class WordsContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<LexicalCategory> LexicalCategories { get; set; }
        public DbSet<GrammaticalGender> GrammaticalGenders { get; set; }

        public WordsContext(DbContextOptions<WordsContext> options) :base(options)
        {
            
        }


       /*protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            options.UseSqlServer(config.GetConnectionString("Default"));
        }*/
    }
}
