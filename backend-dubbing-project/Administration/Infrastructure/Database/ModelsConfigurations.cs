using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database
{
    internal partial class DubbingContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>()
                .HasMany(p => p.Speeches)
                .WithOne(p => p.Performance)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Performance>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Performance>()
                .Property(p => p.Title)
                .IsRequired();

            modelBuilder.Entity<Performance>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder
                .Entity<Speech>()
                .HasMany(s => s.Audios)
                .WithOne(s => s.Speech)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Speech>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Speech>()
                .Property(s => s.Order)
                .IsRequired();

            modelBuilder.Entity<Speech>()
                .Property(s => s.Text)
                .IsRequired();

            modelBuilder.Entity<Speech>()
                .Property(s => s.Duration)
                .IsRequired();

            modelBuilder.Entity<Language>()
                .HasMany(l => l.Audios)
                .WithOne(a => a.Language)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<Language>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Language>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Audio>()
                .Property(a => a.FileName)
                .IsRequired();

            modelBuilder.Entity<Audio>()
                .Property(a => a.OriginalText)
                .IsRequired();
        }
    }
}