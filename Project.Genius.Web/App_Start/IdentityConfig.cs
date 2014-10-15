namespace Project.Genius.Web
{
	using System.Threading.Tasks;

	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using Microsoft.AspNet.Identity.Owin;
	using Microsoft.Owin;
	using Microsoft.Owin.Security.DataProtection;

	using Schema;
	using Schema.Entities;

	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

	public class ApplicationUserManager : UserManager<ApplicationUser>
	{
		#region Constructors and Destructors

		public ApplicationUserManager(IUserStore<ApplicationUser> store)
			: base(store)
		{
		}

		#endregion

		#region Public Methods

		public static ApplicationUserManager Create(
			IdentityFactoryOptions<ApplicationUserManager> options,
			IOwinContext context)
		{
			var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<DataContext>()));
			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 8,
				RequireNonLetterOrDigit = false,
				RequireDigit = true,
				RequireLowercase = false,
				RequireUppercase = false,
			};
			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			manager.RegisterTwoFactorProvider(
				"PhoneCode",
				new PhoneNumberTokenProvider<ApplicationUser> { MessageFormat = "Your security code is: {0}" });
			manager.RegisterTwoFactorProvider(
				"EmailCode",
				new EmailTokenProvider<ApplicationUser> { Subject = "Security Code", BodyFormat = "Your security code is: {0}" });
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}

		#endregion
	}

	public class EmailService : IIdentityMessageService
	{
		#region Implemented Interfaces

		#region IIdentityMessageService

		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your email service here to send an email.
			return Task.FromResult(0);
		}

		#endregion

		#endregion
	}

	public class SmsService : IIdentityMessageService
	{
		#region Implemented Interfaces

		#region IIdentityMessageService

		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your sms service here to send a text message.
			return Task.FromResult(0);
		}

		#endregion

		#endregion
	}
}