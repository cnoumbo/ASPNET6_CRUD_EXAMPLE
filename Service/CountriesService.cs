using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Service
{
	public class CountriesService : ICountriesService
	{
        private readonly PersonsDbContext _db;

		public CountriesService(PersonsDbContext db)
		{
            _db = db;
		}

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validation: coutryAddRequest parameter can't be null
            if (countryAddRequest == null)
                throw new ArgumentNullException(nameof(countryAddRequest));

            // Validation: CountryName can't be null
            if (String.IsNullOrEmpty(countryAddRequest.CountryName))
                throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validation: CountryName can't be duplicate
            if (await _db.Countries.Where(tmp => tmp.country_name == countryAddRequest.CountryName).CountAsync() > 0)
                throw new ArgumentException("Given country name already exists");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.country_id = Guid.NewGuid();

            // Everything is Okay
            await _db.Countries.AddAsync(country);

            await _db.SaveChangesAsync();

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries
                .Select(country => country.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country = await _db.Countries.FirstOrDefaultAsync(tmp => tmp.country_id == countryID);

            if (country == null)
                return null;

            return country.ToCountryResponse();
        }
    }
}

