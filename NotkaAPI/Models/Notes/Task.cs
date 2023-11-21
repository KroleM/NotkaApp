namespace Notka.Database.Data.Notes
{
	public class Task : ANote
	{
		public DateTime Deadline { get; set; }
		public DateTime CompletionTime { get; set; }
	}
}
