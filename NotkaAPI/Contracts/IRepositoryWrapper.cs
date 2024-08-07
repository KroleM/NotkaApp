﻿namespace NotkaAPI.Contracts
{
	public interface IRepositoryWrapper
	{
		IUserRepository User { get; }
		IRoleRepository Role { get; }
		IFeedRepository Feed { get; }
		INoteRepository Note { get; }
		ITagRepository Tag { get; }
		IListRepository List { get; }
		ICurrencyRepository Currency { get; }
		ICountryRepository Country { get; }
		IStockExchangeRepository StockExchange { get; }
		IStockRepository Stock { get; }
		IPortfolioRepository Portfolio { get; }
		IReportRepository Report { get; }
		Task SaveAsync();
	}
}
