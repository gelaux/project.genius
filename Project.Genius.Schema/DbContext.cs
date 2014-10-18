namespace Project.Genius.Schema
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	using Entities;

	using Microsoft.AspNet.Identity.EntityFramework;

	public class DataContext : DbContext
	{
		#region Constructors and Destructors

		public DataContext()
			: base("DefaultConnection")
		{
		}

		#endregion

		#region Properties

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		public DbSet<DefinedTask> DefinedTasks { get; set; }

		public DbSet<ModuleType> ModuleTypes { get; set; }

		public DbSet<Module> Modules { get; set; }

		public DbSet<ProductVersion> ProductVersions { get; set; }

		#endregion

		#region Public Methods

		public static DataContext Create()
		{
			return new DataContext();
		}

		#endregion

		#region Methods

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			//modelBuilder.Entity<Project>().HasRequired(c => c.Owner).WithMany().WillCascadeOnDelete(false);
			//modelBuilder.Entity<Project>().HasRequired(c => c.CreatedBy).WithMany().WillCascadeOnDelete(false);
			//modelBuilder.Entity<Project>().HasRequired(c => c.UpdatedBy).WithMany().WillCascadeOnDelete(false);
			//modelBuilder.Entity<Package>().HasRequired(c => c.CreatedBy).WithMany().WillCascadeOnDelete(false);

			modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
			modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
			modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
		}

		#endregion
	}
}