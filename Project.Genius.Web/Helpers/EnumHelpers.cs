namespace Project.Genius.Web.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Globalization;
	using System.Linq;
	using System.Web.Mvc;

	public static class EnumHelpers
	{
		#region Public Methods

		public static IEnumerable<SelectListItem> GetItems(this Type enumType)
		{
			if (!typeof(Enum).IsAssignableFrom(enumType))
			{
				throw new ArgumentException("Type must be an enum");
			}

			var names = Enum.GetNames(enumType);
			var values = Enum.GetValues(enumType).Cast<int>();

			var items = names.Zip(values, (name, value) =>
				new SelectListItem
				{
					Text = GetName(enumType, name),
					Value = value.ToString(CultureInfo.InvariantCulture)
				});

			return items;
		}

		private static string GetName(Type enumType, string name)
		{
			var result = name;
			var attriute = enumType.GetField(name)
				.GetCustomAttributes(false)
				.OfType<DisplayAttribute>()
				.FirstOrDefault();

			if (attriute != null)
			{
				result = attriute.GetName();
			}

			return result;
		}

		#endregion
	}
}