﻿
namespace NotkaAPI.Models.Notes
{
	public class TaskTag : BaseDatatable
	{
		public int TaskId { get; set; }
		public NotkaAPI.Models.Notes.Task? Task { get; set; }
		public int TagId { get; set; }
		public Tag? Tag { get; set; }
	}
}
