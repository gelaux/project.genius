namespace Project.Genius.Schema.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	using System.Linq;

	using Entities;

	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;

	internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		#region Constructors and Destructors

		public Configuration()
		{
			this.AutomaticMigrationsEnabled = false;
		}

		#endregion

		#region Methods

		protected override void Seed(DataContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			if (!context.ApplicationUsers.Any(u => u.UserName == "rsalayo@openit.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { Email = "rsalayo@openit.com" };

				manager.Create(user, "admin");
			}

			context.Modules.AddOrUpdate(
				m => m.Name,
				new Module
				{
					Name = "Base Pro",
					Type = Module.ModuleType.Server,
					Description = "OpeniT Base Professional",
					Owner = context.ApplicationUsers.FirstOrDefault(u => u.UserName == "rsalayo@openit.com")
				},
				new Module
				{
					Name = "Base Enterprise",
					Type = Module.ModuleType.Server,
					Description = "OpeniT Base Enterprise",
					Owner = context.ApplicationUsers.FirstOrDefault(u => u.UserName == "rsalayo@openit.com")
				},
				new Module
				{
					Name = "LicenseAnalyzer",
					Type = Module.ModuleType.Client,
					Description = "OpeniT Collector [LicenseAnalyzer]",
					Owner = context.ApplicationUsers.FirstOrDefault(u => u.UserName == "rsalayo@openit.com")
				});

			context.ProductVersions.AddOrUpdate(
				v => v.Name,
				new ProductVersion
				{
					Name = "7.0.0.25",
					ReleaseData = Convert.ToDateTime("2014-04-30"),
					Status = ProductVersion.State.ReleaseCandidate
				},
				new ProductVersion
				{
					Name = "6.3.0.5",
					ReleaseData = Convert.ToDateTime("2014-07-17"),
					Status = ProductVersion.State.Official
				},
				new ProductVersion
				{
					Name = "6.3.0.3",
					ReleaseData = Convert.ToDateTime("2014-03-07"),
					Status = ProductVersion.State.Cancelled
				},
				new ProductVersion
				{
					Name = "6.2.1.5",
					ReleaseData = Convert.ToDateTime("2013-10-04"),
					Status = ProductVersion.State.Official
				});
		}

		#endregion
	}
}