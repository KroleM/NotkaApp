using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportUsersController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;

		public ReportUsersController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/ReportUsers
		[HttpGet("{userId}", Name = "GETUsersReport")]
		public async Task<ActionResult<List<ReportUserForView>>> GetUsersReport(int userId, [FromQuery] ReportParameters reportParameters)
		{
			List<ReportUserForView> usersReport;
			try
			{
				usersReport = await _repository.Report.GetUsersReport(userId, reportParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return usersReport;
		}
	}
}
