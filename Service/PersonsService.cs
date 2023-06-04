using System;
using System.ComponentModel.DataAnnotations;
using Entities;
using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Service
{
	public class PersonsService : IPersonsService
	{
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;

        public PersonsService(bool initialize = true)
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();

            if(initialize)
            {
                _persons.Add(new Person() {
                    PersonID = Guid.Parse("D3508E95-68C7-4E1C-9DEC-E64525E7D53F"),
                    PersonName = "Alwin",
                    Email = "acoverlyn0@who.int",
                    DateOfBirth = DateTime.Parse("1914-02-12"),
                    Gender = "Male",
                    Address = "Room 806",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("34C3732D-D071-4EE3-ACDB-2E079C5A7944") });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("F79B33FD-8227-4BA4-A934-CE3AE012A499"),
                    PersonName = "Nikolai",
                    Email = "narmatage1@fc2.com",
                    DateOfBirth = DateTime.Parse("1972-11-25"),
                    Gender = "Male",
                    Address = "Room 357",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("34C3732D-D071-4EE3-ACDB-2E079C5A7944")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("F4788020-63E9-4C39-8C66-341D790AAF76"),
                    PersonName = "Justis",
                    Email = "jrennocks2@imdb.com",
                    DateOfBirth = DateTime.Parse("1947-08-23"),
                    Gender = "Male",
                    Address = "Suite 84",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("8ADEC2F3-917D-4B24-BA06-C3DAA4F40A66")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("9DBA04B8-2EAC-4552-AD1E-6FADCC07D8D3"),
                    PersonName = "Egor",
                    Email = "eabrahamowitcz3@goodreads.com",
                    DateOfBirth = DateTime.Parse("1933-08-04"),
                    Gender = "Male",
                    Address = "Apt 242",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("8ADEC2F3-917D-4B24-BA06-C3DAA4F40A66")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("C64860D9-ABFE-448C-8D77-87DB98DDBE79"),
                    PersonName = "Jerad",
                    Email = "jhail4@mozilla.com",
                    DateOfBirth = DateTime.Parse("1907-04-09"),
                    Gender = "Male",
                    Address = "Room 602",
                    ReceiveNewsLetters = false,
                    CountryID = Guid.Parse("28498692-292D-4DE3-A1C5-13881CAFFC74")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("ADB723A5-199E-43DB-A559-9C8063F6858F"),
                    PersonName = "Howie",
                    Email = "hdevote5@globo.com",
                    DateOfBirth = DateTime.Parse("1977-10-15"),
                    Gender = "Male",
                    Address = "PO Box 97871",
                    ReceiveNewsLetters = false,
                    CountryID = Guid.Parse("8ADEC2F3-917D-4B24-BA06-C3DAA4F40A66")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("E720DCAB-BBED-449C-8186-185C8B185E83"),
                    PersonName = "Ashlan",
                    Email = "ajobbins6@unc.edu",
                    DateOfBirth = DateTime.Parse("1936-11-08"),
                    Gender = "Female",
                    Address = "Apt 1519",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("50845956-E212-419D-89F3-ED51608A580B")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("89102F8F-78F3-42AA-BFD4-63D11CAF12DA"),
                    PersonName = "Marion",
                    Email = "msilburn7@shareasale.com",
                    DateOfBirth = DateTime.Parse("1947-01-28"),
                    Gender = "Female",
                    Address = "Suite 29",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("95DC472B-C209-4A41-B92B-618B023F6D34")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("C5D7CECE-48E0-407D-9662-E2AF08D1CB20"),
                    PersonName = "Ricard",
                    Email = "rbeste8@wsj.com",
                    DateOfBirth = DateTime.Parse("1901-09-02"),
                    Gender = "Male",
                    Address = "Suite 64",
                    ReceiveNewsLetters = true,
                    CountryID = Guid.Parse("95DC472B-C209-4A41-B92B-618B023F6D34")
                });
                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("3A314210-725F-4ACE-AEE5-7F37DF17A63D"),
                    PersonName = "Brennen",
                    Email = "bcondict9@gmpg.org",
                    DateOfBirth = DateTime.Parse("1956-05-01"),
                    Gender = "Male",
                    Address = "Suite 66",
                    ReceiveNewsLetters = false,
                    CountryID = Guid.Parse("95DC472B-C209-4A41-B92B-618B023F6D34")
                });
            }
        }

        private PersonResponse ConvertToPersonResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            personResponse.Country = _countriesService.GetCountryByCountryID(person.CountryID)?.CountryName;
            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            if (personAddRequest == null)
                throw new ArgumentNullException("person can't be null");

            // Validation through Model Validation
            // case 1
            ValidationHelper.ModelValidation(personAddRequest);


            // convert PersonAddRequest into Person type
            Person person = personAddRequest.ToPerson();
            person.PersonID = Guid.NewGuid();
            _persons.Add(person);

            return ConvertToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _persons.Select(temp => ConvertToPersonResponse(temp)).ToList();
        }

        public List<PersonResponse> GetFilteredPersons(string? searchBy, string? searchString)
        {
            List<PersonResponse> allPersons = GetAllPersons();
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

        public PersonResponse? GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = _persons.FirstOrDefault(tmp => tmp.PersonID == personID);
            if (person == null)
                return null;

            return ConvertToPersonResponse(person);
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

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(PersonUpdateRequest));

            // Validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            // get marching person object to update
            Person? matchingPerson = _persons.FirstOrDefault(tmp => tmp.PersonID == personUpdateRequest.PersonID);
            if (matchingPerson == null)
                throw new ArgumentException("Given person id doesn't exist");

            // Update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

            return ConvertToPersonResponse(matchingPerson);
        }

        public bool DeletePerson(Guid? personID)
        {
            if (personID == null)
                throw new ArgumentNullException(nameof(personID));

            Person? person = _persons.FirstOrDefault(tmp => tmp.PersonID == personID);
            if (person == null)
                return false;

            _persons.RemoveAll(tmp => tmp.PersonID == personID);
            return true;
        }
    }
}

