﻿namespace NotkaAPI.ViewModels
{
	public class ReportStocksForView
	{
		public string Name { get; set; }
		public string Ticker { get; set; }
		public int StockExchangeId { get; set; }
		public string StockExchangeShortName { get; set; }
		public int NumberOfPortfolios { get; set; }
	}
}
