namespace NotkaAPI.Helpers
{
	public class NotFoundException : Exception
	{
	}
	public class ForbidException : Exception
	{
	}
	public class UnauthorizedException : Exception
	{
	}
	/// <summary>
	/// HTTP response status code 409 (Conflict)
	/// </summary>
	public class ConflictException : Exception
	{
	}
}
