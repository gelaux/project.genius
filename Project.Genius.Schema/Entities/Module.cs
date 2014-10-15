namespace Project.Genius.Schema.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Module
	{
		#region Properties

		public string Caption { get; set; }

		public string Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public virtual ApplicationUser Owner { get; set; }

		public virtual ICollection<DefinedTask> Tasks { get; set; }

		#endregion
	}
}