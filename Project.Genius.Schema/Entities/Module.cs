namespace Project.Genius.Schema.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Module
	{
		#region Properties

		public string Caption { get; set; }

		[Display(Name = "Created By")]
		public virtual ApplicationUser CreateBy { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Created On")]
		public DateTime CreatedOn { get; set; }

		public string Description { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public bool Optional { get; set; }

		public virtual ICollection<DefinedTask> Tasks { get; set; }

		public virtual ModuleType Type { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Updated By")]
		public virtual ApplicationUser UpdateBy { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Updated On")]
		public DateTime UpdateOn { get; set; }

		#endregion
	}
}