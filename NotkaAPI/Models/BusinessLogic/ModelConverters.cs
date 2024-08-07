﻿using NotkaAPI.Helpers;
using NotkaAPI.Models.CMS;
using NotkaAPI.Models.General;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Models.BusinessLogic
{
	public static class ModelConverters
	{
		//ViewModels
		public static NoteForView ConvertToNoteForView(Note? note)
		{
			return new NoteForView
			{
				TagsForView = note?.NoteTags.Select(notetag => new TagForView().CopyProperties(notetag.Tag)).ToList() ?? new(),
			}.CopyProperties(note);
		}
		public static ListForView ConvertToListForView(List? list)
		{
			return new ListForView
			{
				TagsForView = list?.ListTags.Select(listtag => new TagForView().CopyProperties(listtag.Tag)).ToList() ?? new(),
				ListElementsForView = list?.ListElements.Select(le => new ListElementForView().CopyProperties(le)).ToList() ?? new(),
			}.CopyProperties(list);
		}
		public static TagForView ConvertToTagForView(Tag? tag)
		{
			return new TagForView
			{
				NotesForView = tag?.NoteTags.Select(notetag => ConvertToNoteForView(notetag?.Note)).ToList() ?? new(),
				ListsForView = tag?.ListTags.Select(listtag => ConvertToListForView(listtag?.List)).ToList() ?? new(),
			}.CopyProperties(tag);
		}
		public static UserForView ConvertToUserForView(User? user)
		{
			return new UserForView
			{
				RolesForView = user?.RoleUsers.Select(roleuser => ConvertToRoleForView(roleuser?.Role)).ToList() ?? new(),
			}.CopyProperties(user);
		}
		public static RoleForView ConvertToRoleForView(Role? role) 
		{
			return new RoleForView { }.CopyProperties(role);
		}
		public static FeedForView ConvertToFeedForView(Feed? feed)
		{
			return new FeedForView { }.CopyProperties(feed);
		}
		public static CurrencyForView ConvertToCurrencyForView(Currency? currency)
		{
			return new CurrencyForView { }.CopyProperties(currency);
		}
		public static CountryForView ConvertToCountryForView(Country? country)
		{
			return new CountryForView { }.CopyProperties(country);
		}
		public static StockExchangeForView ConvertToStockExchangeForView(StockExchange? stockExchange)
		{
			return new StockExchangeForView 
			{
				//Do przypisania CountryShortName jest konieczne Include(Country)
				CountryShortName = stockExchange?.Country.ShortName ?? string.Empty,
				StocksForView = stockExchange?.Stocks.Select(stock => ConvertToStockForView(stock)).ToList() ?? new(),
			}.CopyProperties(stockExchange);
		}
		public static StockForView ConvertToStockForView(Stock? stock)
		{
			return new StockForView
			{
				StockExchangeShortName = stock?.StockExchange.ShortName ?? string.Empty,
				CurrencyShortName = stock?.Currency.ShortName ?? string.Empty,
				NotesForViews = stock?.StockNotes.Select(stocknote => ConvertToNoteForView(stocknote?.Note)).ToList() ?? new(),
			}.CopyProperties(stock);
		}
		public static PortfolioForView ConvertToPortfolioForView(Portfolio? portfolio)
		{
			return new PortfolioForView 
			{
				//StocksForView = portfolio?.PortfolioStocks.Select(ps => new StockForView().CopyProperties(ps.Stock)).ToList() ?? new(),
				StocksForView = portfolio?.PortfolioStocks.Select(ps => ConvertToStockForView(ps.Stock)).ToList() ?? new(),
			}.CopyProperties(portfolio);
		}
	}
}
