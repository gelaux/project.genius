namespace Project.Genius.Schema.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class ProductVersion
	{
		#region Enums

		public enum State
		{
			[Display(Name = "Release Candidate")]
			ReleaseCandidate,

			Official,

			Cancelled
		};

		#endregion

		#region Properties

		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime ReleaseData { get; set; }

		public State Status { get; set; }

		#endregion
	}
}