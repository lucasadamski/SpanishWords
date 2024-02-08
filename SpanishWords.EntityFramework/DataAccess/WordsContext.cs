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
using SpanishWords.Models.Tables;

namespace EFDataAccess.DataAccess
{


    public class WordsContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<LexicalCategory> LexicalCategories { get; set; }
        public DbSet<GrammaticalGender> GrammaticalGenders { get; set; }
        public DbSet<StudyEntry> StudyEntries { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<HelperType> HelperTypes { get; set; }


        public WordsContext(DbContextOptions<WordsContext> options) : base(options) { }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Property(e => e.FirstName).HasMaxLength(250);
            modelBuilder.Entity<ApplicationUser>().Property(e => e.LastName).HasMaxLength(250);

            modelBuilder.Entity<HelperType>().HasData(
                new HelperType { Id = 1, Name = "None"},
                new HelperType { Id = 2, Name = "Letter" },
                new HelperType { Id = 3, Name = "Sentence" }
                );

            modelBuilder.Entity<AnswerType>().HasData(
                new AnswerType { Id = 1, Name = "Text" },
                new AnswerType { Id = 2, Name = "Quiz" },
                new AnswerType { Id = 3, Name = "Card" }
                );

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
