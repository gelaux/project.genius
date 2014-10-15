namespace Project.Genius.Schema.Migrations
{
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

			if (!context.ApplicationUsers.Any(u => u.UserName == "admin"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser { Email = "rsalayo@openit.com" };

				manager.Create(user, "admin");
			}
		}

		#endregion
	}
}