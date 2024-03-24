using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class NoteParameters : AGetParameters
	{
		public DateTimeOffset MinDateOfCreation { get; set; } = new DateTimeOffset(new DateTime(2020, 1, 1));
		public DateTimeOffset MaxDateOfCreation { get; set; } = DateTimeOffset.MaxValue; //DateTimeOffset.Now.AddSeconds(1);
		//public bool HasPicture { get; set; }
		public bool ValidTimeRange => MaxDateOfCreation > MinDateOfCreation;
	}
}
