namespace Project.Genius.Web.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	public class ExternalLoginConfirmationViewModel
	{
		#region Properties

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		#endregion
	}

	public class ExternalLoginListViewModel
	{
		#region Properties

		public string Action { get; set; }

		public string ReturnUrl { get; set; }

		#endregion
	}

	public class ManageUserViewModel
	{
		#region Properties

		[DataType(DataType.Password)]
		[Display(Name = "Confirm new password")]
		[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "New password")]
		public string NewPassword { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Current password")]
		public string OldPassword { get; set; }

		#endregion
	}

	public class LoginViewModel
	{
		#region Properties

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }

		#endregion
	}

	public class RegisterViewModel
	{
		#region Properties

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		#endregion
	}

	public class ResetPasswordViewModel
	{
		#region Properties

		public string Code { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		#endregion
	}

	public class ForgotPasswordViewModel
	{
		#region Properties

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		#endregion
	}
}