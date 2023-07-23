using System;
using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class PersonUpdateRequest
	{
        [Required(ErrorMessage = "Person ID can't be epmty")]
        public Guid PersonID { get; set; }

        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date Of Birth can't be empty")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must select one option")]
        public GenderOptions? Gender { get; set; }

        [Required(ErrorMessage ="Country Field can't be null")]
        public Guid? CountryID { get; set; }
        
        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Converts the current object of PersonAddRequest into a new object of Person type
        /// </summary>
        /// <returns>Returns Person object</returns>
        public Person ToPerson()
        {
            return new Person() { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth != null ? DateOnly.FromDateTime(DateOfBirth.Value) : null, Gender = Gender.ToString(), Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }
}

