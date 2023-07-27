using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Acts as a DTO for inserting a new Person
	/// </summary>
	public class PersonAddRequest
	{
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Of Birth can't be empty")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage="You must select one option")]
        public GenderOptions Gender { get; set; }

        [Required(ErrorMessage = "Country can't be empty")]
        public Guid? CountryID { get; set; }

        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current object of PersonAddRequest into a new object of Person tyle
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            return new Person()
            {
                person_name = PersonName,
                email = Email,
                date_of_birth = DateOfBirth != null ? DateOnly.FromDateTime(DateOfBirth.Value) : null,
                gender = Gender.ToString(),
                country_id = CountryID,
                address = Address,
                receive_newsletters = ReceiveNewsLetters
            };
        }
    }
}

