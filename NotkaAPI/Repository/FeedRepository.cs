using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.CMS;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class FeedRepository : RepositoryBase<Feed>, IFeedRepository
	{
		public FeedRepository(NotkaDatabaseContext repositoryContext)
			: base(repositoryContext)
		{
		}

		public async Task<PagedList<FeedForView>> GetFeeds(int userId, FeedParameters feedParameters)
		{
			var feeds = FindByCondition(f => ((feedParameters.IsActive ?? false) ? f.IsActive == true : ((feedParameters.IsActive ?? true) ? (f.IsActive == true || f.IsActive == false) : f.IsActive == false)));

			var feedsWithIncludes = feeds.Include(feed => feed.Picture);

			return await PagedList<FeedForView>.CreateAsync(feedsWithIncludes.OrderBy(f => f.CreatedDate)
							.Select(feed => ModelConverters.ConvertToFeedForView(feed)),
										feedParameters.PageNumber,
										feedParameters.PageSize);
		}
		public async Task<FeedForView> GetFeedById(int userId, int id)
		{
			if (!await Context.Feed.AnyAsync(f => f.Id == id))
			{
				throw new NotFoundException();
			}
			var feed = await Context.Feed
				.Include(f => f.Picture)
				.SingleOrDefaultAsync(f => f.Id == id);

			return ModelConverters.ConvertToFeedForView(feed);
		}
		public async Task<FeedForView> CreateFeed(int userId, FeedForView feed)
		{
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			var feedToAdd = new Feed().CopyProperties(feed);
			Context.Feed.Add(feedToAdd);
			await Context.SaveChangesAsync();

			var uploadedFeed = await Context.Feed
				.Include(feed => feed.Picture)
				.SingleOrDefaultAsync(feed => feed.Id == feedToAdd.Id);

			return ModelConverters.ConvertToFeedForView(uploadedFeed);
		}
		public async Task UpdateFeed(int id, FeedForView feed)
		{
			var feedToAdd = new Feed().CopyProperties(feed);
			Context.Entry(feedToAdd).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = Context.Database.BeginTransaction())
				{
					//Picture
					if (feedToAdd.Picture != null)
					{
						await Context.Picture.AddAsync(feedToAdd.Picture);
					}

					await Context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FeedExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}
		public async Task DeleteFeed(int userId, int id)
		{
			if (!Context.User.Any(u => u.Id == userId && u.RoleUsers.Any(ru => ru.RoleId == 3)))
			{
				throw new UnauthorizedException();
			}

			var feed = await Context.Feed
						.Include(f => f.Picture)
						.SingleOrDefaultAsync(feed => feed.Id == id);

			if (feed == null)
			{
				throw new NotFoundException();
			}

			Delete(feed);
			await Context.SaveChangesAsync();
		}
		private bool FeedExists(int id)
		{
			return Context.Feed.Any(e => e.Id == id);
		}
	}
}
