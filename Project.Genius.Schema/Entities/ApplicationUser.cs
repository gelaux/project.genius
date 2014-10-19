namespace Project.Genius.Schema.Entities
{
	using System.Security.Claims;
	using System.Threading.Tasks;

	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;

	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
	{
		#region Properties

		public string DisplayName
		{
			get
			{
				return string.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		#endregion

		#region Public Methods

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		#endregion
	}
}