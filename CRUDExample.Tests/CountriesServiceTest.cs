using System;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUDExample.Tests
{
	public class CountriesServiceTest
	{
		private readonly ICountriesService _countriesService;

		// Constructor
		public CountriesServiceTest(ICountriesService countriesService)
		{
			_countriesService = countriesService;

        }

		#region AddCountry
		// When CountryAddRequest is null, it should throw ArgumentNullException
		[Fact]
		public void AddCountry_NullCountry()
		{
			// Arrange
			CountryAddRequest? request = null;

			// Assert
			Assert.Throws<ArgumentNullException>(() =>
			{
				// Act
				_countriesService.AddCountry(request);
			});
		}

		// When the countryNam is null, it should throw ArgumentException
		[Fact]
		public async Task AddCountry_CountryNameIsNull()
		{
			// Arrange
			CountryAddRequest? request = new CountryAddRequest() { CountryName = null };

			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				await _countriesService.AddCountry(request);
			});
		}

		// When you supply proper country name, it should insert (add) the country to the existing list of countries
		[Fact]
		public async Task AddCountry_ProperCountryDetails()
		{
			// Arrange
			CountryAddRequest? request = new CountryAddRequest() { CountryName = "Nigeria" };

			// Act
			CountryResponse response = await _countriesService.AddCountry(request);
			List<CountryResponse> countries_from_GetAllCountries = await _countriesService.GetAllCountries();

			// Assert
			Assert.True(response.CountryID != Guid.Empty);
			Assert.Contains(response, countries_from_GetAllCountries);
		}

        // When CountryName is duplicate, it should throw ArgumentException
        [Fact]
		public async Task AddCountry_DuplicateCountryName()
		{
			// Arrage
			CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "Cameroon" };
			CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "Cameroon" };

			// Assert
			await Assert.ThrowsAsync<ArgumentException>(async () =>
			{
				// Act
				await _countriesService.AddCountry(request1);
				await _countriesService.AddCountry(request2);
			});
        }

        #endregion
    }
}

