﻿using NotkaAPI.Contracts;
using NotkaAPI.Data;

namespace NotkaAPI.Repository
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private NotkaDatabaseContext _context;
		private IUserRepository? _user;
		private IRoleRepository? _role;
		private IFeedRepository? _feed;
		private INoteRepository? _note;
		private ITagRepository? _tag;
		private IListRepository? _list;
		private ICurrencyRepository? _currency;
		private ICountryRepository? _country;
		private IStockExchangeRepository? _stockExchange;
		private IStockRepository? _stock;
		private IPortfolioRepository? _portfolio;
		private IReportRepository? _report;

		public IUserRepository User
		{
			get
			{
				if (_user == null)
				{
					_user = new UserRepository(_context);
				}

				return _user;
			}
		}
		public IRoleRepository Role
		{
			get
			{
				if (_role == null)
				{
					_role = new RoleRepository(_context);
				}

				return _role;
			}
		}
		public IFeedRepository Feed
		{
			get
			{
				if (_feed == null)
				{
					_feed = new FeedRepository(_context);
				}

				return _feed;
			}
		}
		public INoteRepository Note
		{
			get
			{
				if (_note == null)
				{
					_note = new NoteRepository(_context);
				}

				return _note;
			}
		}
		public ITagRepository Tag
		{
			get
			{
				if (_tag == null)
				{
					_tag = new TagRepository(_context);
				}

				return _tag;
			}
		}
		public IListRepository List
		{
			get
			{
				if (_list == null)
				{
					_list = new ListRepository(_context);
				}

				return _list;
			}
		}
		public ICurrencyRepository Currency
		{
			get
			{
				if (_currency == null)
				{
					_currency = new CurrencyRepository(_context);
				}

				return _currency;
			}
		}
		public ICountryRepository Country
		{
			get
			{
				if (_country == null)
				{
					_country = new CountryRepository(_context);
				}

				return _country;
			}
		}
		public IStockExchangeRepository StockExchange
		{
			get
			{
				if (_stockExchange == null)
				{
					_stockExchange = new StockExchangeRepository(_context);
				}

				return _stockExchange;
			}
		}
		public IStockRepository Stock
		{
			get
			{
				if (_stock == null)
				{
					_stock = new StockRepository(_context);
				}

				return _stock;
			}
		}
		public IPortfolioRepository Portfolio
		{
			get
			{
				if (_portfolio == null)
				{
					_portfolio = new PortfolioRepository(_context);
				}

				return _portfolio;
			}
		}
		public IReportRepository Report
		{
			get
			{
				if (_report == null)
				{
					_report = new ReportRepository(_context);
				}

				return _report;
			}
		}

		public RepositoryWrapper(NotkaDatabaseContext context)
		{
			_context = context;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
