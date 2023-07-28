  using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	/// <summary>
	/// Person domain model class
	/// </summary>
	public class Person
	{
		[Key]
		public Guid person_id { get; set; }

		[StringLength(40)]
		public string? person_name { get; set; }

		[StringLength(50)]
		public string? email { get; set; }

		[DataType(DataType.Date)]
		public DateOnly? date_of_birth { get; set; }

		[StringLength(10)]
		public string? gender { get; set; }

		public Guid? country_id { get; set; }

		[StringLength(200)]
		public string? address { get; set; }

		public bool receive_newsletters { get; set; }

		public string? TIN { get; set; }
	}
}

