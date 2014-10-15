namespace Project.Genius.Schema.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Module
	{
		#region Enums

		public enum ModuleType
		{
			Server,

			Client,

			[Display(Name = "Single Feature")]
			SingleFeature
		};

		#endregion

		#region Properties

		public string Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool Optional { get; set; }

		public virtual ApplicationUser Owner { get; set; }

		public virtual ICollection<DefinedTask> Tasks { get; set; }

		public ModuleType Type { get; set; }

		#endregion
	}
}