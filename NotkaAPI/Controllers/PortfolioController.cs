using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.Models.Investments;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public PortfolioController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		//// GET: api/Portfolio
		//[HttpGet("{userId}", Name = "PortfolioGETAll")]
		//public async Task<ActionResult<PagedList<PortfolioForView>>> GetPortfolio(int userId, [FromQuery] PortfolioParameters portfolioParameters)
		//{
		//	PagedList<PortfolioForView> portfolios;
		//	try
		//	{
		//		portfolios = await _repository.Portfolio.GetPortfolios(userId, portfolioParameters);
		//	}
		//	catch (NotFoundException)
		//	{
		//		return NotFound();
		//	}
		//	catch
		//	{
		//		return BadRequest();
		//	}

		//	return portfolios;
		//}

		// GET: api/Portfolio/5/12
		[HttpGet("{userId}/{id}", Name = "PortfolioGET")]
		public async Task<ActionResult<PortfolioForView>> GetPortfolio(int userId, int id)
		{
			try
			{
				return await _repository.Portfolio.GetPortfolioById(userId, id);
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

		// GET: api/Portfolio/5
		[HttpGet("{userId}", Name = "PortfolioGETByUser")]
		public async Task<ActionResult<PortfolioForView>> GetPortfolioByUser(int userId)
		{
			try
			{
				return await _repository.Portfolio.GetPortfolioByUser(userId);
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

		// PUT: api/Portfolio/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "PortfolioPUT")]
		public async Task<IActionResult> PutPortfolio(int id, PortfolioForView portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }

			try
			{
				await _repository.Portfolio.UpdatePortfolio(id, portfolio);
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

		// POST: api/Portfolio
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "PortfolioPOST")]
		public async Task<ActionResult<PortfolioForView>> PostPortfolio(PortfolioForView portfolio)
        {
			if (portfolio == null) return Forbid();

			PortfolioForView uploadedPortfolio;
			try
			{
				uploadedPortfolio = await _repository.Portfolio.CreatePortfolio(portfolio);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedPortfolio);
		}

		// DELETE: api/Portfolio/5
		[HttpDelete("{userId}/{id}", Name = "PortfolioDELETE")]
		public async Task<IActionResult> DeletePortfolio(int userId, int id)
		{
			try
			{
				await _repository.Portfolio.DeletePortfolio(userId, id);
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
