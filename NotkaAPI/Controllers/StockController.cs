using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public StockController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/Stock
		[HttpGet("{userId}", Name = "StockGETAll")]
		public async Task<ActionResult<PagedList<StockForView>>> GetStock(int userId, [FromQuery] StockParameters stockParameters)
		{
			PagedList<StockForView> stocks;
			try
			{
				stocks = await _repository.Stock.GetStocks(userId, stockParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return stocks;
		}

		// GET: api/Stock/5
		[HttpGet("{userId}/{id}", Name = "StockGET")]
		public async Task<ActionResult<StockForView>> GetStock(int userId, int id)
		{
			try
			{
				return await _repository.Stock.GetStockById(userId, id);
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

		// PUT: api/Stock/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "StockPUT")]
		public async Task<IActionResult> PutStock(int id, StockForView stock)
        {
			if (id != stock.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.Stock.UpdateStock(id, stock);
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

		// POST: api/Stock
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "StockPOST")]
		public async Task<ActionResult<StockForView>> PostStock(StockForView stock)
        {
			if (stock == null) return Forbid();

			StockForView uploadedStock;
			try
			{
				uploadedStock = await _repository.Stock.CreateStock(stock);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedStock);
		}

		// DELETE: api/Stock/5
		[HttpDelete("{userId}/{id}", Name = "StockDELETE")]
		public async Task<IActionResult> DeleteStock(int userId, int id)
		{
			try
			{
				await _repository.Stock.DeleteStock(userId, id);
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
