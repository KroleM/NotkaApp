using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public CurrencyController(IRepositoryWrapper repository)
        {
			_repository = repository;
		}

        // GET: api/Currency
		[HttpGet("{userId}", Name = "CurrencyGETAll")]
		public async Task<ActionResult<PagedList<CurrencyForView>>> GetCurrency(int userId, [FromQuery] CurrencyParameters currencyParameters)
        {
			PagedList<CurrencyForView> currencies;
			try
			{
				currencies = await _repository.Currency.GetCurrencies(userId, currencyParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return currencies;
		}

		// GET: api/Currency/5
		[HttpGet("{userId}/{id}", Name = "CurrencyGET")]
		public async Task<ActionResult<CurrencyForView>> GetCurrency(int userId, int id)
		{
			try
			{
				return await _repository.Currency.GetCurrencyById(userId, id);
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

		// PUT: api/Currency/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "CurrencyPUT")]
		public async Task<IActionResult> PutCurrency(int id, CurrencyForView currency)
        {
			if (id != currency.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.Currency.UpdateCurrency(id, currency);
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

		// POST: api/Currency
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "CurrencyPOST")]
		public async Task<ActionResult<CurrencyForView>> PostCurrency(CurrencyForView currency)
        {
			if (currency == null) return Forbid();

			CurrencyForView uploadedCurrency;
			try
			{
				uploadedCurrency = await _repository.Currency.CreateCurrency(currency);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedCurrency);
		}

		// DELETE: api/Currency/5
		[HttpDelete("{userId}/{id}", Name = "CurrencyDELETE")]
		public async Task<IActionResult> DeleteCurrency(int userId, int id)
		{
			try
			{
				await _repository.Currency.DeleteCurrency(userId, id);
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
