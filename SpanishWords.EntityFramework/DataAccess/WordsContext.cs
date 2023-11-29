﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpanishWords.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Login = "Luki", Password = "1234" }, //EF ma specjalne prawa i może przypisywać wartości do pól readonly/seed
                new User { Id = 2, Login = "John", Password = "1234" },
                new User { Id = 3, Login = "Iggy", Password = "1234" }
            );

            modelBuilder.Entity<Statistic>().HasData(
                new Statistic { Id = 1, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 2, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 3, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 4, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 5, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 6, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 7, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 8, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 9, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 10, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 11, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 12, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 13, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 14, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 },
                new Statistic { Id = 15, CreateDate = new DateTime(), LastUpdated = new DateTime(), TimesCorrect = 0, TimesIncorrect = 0, TimesTrained = 0 }
            );

            modelBuilder.Entity<LexicalCategory>().HasData(
                new LexicalCategory { Id = 1, Name = "Noun" },
                new LexicalCategory { Id = 2, Name = "Verb" },
                new LexicalCategory { Id = 3, Name = "Adjective" }
                );

            modelBuilder.Entity<GrammaticalGender>().HasData(
                new GrammaticalGender { Id = 1, Name = "Masculine" },
                new GrammaticalGender { Id = 2, Name = "Masculine" }
                );

            modelBuilder.Entity<Word>().HasData(
                new Word { Id = 1, Spanish = "coche", English = "car", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = 1, StatisticId = 1},
                new Word { Id = 2, Spanish = "gato", English = "cat", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = 1, StatisticId = 2 },
                new Word { Id = 3, Spanish = "perro", English = "dog", LexicalCategoryId = 1, GrammaticalGenderId = 1, UserId = 1, StatisticId = 3 }
                );
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
