﻿// <auto-generated />
using System;
using LiveScoreReporter.EFCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LiveScoreReporter.EFCore.Infrastructure.Migrations
{
    [DbContext(typeof(LiveScoreReporterDbContext))]
    [Migration("20250111151642_AddedPostionToPlayer")]
    partial class AddedPostionToPlayer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LineupPlayer", b =>
                {
                    b.Property<int>("LineupsId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("LineupsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("LineupPlayers", (string)null);
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssistPlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("Time")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssistPlayerId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", b =>
                {
                    b.Property<int>("FixtureId")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int?>("ScoreId")
                        .HasColumnType("int");

                    b.HasKey("FixtureId");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("ScoreId")
                        .IsUnique()
                        .HasFilter("[ScoreId] IS NOT NULL");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Lineup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("TeamId");

                    b.ToTable("Lineups");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Player", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Away")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Home")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LineupPlayer", b =>
                {
                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Lineup", null)
                        .WithMany()
                        .HasForeignKey("LineupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Event", b =>
                {
                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Player", "AssistPlayer")
                        .WithMany("AssistedEvents")
                        .HasForeignKey("AssistPlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", "Game")
                        .WithMany("Events")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Player", "Player")
                        .WithMany("Events")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", "Team")
                        .WithMany("Events")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssistPlayer");

                    b.Navigation("Game");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", b =>
                {
                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", "AwayTeam")
                        .WithMany("AwayGames")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", "HomeTeam")
                        .WithMany("HomeGames")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.League", "League")
                        .WithMany("Games")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Score", "Score")
                        .WithOne("Game")
                        .HasForeignKey("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", "ScoreId");

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("League");

                    b.Navigation("Score");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Lineup", b =>
                {
                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", "Game")
                        .WithMany("Lineups")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", "Team")
                        .WithMany("Lineups")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Game", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Lineups");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.League", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Player", b =>
                {
                    b.Navigation("AssistedEvents");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Score", b =>
                {
                    b.Navigation("Game")
                        .IsRequired();
                });

            modelBuilder.Entity("LiveScoreReporter.EFCore.Infrastructure.Entities.Team", b =>
                {
                    b.Navigation("AwayGames");

                    b.Navigation("Events");

                    b.Navigation("HomeGames");

                    b.Navigation("Lineups");
                });
#pragma warning restore 612, 618
        }
    }
}
