using System.ComponentModel.DataAnnotations;

namespace NotkaAPI.Models
{
	public abstract class BaseDatatable
	{
		[Key]
		public int Id { get; set; }
		public bool IsActive { get; set; }
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
		public DateTimeOffset ModifiedDate { get; set; } = DateTimeOffset.Now;
	}
}
