using System.ComponentModel.DataAnnotations;

namespace NotkaAPI.Models
{
	public abstract class DictionaryTable : BaseDatatable
	{
		[Required(ErrorMessage = "This field should have a name!")]
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
	}
}
