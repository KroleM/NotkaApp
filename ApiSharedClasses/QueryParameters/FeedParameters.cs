using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class FeedParameters : AGetParameters
	{
		public bool IsActive { get; set; }
	}
}
