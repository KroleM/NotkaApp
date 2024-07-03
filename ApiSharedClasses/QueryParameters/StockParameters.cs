using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class StockParameters : AGetParameters
	{
		public int StockExchangeId { get; set; } = 0;
		public int CurrencyId { get; set; } = 0;
		public bool? IsActive { get; set; }
	}
}
