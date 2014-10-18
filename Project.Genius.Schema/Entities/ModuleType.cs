namespace Project.Genius.Schema.Entities
{
	using System.Collections.Generic;

	public class ModuleType
	{
		#region Properties

		public int Id { get; set; }

		public virtual ICollection<Module> Modules { get; set; }

		public string Name { get; set; }

		#endregion
	}
}