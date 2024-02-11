namespace NotkaAPI.Models.Notes
{
	public class TaskClass : ANote
	{
		public List<TaskTag> TaskTags { get; set; } = new();
		public DateTime? Deadline { get; set; }
		public DateTime? CompletionTime { get; set; }
	}
}
