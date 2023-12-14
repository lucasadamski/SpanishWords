using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpanishWords.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccess.DataAccess
{


    public class WordsContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<LexicalCategory> LexicalCategories { get; set; }
        public DbSet<GrammaticalGender> GrammaticalGenders { get; set; }

        public WordsContext(DbContextOptions<WordsContext> options) : base(options) { }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Property(e => e.FirstName).HasMaxLength(250);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.LastName).HasMaxLength(250);

            /*
            modelBuilder.Entity<Statistic>().HasData(
                new Statistic { Id = 1, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 2, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 3, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 4, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 5, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 6, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 7, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 8, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 9, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 10, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 11, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 12, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 13, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 14, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0},
                new Statistic { Id = 15, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0}
            );

             modelBuilder.Entity<Word>().HasData(
                new Word { Id = 1, Spanish = "coche", English = "car", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = "1", StatisticId = 1},
                new Word { Id = 2, Spanish = "gato", English = "cat", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = "1", StatisticId = 2 },
                new Word { Id = 3, Spanish = "perro", English = "dog", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = "1", StatisticId = 3 }
                );
        */

            modelBuilder.Entity<LexicalCategory>().HasData(
                new LexicalCategory { Id = 1, Name = "Noun" },
                new LexicalCategory { Id = 2, Name = "Verb" },
                new LexicalCategory { Id = 3, Name = "Adjective" }
                );

            modelBuilder.Entity<GrammaticalGender>().HasData(
                new GrammaticalGender { Id = 1, Name = "Masculine" },
                new GrammaticalGender { Id = 2, Name = "Feminine" }
                );

           
        }

    }
}
