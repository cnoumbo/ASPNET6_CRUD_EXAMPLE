using System;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
	[Route("[controller]")]
	public class PersonsController : Controller
	{
		private readonly IPersonsService _personsService;
		private readonly ICountriesService _countriesService;

		public PersonsController(IPersonsService personsService, ICountriesService countriesService)
		{
			_personsService = personsService;
			_countriesService = countriesService;
		}

		[Route("index")]
		[Route("/")]
		public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy=nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
		{
			// Search person
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
				{ nameof(PersonResponse.PersonName), "Person Name" },
				{ nameof(PersonResponse.Email), "Email" },
				{ nameof(PersonResponse.DateOfBirth), "Date of Birth" },
				{ nameof(PersonResponse.Gender), "Gender" },
				{ nameof(PersonResponse.CountryID), "Country" },
				{ nameof(PersonResponse.Address), "Address" }
			};

			List<PersonResponse> persons = await _personsService.GetFilteredPersons(searchBy, searchString);
			ViewBag.CurrentSearchBy = searchBy;
			ViewBag.CurrentSearchString = searchString;

			// Sort person
			List<PersonResponse> sortedPersons = _personsService.GetSortedPersons(persons, sortBy, sortOrder);

            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons);

		}
	
		[HttpGet]
		[Route("create")]
		public async Task<IActionResult> Create(){
			List<CountryResponse> countries = await _countriesService.GetAllCountries();
			//ViewBag.Countries = countries;

			// Usage of List<SelectListItem>
			ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

			return View();
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> Create(PersonAddRequest personAddRequest) {
			if(!ModelState.IsValid)
			{
				List<CountryResponse> countries = await _countriesService.GetAllCountries();
				ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString()});

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				return View();
			}

			// Call the service method
			PersonResponse personResponse = await _personsService.AddPerson(personAddRequest);

			return RedirectToAction("Index", "Persons");
		}

		[HttpGet]
		[Route("[action]/{personID}")]
		public async Task<IActionResult> Edit(Guid personID)
		{
			// Retrieve Person Info through personID
			PersonResponse? selectedPerson = await _personsService.GetPersonByPersonID(personID);

			if (selectedPerson is null)
				return RedirectToAction("Index");

			//Convert to PersonUpdate
			PersonUpdateRequest personUpdate = selectedPerson.ToPersonUdpateRequest();

            // Generate list of countries for the required dropdown list
            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

            return View(personUpdate);
		}

		[HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdate)
		{
			// perform update operation
			if(ModelState.IsValid)
			{
                PersonResponse personResponse = await _personsService.UpdatePerson(personUpdate);
				//if(personResponse is not null)
                    return RedirectToAction("Index");
				//else
				//{
				//	// Perform some operation - that probably means personID doesn't match to any personID in the database
				//	ViewBag.Errors 
				//	return View();
				//}

            }
			else
			{
                // there are some validation errors to show on view
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

				return View();
            }
        }

		[HttpGet]
		[Route("[action]/{personID}")]
		public async Task<IActionResult> Delete(Guid personID)
		{			
			PersonResponse? person = await _personsService.GetPersonByPersonID(personID);

			if (person is null)
				return RedirectToAction("Index");

			return View(person.ToPersonUdpateRequest());
		}

        [HttpPost]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Delete(PersonUpdateRequest personUpdate)
        {
			Guid personID = personUpdate.PersonID;
			bool isPersonDelete = await _personsService.DeletePerson(personID);

			if(isPersonDelete)
			{
                return RedirectToAction("Index");
            }
            else
			{
				PersonResponse? person = await _personsService.GetPersonByPersonID(personID);
				ViewBag.ErrorNotification = "Error occured while suppressing person, please retry later";
                return View(person);
            }
        }
    }
}

