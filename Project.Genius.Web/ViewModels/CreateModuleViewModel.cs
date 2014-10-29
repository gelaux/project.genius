namespace Project.Genius.Web.ViewModels
{
	using System.Collections.Generic;

	using Schema.Entities;

	public class CreateModuleViewModel
	{
		#region Properties

		public Module Module { get; set; }

		public IEnumerable<ModuleType> ModuleTypes { get; set; }

		#endregion
	}
}