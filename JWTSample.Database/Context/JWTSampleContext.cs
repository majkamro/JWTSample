using JWTSample.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace JWTSample.Database.Context
{
	public class JWTSampleContext : DbContext
	{
		public JWTSampleContext()
		{

		}

		public JWTSampleContext(DbContextOptions<JWTSampleContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}