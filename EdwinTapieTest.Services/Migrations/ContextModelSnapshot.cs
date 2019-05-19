﻿// <auto-generated />
using System;
using EdwinTapieTest.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdwinTapieTest.Services.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EdwinTapieTest.Services.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("EdwinTapieTest.Services.Models.GameMove", b =>
                {
                    b.Property<int>("GameMoveId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("GameId");

                    b.Property<int?>("PlayerOneId");

                    b.Property<string>("PlayerOneMove");

                    b.Property<int?>("PlayerTwoId");

                    b.Property<string>("PlayerTwoMove");

                    b.Property<int>("Round");

                    b.Property<int?>("Winner");

                    b.HasKey("GameMoveId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerOneMove");

                    b.HasIndex("PlayerTwoId");

                    b.HasIndex("PlayerTwoMove");

                    b.HasIndex("Winner");

                    b.ToTable("GameMoves");
                });

            modelBuilder.Entity("EdwinTapieTest.Services.Models.Move", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Code");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("EdwinTapieTest.Services.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlayerName");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("EdwinTapieTest.Services.Models.GameMove", b =>
                {
                    b.HasOne("EdwinTapieTest.Services.Models.Game", "Game")
                        .WithMany("GameMoves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EdwinTapieTest.Services.Models.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId");

                    b.HasOne("EdwinTapieTest.Services.Models.Move", "MoveOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneMove");

                    b.HasOne("EdwinTapieTest.Services.Models.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId");

                    b.HasOne("EdwinTapieTest.Services.Models.Move", "MoveTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoMove");

                    b.HasOne("EdwinTapieTest.Services.Models.Player", "WinnerPlayer")
                        .WithMany()
                        .HasForeignKey("Winner");
                });
#pragma warning restore 612, 618
        }
    }
}