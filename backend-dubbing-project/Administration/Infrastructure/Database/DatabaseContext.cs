using Microsoft.EntityFrameworkCore;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Infrastructure.Database
{
    internal partial class DubbingContext : DbContext
    {
        public DbSet<Performance> Performances { get; set; }

        public DbSet<Audio> Audio { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Speech> Speeches { get; set; }

        public DubbingContext(DbContextOptions<DubbingContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=dubbing.db");
        }
    }
}