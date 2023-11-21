namespace NotkaAPI.Models.Notes
{
	public class Task : ANote
	{
		public List<TaskTag> TaskTag { get; set; } = new();
		public DateTime Deadline { get; set; }
		public DateTime CompletionTime { get; set; }
	}
}
