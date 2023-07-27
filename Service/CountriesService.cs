using Entities;
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

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validation: coutryAddRequest parameter can't be null
            if (countryAddRequest == null)
                throw new ArgumentNullException(nameof(countryAddRequest));

            // Validation: CountryName can't be null
            if (String.IsNullOrEmpty(countryAddRequest.CountryName))
                throw new ArgumentException(nameof(countryAddRequest.CountryName));

            // Validation: CountryName can't be duplicate
            if (_db.Countries.Where(tmp => tmp.country_name == countryAddRequest.CountryName).Count() > 0)
                throw new ArgumentException("Given country name already exists");

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate CountryID
            country.country_id = Guid.NewGuid();

            // Everything is Okay
            _db.Countries.Add(country);

            _db.SaveChanges();

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _db.Countries.ToList()
                .Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
                return null;

            Country? country = _db.Countries.FirstOrDefault(tmp => tmp.country_id == countryID);

            if (country == null)
                return null;

            return country.ToCountryResponse();
        }
    }
}

