﻿// <auto-generated />
using System;
using EFDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SpanishWords.EntityFramework.Migrations
{
    [DbContext(typeof(WordsContext))]
    partial class WordsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SpanishWords.Models.GrammaticalGender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("varchar(9)");

                    b.HasKey("Id");

                    b.ToTable("GrammaticalGenders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Masculine"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Masculine"
                        });
                });

            modelBuilder.Entity("SpanishWords.Models.LexicalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("LexicalCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Noun"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Verb"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Adjective"
                        });
                });

            modelBuilder.Entity("SpanishWords.Models.Statistic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimesCorrect")
                        .HasColumnType("int");

                    b.Property<int>("TimesIncorrect")
                        .HasColumnType("int");

                    b.Property<int>("TimesTrained")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statistics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 2,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 3,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 4,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 5,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 6,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 7,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 8,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 9,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 10,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 11,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 12,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 13,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 14,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        },
                        new
                        {
                            Id = 15,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimesCorrect = 0,
                            TimesIncorrect = 0,
                            TimesTrained = 0
                        });
                });

            modelBuilder.Entity("SpanishWords.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "Luki",
                            Password = "1234"
                        },
                        new
                        {
                            Id = 2,
                            Login = "Zdzichu",
                            Password = "jabol1234"
                        },
                        new
                        {
                            Id = 3,
                            Login = "Miroslaw",
                            Password = "karpackieMocne"
                        });
                });

            modelBuilder.Entity("SpanishWords.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("English")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("GrammaticalGenderId")
                        .HasColumnType("int");

                    b.Property<int>("LexicalCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Spanish")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("StatisticId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrammaticalGenderId");

                    b.HasIndex("LexicalCategoryId");

                    b.HasIndex("StatisticId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Words");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            English = "car",
                            GrammaticalGenderId = 1,
                            LexicalCategoryId = 1,
                            Spanish = "coche",
                            StatisticId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            English = "cat",
                            GrammaticalGenderId = 1,
                            LexicalCategoryId = 1,
                            Spanish = "gato",
                            StatisticId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            English = "dog",
                            GrammaticalGenderId = 1,
                            LexicalCategoryId = 1,
                            Spanish = "perro",
                            StatisticId = 3,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SpanishWords.Models.Word", b =>
                {
                    b.HasOne("SpanishWords.Models.GrammaticalGender", "GrammaticalGender")
                        .WithMany("Words")
                        .HasForeignKey("GrammaticalGenderId");

                    b.HasOne("SpanishWords.Models.LexicalCategory", "LexicalCategory")
                        .WithMany("Words")
                        .HasForeignKey("LexicalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpanishWords.Models.Statistic", "Statistic")
                        .WithOne("Word")
                        .HasForeignKey("SpanishWords.Models.Word", "StatisticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpanishWords.Models.User", "User")
                        .WithMany("Words")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrammaticalGender");

                    b.Navigation("LexicalCategory");

                    b.Navigation("Statistic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpanishWords.Models.GrammaticalGender", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("SpanishWords.Models.LexicalCategory", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("SpanishWords.Models.Statistic", b =>
                {
                    b.Navigation("Word")
                        .IsRequired();
                });

            modelBuilder.Entity("SpanishWords.Models.User", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
