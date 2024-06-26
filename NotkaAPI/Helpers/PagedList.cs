﻿using Microsoft.EntityFrameworkCore;

namespace NotkaAPI.Helpers
{
	public class PagedList<T>// : List<T>
	{
		public List<T> Items { get; private set; } = new List<T>();
		public int CurrentPage { get; private set; }
		public int TotalPages { get; private set; }
		public int PageSize { get; private set; }
		public int TotalCount { get; private set; }

		public bool HasPrevious => CurrentPage > 1;
		public bool HasNext => CurrentPage < TotalPages;

		public PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{
			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);

			Items.AddRange(items);
		}

		public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var count = await source.CountAsync();
			var finalPageSize = pageSize > 0 ? pageSize : count;    //if pageSize <= 0 then take all items

			var items = await source.Skip((pageNumber - 1) * pageSize).Take(finalPageSize).ToListAsync();

			return new PagedList<T>(items, count, pageNumber, finalPageSize);
		}
	}
}
