using System;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
	public class CountriesService : ICountriesService
	{
        List<Country> _countries;

		public CountriesService(bool initialize = true)
		{
            _countries = new List<Country>();
            if (initialize)
            {
                _countries.AddRange(new List<Country>()
                {
                    new Country() { CountryID = Guid.Parse("34C3732D-D071-4EE3-ACDB-2E079C5A7944"), CountryName = "Cameroon" },
                    new Country() { CountryID = Guid.Parse("B1EE06D5-5EF1-4047-A69F-7549617B7BDA"), CountryName = "Canada" },
                    new Country() { CountryID = Guid.Parse("8ADEC2F3-917D-4B24-BA06-C3DAA4F40A66"), CountryName = "USA" },
                    new Country() { CountryID = Guid.Parse("28498692-292D-4DE3-A1C5-13881CAFFC74"), CountryName = "China" },
                    new Country() { CountryID = Guid.Parse("50845956-E212-419D-89F3-ED51608A580B"), CountryName = "Germany" },
                    new Country() { CountryID = Guid.Parse("95DC472B-C209-4A41-B92B-618B023F6D34"), CountryName = "Russie" }
                });
            }
		}

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validation: coutryAddRequest parameter can't be null
            if (countryAddRequest == null)
                throw new ArgumentNullException(nameof(countryAddRequest));

            // Validation: CountryName can't be null
            if (String.IsNullOrEmpty(countryAddRequest.CountryName))
                throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validation: CountryName can't be duplicate
            if (_countries.Where(tmp => tmp.CountryName == countryAddRequest.CountryName).Count() > 0)
                throw new ArgumentException("Given country name already exists");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.CountryID = Guid.NewGuid();

            // Everything is Okay
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country = _countries.FirstOrDefault(tmp => tmp.CountryID == countryID);

            if (country == null)
                return null;

            return country.ToCountryResponse();
        }
    }
}

