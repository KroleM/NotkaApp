using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class NoteParameters : AGetParameters
	{
		public DateTime MinDateOfCreation { get; set; } = DateTime.MinValue;
		public DateTime MaxDateOfCreation { get; set; } = DateTime.Now;
		public bool ValidTimeRange => MaxDateOfCreation > MinDateOfCreation;
	}
}
