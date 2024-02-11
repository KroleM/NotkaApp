namespace NotkaAPI.Parameters
{
	public abstract class AGetParameters
	{
		const int maxPageSize = 60;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = 10; //20
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
		}
	}
}
