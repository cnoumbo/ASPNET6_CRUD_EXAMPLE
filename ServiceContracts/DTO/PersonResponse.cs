﻿using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTP class that is used as return type of most methods of Persons Service
    /// </summary>
	public class PersonResponse
	{
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;
            return PersonID == person.PersonID && PersonName == person.PersonName && Email == person.Email && DateOfBirth == person.DateOfBirth && Gender == person.Gender && CountryID == person.CountryID && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters;
        }

        public PersonUpdateRequest ToPersonUdpateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender != null ? (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender.ToString(), true) : null,
                Address = Address,
                CountryID = CountryID,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Person ID: {PersonID}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DateOfBirth?.ToString("dd MMM yyyy")}, Gender: {Gender}, Country ID: {CountryID}, Country: {Country}, Address: {Address}, Receive News Letters: {ReceiveNewsLetters}";
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            // Person => convert => PersonResponse
            return new PersonResponse()
            {
                PersonID = person.person_id,
                PersonName = person.person_name,
                Email = person.email,
                DateOfBirth = person.date_of_birth != null ? person.date_of_birth.Value.ToDateTime(TimeOnly.Parse("00:00 AM")) : null,
                ReceiveNewsLetters = person.receive_newsletters,
                Address = person.address,
                CountryID = person.country_id,
                Gender = person.gender,
                Age = (person.date_of_birth != null) ? (Math.Round((DateTime.Now - person.date_of_birth.Value.ToDateTime(TimeOnly.Parse("00:00 AM"))).TotalDays / 365.25)) : null,
                Country = person.country?.country_name
            };
        }
    }
}

