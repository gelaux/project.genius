namespace Project.Genius.Web.ViewModels
{
	using System.Collections.Generic;

	using Schema.Entities;

	public class ModuleViewModels
	{
		#region Properties

		public ICollection<Module> Modules { get; set; }

		public ICollection<ModuleType> ModuleTypes { get; set; }

		public ICollection<ProductVersion> ProductVersions { get; set; }

		#endregion
	}
}