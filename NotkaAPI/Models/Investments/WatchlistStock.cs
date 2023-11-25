namespace NotkaAPI.Models.Investments
{
	public class WatchlistStock : BaseDatatable
	{
		public int WatchlistId { get; set; }
		public Watchlist Watchlist { get; set; }
		public int StockId { get; set; }
		public Stock Stock { get; set; }
		// TargetPrice?
	}
}
