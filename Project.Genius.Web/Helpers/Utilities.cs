namespace Project.Genius.Web.Helpers
{
	using System;
	using System.Reflection;

	using Schema;
	using Schema.Entities;

	public class Utilities
	{
		#region Public Methods

		public static Object SetPropertyValue(Object entity, string property, string value, DataContext db = null)
		{
			PropertyInfo propertyInfo = entity.GetType().GetProperty(property);

			if (propertyInfo != null)
			{
				Type t = propertyInfo.PropertyType;
				object d = null;
				if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					if (String.IsNullOrEmpty(value))
					{
						d = null;
					}
					else
					{
						d = Convert.ChangeType(value, t.GetGenericArguments()[0]);
					}
				}
				else if (t == typeof(Guid))
				{
					d = new Guid(value);
				}
				else if (t == typeof(ModuleType))
				{
					if (db != null)
					{
						d = db.ModuleTypes.Find(Convert.ToInt32(value));
					}
				}
				else
				{
					d = Convert.ChangeType(value, t);
				}

				propertyInfo.SetValue(entity, d, null);
			}
			return entity;
		}

		#endregion
	}
}