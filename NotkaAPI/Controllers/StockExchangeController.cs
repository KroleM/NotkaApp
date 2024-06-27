using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class StockExchangeController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public StockExchangeController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/StockExchange
		[HttpGet("{userId}", Name = "StockExchangeGETAll")]
		public async Task<ActionResult<PagedList<StockExchangeForView>>> GetStockExchange(int userId, [FromQuery] StockExchangeParameters stockExchangeParameters)
		{
			PagedList<StockExchangeForView> stockExchanges;
			try
			{
				stockExchanges = await _repository.StockExchange.GetStockExchanges(userId, stockExchangeParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return stockExchanges;
		}

		// GET: api/StockExchange/5
		[HttpGet("{userId}/{id}", Name = "StockExchangeGET")]
		public async Task<ActionResult<StockExchangeForView>> GetStockExchange(int userId, int id)
		{
			try
			{
				return await _repository.StockExchange.GetStockExchangeById(userId, id);
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

		// PUT: api/StockExchange/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "StockExchangePUT")]
		public async Task<IActionResult> PutStockExchange(int id, StockExchangeForView stockExchange)
        {
			if (id != stockExchange.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.StockExchange.UpdateStockExchange(id, stockExchange);
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

		// POST: api/StockExchange
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "StockExchangePOST")]
		public async Task<ActionResult<StockExchangeForView>> PostStockExchange(StockExchangeForView stockExchange)
        {
			if (stockExchange == null) return Forbid();

			StockExchangeForView uploadedStockExchange;
			try
			{
				uploadedStockExchange = await _repository.StockExchange.CreateStockExchange(stockExchange);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedStockExchange);
		}

		// DELETE: api/StockExchange/5
		[HttpDelete("{userId}/{id}", Name = "StockExchangeDELETE")]
		public async Task<IActionResult> DeleteStockExchange(int userId, int id)
		{
			try
			{
				await _repository.StockExchange.DeleteStockExchange(userId, id);
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
