using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class NoteParameters : AGetParameters
	{
		public DateTime MinDateOfCreation { get; set; } = new DateTime(2020, 1, 1);
		public DateTime MaxDateOfCreation { get; set; } = DateTime.Now;
		//public bool HasPicture { get; set; }
		public bool ValidTimeRange => MaxDateOfCreation > MinDateOfCreation;
	}
}
