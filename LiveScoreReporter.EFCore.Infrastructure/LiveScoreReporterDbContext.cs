using LiveScoreReporter.EFCore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreReporter.EFCore.Infrastructure
{
    public class LiveScoreReporterDbContext : DbContext
    {
        public LiveScoreReporterDbContext(DbContextOptions<LiveScoreReporterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Lineup> Lineups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(g => g.FixtureId);

            modelBuilder.Entity<Game>()
                .Property(g => g.FixtureId)
                .ValueGeneratedNever();

            modelBuilder.Entity<League>()
                .Property(l => l.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Team>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Player>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g=>g.AwayTeam)
                .WithMany(t=>t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Score)
                .WithOne(s => s.Game)
                .HasForeignKey<Score>(g => g.GameId);

            modelBuilder.Entity<Game>()
                .HasOne(g=>g.League)
                .WithMany(l=>l.Games)
                .HasForeignKey(g=>g.LeagueId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Game)
                .WithMany(g => g.Events)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Team)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Player)
                .WithMany(p => p.Events)
                .HasForeignKey(e => e.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.AssistPlayer)
                .WithMany(p => p.AssistedEvents) 
                .HasForeignKey(e => e.AssistPlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(s => s.Game)
                .WithOne(g => g.Score)
                .HasForeignKey<Game>(g => g.ScoreId);
            
           
            modelBuilder.Entity<Lineup>()
                .HasOne(l => l.Game)             
                .WithMany(g => g.Lineups)        
                .HasForeignKey(l => l.GameId);   

            
            modelBuilder.Entity<Lineup>()
                .HasMany(l => l.Players)         
                .WithMany(p => p.Lineups)        
                .UsingEntity(j => j.ToTable("LineupPlayers"));
            modelBuilder.Entity<Lineup>()
                .HasOne(l => l.Team)
                .WithMany(t => t.Lineups)
                .HasForeignKey(l => l.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
