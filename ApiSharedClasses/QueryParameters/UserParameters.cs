using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class UserParameters : AGetParameters
	{
		public bool IsActive { get; set; }
		public int RoleId { get; set; } = 0;
	}
}
