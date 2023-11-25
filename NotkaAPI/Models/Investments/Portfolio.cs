using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.Investments
{
	public class Portfolio : DictionaryTable
	{
        public int UserId { get; set; }
        public User User { get; set; }
        public List<PortfolioStock> PortfolioStocks { get; set; } = new();
    }
}
