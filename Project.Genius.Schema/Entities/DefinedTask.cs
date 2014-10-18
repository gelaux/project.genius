namespace Project.Genius.Schema.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;

	public class DefinedTask
	{
		#region Properties

		public double Duration { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string Instruction { get; set; }

		public Module Module { get; set; }

		public string Name { get; set; }

		public virtual ApplicationUser Owner { get; set; }

		#endregion
	}
}