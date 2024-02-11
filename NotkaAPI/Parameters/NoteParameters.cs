namespace NotkaAPI.Parameters
{
	public class NoteParameters : AGetParameters
	{
		public DateTime MinDateOfCreation { get; set; } = DateTime.MinValue;
		public DateTime MaxDateOfCreation { get; set; } = DateTime.Now;
		public bool ValidTimeRange => MaxDateOfCreation > MinDateOfCreation;
	}
}
