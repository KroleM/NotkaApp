using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public CountriesController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/Countries
		[HttpGet("{userId}", Name = "CountryGETAll")]
		public async Task<ActionResult<PagedList<CountryForView>>> GetCountry(int userId, [FromQuery] CountryParameters countryParameters)
		{
			PagedList<CountryForView> countries;
			try
			{
				countries = await _repository.Country.GetCountries(userId, countryParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return countries;
		}

		// GET: api/Countries/5
		[HttpGet("{userId}/{id}", Name = "CountryGET")]
		public async Task<ActionResult<CountryForView>> GetCountry(int userId, int id)
		{
			try
			{
				return await _repository.Country.GetCountryById(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (UnauthorizedException)
			{
				return Unauthorized();
			}
			catch
			{
				return BadRequest();
			}
		}

		// PUT: api/Countries/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "CountryPUT")]
		public async Task<IActionResult> PutCountry(int id, CountryForView country)
        {
			if (id != country.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.Country.UpdateCountry(id, country);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return NoContent();
		}

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name="CountryPOST")]
        public async Task<ActionResult<CountryForView>> PostCountry(CountryForView country)
        {
			if (country == null) return Forbid();

			CountryForView uploadedCountry;
			try
			{
				uploadedCountry = await _repository.Country.CreateCountry(country);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedCountry);
		}

		// DELETE: api/Countries/5
		[HttpDelete("{userId}/{id}", Name = "CountryDELETE")]
		public async Task<IActionResult> DeleteCountry(int userId, int id)
		{
			try
			{
				await _repository.Country.DeleteCountry(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (ForbidException)
			{
				return Forbid();
			}

			return NoContent();
		}

    }
}
