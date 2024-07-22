using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportStocksController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;

		public ReportStocksController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/ReportStocks
		[HttpGet("{userId}", Name = "GETStocksReport")]
		public async Task<ActionResult<PagedList<ReportStockForView>>> GetStocksReport(int userId, [FromQuery] ReportParameters reportParameters)
		{
			PagedList<ReportStockForView> stocksReport;
			try
			{
				stocksReport = await _repository.Report.GetStocksReport(userId, reportParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return stocksReport;
		}

	}
}
