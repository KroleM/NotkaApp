using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class ReportRepository : IReportRepository
	{
		protected NotkaDatabaseContext Context { get; }

		public ReportRepository(NotkaDatabaseContext repositoryContext)
		{
			Context = repositoryContext;
		}

		public async Task<PagedList<ReportStockForView>> GetStocksReport(int userId, ReportParameters reportParameters)
		{
			var reportStocks = Context.PortfolioStock
				.Include(ps => ps.Stock)
				.ThenInclude(s => s.StockExchange)
				.GroupBy(x => x.Stock)
				.Select(stock => new ReportStockForView
				{
					Name = stock.Key.Name,
					Ticker = stock.Key.Ticker,
					StockExchangeId = stock.Key.StockExchangeId,
					StockExchangeShortName = stock.Key.StockExchange.ShortName ?? string.Empty,
					NumberOfPortfolios = stock.Count()
				})
				.OrderByDescending(x => x.NumberOfPortfolios).ThenBy(x => x.Name);


			return await PagedList<ReportStockForView>.CreateAsync(reportStocks,
										reportParameters.PageNumber,
										reportParameters.PageSize);
		}

		public async Task<List<ReportUserForView>> GetUsersReport(int userId, ReportParameters reportParameters)
		{
			//var reportUsers = from user in Context.Set<User>()
			//			 join note in Context.Set<Note>()
			//				 on user.Id equals note.UserId into noteGrouping
			//			 //join tag in Context.Set<List>()
			//				// on user.Id equals tag.UserId into listGrouping
			//			 select new ReportUserForView
			//			 {
			//				 UserId = user.Id,
			//				 Email = user.Email,
			//				 FirstName = user.FirstName,
			//				 LastName = user.LastName,
			//				 NumberOfNotes = noteGrouping.Count(),
			//				 //NumberOfLists = listGrouping.Count(),
			//			 };

			var userNotes = Context.Set<User>()
				.GroupJoin(
					Context.Set<Note>(),
					user => user.Id,
					note => note.UserId,
					(user, notes) => new
					{
						user,
						NoteCount = notes.Count()
					}).ToList();

			var userNotesLists = userNotes
				.GroupJoin(
					Context.Set<List>(),
					noteuser => noteuser.user.Id,
					list => list.UserId,
					(noteUser, lists) => new ReportUserForView
					{
						UserId = noteUser.user.Id,
						Email = noteUser.user.Email,
						FirstName = noteUser.user.FirstName,
						LastName = noteUser.user.LastName,
						NumberOfNotes = noteUser.NoteCount,
						NumberOfLists = lists.Count(),
					}).ToList();

			var userNotesListsTags = userNotesLists
				.GroupJoin(
					Context.Set<Tag>(),
					notelistuser => notelistuser.UserId,
					tag => tag.UserId,
					(noteListUser, tags) => new ReportUserForView
					{
						UserId = noteListUser.UserId,
						Email = noteListUser.Email,
						FirstName = noteListUser.FirstName,
						LastName = noteListUser.LastName,
						NumberOfNotes = noteListUser.NumberOfNotes,
						NumberOfLists = noteListUser.NumberOfLists,
						NumberOfTags = tags.Count(),
					}).ToList();

			return userNotesListsTags;
		}
	}
}
