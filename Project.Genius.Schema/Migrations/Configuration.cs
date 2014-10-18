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

			if (!context.ApplicationUsers.Any(u => u.UserName == "rsalayo@openit.com"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { Email = "rsalayo@openit.com" };

				manager.Create(user, "admin");
			}

			ApplicationUser createBy = context.ApplicationUsers.FirstOrDefault(u => u.UserName == "rsalayo@openit.com");

			context.ModuleTypes.AddOrUpdate(
				t => t.Name,
				new ModuleType { Name = "Server", },
				new ModuleType { Name = "Client", },
				new ModuleType { Name = "Single Feature", });

			context.SaveChanges();

			context.Modules.AddOrUpdate(
				m => m.Name,
				new Module
				{
					Name = "Base Professional",
					Type = context.ModuleTypes.FirstOrDefault(),
					Caption = "Install OpeniT Core Server",
					Description = "OpeniT Base Professional",
					CreateBy = createBy,
					CreatedOn = DateTime.Now,
					UpdateBy = createBy,
					UpdateOn = DateTime.Now
				},
				new Module
				{
					Name = "Base Enterprise",
					Type = context.ModuleTypes.FirstOrDefault(t => t.Name == "Server"),
					Caption = "Install OpeniT Analysis Server",
					Description = "OpeniT Base Enterprise",
					CreateBy = createBy,
					CreatedOn = DateTime.Now,
					UpdateBy = createBy,
					UpdateOn = DateTime.Now
				},
				new Module
				{
					Name = "LicenseAnalyzer",
					Type = context.ModuleTypes.FirstOrDefault(t => t.Name == "Client"),
					Caption = "Install OpeniT LicenseAnalyzer Client",
					Description = "OpeniT Collector [LicenseAnalyzer]",
					CreateBy = createBy,
					CreatedOn = DateTime.Now,
					UpdateBy = createBy,
					UpdateOn = DateTime.Now
				},
				new Module
				{
					Name = "Petrel Module Logging",
					Type = context.ModuleTypes.FirstOrDefault(t => t.Name == "Single Feature"),
					Caption = "Install OpeniT SystemAnalyzer Client",
					Description = "OpeniT Collector [SystemAnalyzer]",
					CreateBy = createBy,
					CreatedOn = DateTime.Now,
					UpdateBy = createBy,
					UpdateOn = DateTime.Now
				});

			context.SaveChanges();

			context.DefinedTasks.AddOrUpdate(
				t => t.Name,
				new DefinedTask
				{
					Name = "Install OpeniT CoreServer",
					Duration = 1,
					Module = context.Modules.FirstOrDefault(m => m.Name == "Base Professional"),
					Owner = createBy
				},
				new DefinedTask
				{
					Name = "Install OpeniT License File",
					Duration = .25,
					Module = context.Modules.FirstOrDefault(m => m.Name == "Base Professional"),
					Owner = createBy
				},
				new DefinedTask
				{
					Name = "Configure OpeniT CoreServer",
					Duration = 1,
					Module = context.Modules.FirstOrDefault(m => m.Name == "Base Professional"),
					Owner = createBy
				});

			context.SaveChanges();

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