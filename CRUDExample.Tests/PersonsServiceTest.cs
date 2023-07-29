using System;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Xunit.Abstractions;

namespace CRUDExample.Tests
{
	public class PersonsServiceTest
	{
		private readonly IPersonsService _personsService;
		private readonly ICountriesService _countriesService;
		private readonly ITestOutputHelper _testOutputHelper;

		public PersonsServiceTest(ITestOutputHelper testOutputHelper, IPersonsService personsService, ICountriesService countriesService)
		{
			_personsService = personsService;
			_countriesService = countriesService;
			_testOutputHelper = testOutputHelper;
		}

		#region AddPerson

		// When we supply nulll value as PersonAddRequest, it should thorw ArgumentNullException
		[Fact]
		public async Task AddPerson_NullPerson()
		{
			// Arrange
			PersonAddRequest? request = null;

			// Assert
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await _personsService.AddPerson(request);
			});
		}

		// When you supply null value as prsonName, it should throw ArgumentException
		[Fact]
		public async Task AddPerson_PersonNameIsNull()
		{
			// Arrange
			PersonAddRequest? request = new PersonAddRequest()
			{
				PersonName = null
			};

			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				await _personsService.AddPerson(request);
			});
		}

		// When we supply with proper person details, it should insert the person into persons list; and it should return an object of PersonResponse, which includes with the newly generated guid
		[Fact]
		public async Task AddPerson_ProperPersonDetails()
		{
			// Arrange
			PersonAddRequest? request = new PersonAddRequest()
			{
				PersonName = "Person name...",
				Email = "person@email.com",
				Address = "sample address",
				CountryID = Guid.NewGuid(),
				Gender = GenderOptions.Male,
				DateOfBirth = DateTime.Parse("2000-01-01"),
				ReceiveNewsLetters = true
			};

			// Act
			PersonResponse response = await _personsService.AddPerson(request);

			List<PersonResponse> personsList = await _personsService.GetAllPersons();

			// Assert
			Assert.True(response.PersonID != Guid.Empty);
			Assert.Contains(response, personsList);
		}

		#endregion

		#region GetAllPersons

		// The GetAllPersons() should return an empty list by default
		[Fact]
		public async Task GetAllPersons_EmptyList()
		{
			// Act
			List<PersonResponse> personsResponsesList = await _personsService.GetAllPersons();

			// Assert
			Assert.Empty(personsResponsesList);
		}

		// The returns list of persons added
		[Fact]
		public async Task GetAllPersons_AddFewPersons()
		{
			// Arrange
			CountryAddRequest countryReq1 = new CountryAddRequest() { CountryName = "Cameroon" };
			CountryAddRequest countryReq2 = new CountryAddRequest() { CountryName = "Canada" };

			CountryResponse countryResponse1 = await _countriesService.AddCountry(countryReq1);
			CountryResponse countryResponse2 = await _countriesService.AddCountry(countryReq2);

            PersonAddRequest person_request_1 = new PersonAddRequest() { PersonName = "Smith", Email = "smith@example.com", Gender = GenderOptions.Male, Address = "address of smith", CountryID = countryResponse1.CountryID, DateOfBirth = DateTime.Parse("2002-05-06"), ReceiveNewsLetters = true };

            PersonAddRequest person_request_2 = new PersonAddRequest() { PersonName = "Mary", Email = "mary@example.com", Gender = GenderOptions.Female, Address = "address of mary", CountryID = countryResponse1.CountryID, DateOfBirth = DateTime.Parse("2000-02-02"), ReceiveNewsLetters = false };

            PersonAddRequest person_request_3 = new PersonAddRequest() { PersonName = "Rahman", Email = "rahman@example.com", Gender = GenderOptions.Male, Address = "address of rahman", CountryID = countryResponse2.CountryID, DateOfBirth = DateTime.Parse("1999-03-03"), ReceiveNewsLetters = true };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_request_1, person_request_2, person_request_3 };

            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest person_request in person_requests)
            {
                PersonResponse person_response = await _personsService.AddPerson(person_request);
                person_response_list_from_add.Add(person_response);
            }

			// Print person_response_list_from_add
			_testOutputHelper.WriteLine("Expected : ");
			foreach (PersonResponse person in person_response_list_from_add)
				_testOutputHelper.WriteLine(person.ToString());

			// Act
			List<PersonResponse> personsListFromGetAll = await _personsService.GetAllPersons();
            _testOutputHelper.WriteLine("Actual : ");
            foreach (PersonResponse person in personsListFromGetAll)
                _testOutputHelper.WriteLine(person.ToString());

            // Assert
            foreach (PersonResponse person in person_response_list_from_add)
			{
				Assert.Contains(person, personsListFromGetAll);
			}
        }

        #endregion

        #region GetFilteredPersons

        // If the seach text is empty and search by is "PersonName", it should return all persons

        #endregion
    }
}

