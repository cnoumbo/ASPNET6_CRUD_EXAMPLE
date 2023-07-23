﻿  using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
	/// <summary>
	/// Person domain model class
	/// </summary>
	public class Person
	{
		[Key]
		public Guid PersonID { get; set; }

		[StringLength(40)]
		public string? PersonName { get; set; }

		[StringLength(50)]
		public string? Email { get; set; }

		[DataType(DataType.Date)]
		public DateOnly? DateOfBirth { get; set; }

		[StringLength(10)]
		public string? Gender { get; set; }

		public Guid? CountryID { get; set; }

		[StringLength(200)]
		public string? Address { get; set; }

		public bool ReceiveNewsLetters { get; set; }
	}
}

