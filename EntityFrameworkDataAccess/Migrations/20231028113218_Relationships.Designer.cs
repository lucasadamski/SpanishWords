﻿// <auto-generated />
using System;
using EFDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameworkDataAccess.Migrations
{
    [DbContext(typeof(WordsContext))]
    [Migration("20231028113218_Relationships")]
    partial class Relationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WordId")
                        .IsUnique();

                    b.ToTable("Statistics");
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

                    b.Property<int?>("GenderId")
                        .HasColumnType("int");

                    b.Property<int>("GrammaticalGenderId")
                        .HasColumnType("int");

                    b.Property<int>("LexicalCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("LexicalId")
                        .HasColumnType("int");

                    b.Property<string>("Spanish")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrammaticalGenderId");

                    b.HasIndex("LexicalCategoryId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("UserWord", b =>
                {
                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.Property<int>("WordsId")
                        .HasColumnType("int");

                    b.HasKey("UsersId", "WordsId");

                    b.HasIndex("WordsId");

                    b.ToTable("UserWord");
                });

            modelBuilder.Entity("SpanishWords.Models.Statistic", b =>
                {
                    b.HasOne("SpanishWords.Models.User", "User")
                        .WithMany("Statistics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpanishWords.Models.Word", "Word")
                        .WithOne("Statistic")
                        .HasForeignKey("SpanishWords.Models.Statistic", "WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("SpanishWords.Models.Word", b =>
                {
                    b.HasOne("SpanishWords.Models.GrammaticalGender", "GrammaticalGender")
                        .WithMany("Words")
                        .HasForeignKey("GrammaticalGenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpanishWords.Models.LexicalCategory", "LexicalCategory")
                        .WithMany("Words")
                        .HasForeignKey("LexicalCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrammaticalGender");

                    b.Navigation("LexicalCategory");
                });

            modelBuilder.Entity("UserWord", b =>
                {
                    b.HasOne("SpanishWords.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpanishWords.Models.Word", null)
                        .WithMany()
                        .HasForeignKey("WordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpanishWords.Models.GrammaticalGender", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("SpanishWords.Models.LexicalCategory", b =>
                {
                    b.Navigation("Words");
                });

            modelBuilder.Entity("SpanishWords.Models.User", b =>
                {
                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("SpanishWords.Models.Word", b =>
                {
                    b.Navigation("Statistic")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
