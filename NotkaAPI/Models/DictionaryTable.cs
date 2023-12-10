using System.ComponentModel.DataAnnotations;

namespace NotkaAPI.Models
{
	public abstract class DictionaryTable : BaseDatatable
	{
		[Required(ErrorMessage = "This field should have a name!")]
		[MaxLength(200)]
		public string Name { get; set; } = null!;
		public string? Description { get; set; } = null!;
	}
}
