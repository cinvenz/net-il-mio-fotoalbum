using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Plase provide a post Title.")]
		[StringLength(100, ErrorMessage = "Title must have less than 100 characters.")]
		public string Title { get; set; } = string.Empty;

		public IEnumerable<Photo>? Photos { get; set; }
	}
}
