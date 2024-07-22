using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;

		public ReportController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/Report
		[HttpGet("{userId}", Name = "GETStocksReport")]
		public async Task<ActionResult<PagedList<ReportStocksForView>>> GetStocksReport(int userId, [FromQuery] ReportParameters reportParameters)
		{
			PagedList<ReportStocksForView> stocksReport;
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
