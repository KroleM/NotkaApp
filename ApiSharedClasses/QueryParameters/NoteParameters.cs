using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class NoteParameters : AGetParameters
	{
		public DateTime MinDateOfCreation { get; set; } = new DateTime(2020, 1, 1).ToUniversalTime();
		public DateTime MaxDateOfCreation { get; set; } = DateTime.Now.AddSeconds(1).ToUniversalTime();
		//public bool HasPicture { get; set; }
		public bool ValidTimeRange => MaxDateOfCreation > MinDateOfCreation;
	}
}
