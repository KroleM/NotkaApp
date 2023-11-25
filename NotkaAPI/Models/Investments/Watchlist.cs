using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.Investments
{
	public class Watchlist : DictionaryTable
	{
		public int UserId { get; set; }
		public User User { get; set; }
		public List<WatchlistStock> WatchlistStocks { get; set; } = new();
    }
}
