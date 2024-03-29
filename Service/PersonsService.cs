﻿using Entities;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Service
{
	public class PersonsService : IPersonsService
	{
        private readonly PersonsDbContext _db;
        private readonly ICountriesService _countriesService;

        public PersonsService(PersonsDbContext db, ICountriesService countriesService)
        {
            _db = db;
            _countriesService = countriesService;
        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
                throw new ArgumentNullException("person can't be null");

            // Validation through Model Validation
            // case 1
            ValidationHelper.ModelValidation(personAddRequest);


            // convert PersonAddRequest into Person type
            Person person = personAddRequest.ToPerson();
            person.person_id = Guid.NewGuid();
            await _db.Persons.AddAsync(person);
            await _db.SaveChangesAsync();
            //_db.sp_InsertPerson(person);

            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetAllPersons()
        {
            var persons = await _db.Persons.Include("country").ToListAsync();

            return await _db.Persons
                .Select(t => t.ToPersonResponse()).ToListAsync();
            //return _db.sp_GetAllPersons().
            //    Select(ConvertToPersonResponse).ToList();
        }

        public async Task<List<PersonResponse>> GetFilteredPersons(string? searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = await GetAllPersons();
            List<PersonResponse> matchingPersons = allPersons;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingPersons;

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName):
                    matchingPersons = allPersons.Where(tmp => (!string.IsNullOrEmpty(tmp.PersonName) ? tmp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.DateOfBirth):
                    matchingPersons = allPersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PersonResponse.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                default:
                    matchingPersons = allPersons;
                    break;
            }
            return matchingPersons;
        }

        public async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = await _db.Persons.FirstOrDefaultAsync(tmp => tmp.person_id == personID);
            if (person == null)
                return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return allPersons;

            List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(tmp => tmp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(tmp => tmp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),

                (nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),

                _ => allPersons
            };

            return sortedPersons;
        }

        public async Task<PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(PersonUpdateRequest));

            // Validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            // get marching person object to update
            Person? matchingPerson = await _db.Persons.FirstAsync(tmp => tmp.person_id == personUpdateRequest.PersonID);
            if (matchingPerson == null)
                throw new ArgumentException("Given person id doesn't exist");

            // Convert PersonUpdate to Person
            var person = personUpdateRequest.ToPerson();

            // Update all details
            matchingPerson.person_name = personUpdateRequest.PersonName;
            matchingPerson.email = personUpdateRequest.Email;
            matchingPerson.date_of_birth = personUpdateRequest.DateOfBirth != null ? DateOnly.FromDateTime(personUpdateRequest.DateOfBirth.Value) : null;
            matchingPerson.gender = personUpdateRequest.Gender.ToString();
            matchingPerson.country_id = personUpdateRequest.CountryID;
            matchingPerson.address = personUpdateRequest.Address;
            matchingPerson.receive_newsletters = personUpdateRequest.ReceiveNewsLetters;

            await _db.SaveChangesAsync();

            //_db.sp_UpdatePerson(person);

            return person.ToPersonResponse();
        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = await _db.Persons.FirstOrDefaultAsync(tmp => tmp.person_id == personID);
            if (person == null)
                return false;

            _db.Persons.Remove(person);

            await _db.SaveChangesAsync();
            //_db.sp_DeletePerson(personID.Value);

            return true;
        }
    }
}

