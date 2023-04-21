using net_il_mio_fotoalbum.Attributes;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
	public class Photo
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Inserisci un Titolo della Foto.")]
		[StringLength(100, ErrorMessage = "Il Titolo non può essere più lungo di 100 caratteri.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Inserisci una descrizione per la Foto.")]
		[StringLength(300, ErrorMessage = "la descrizione della Foto non può superare i 300 caratteri.")]
		[MoreThanOneWord(5, ErrorMessage = "la descrizione della pizza deve contenere almeno 5 parole.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Inserisci un immagine per la Foto.")]
		public string Image { get; set; } = string.Empty;
		public byte[]? ImageFile { get; set; }
		public string ImgSrc => ImageFile is null
			? Image
			: $"data:image/png;base64,{Convert.ToBase64String(ImageFile)}";
		public bool Visible { get; set; }

		//public int CategoryId { get; set; }
		////public Category? Category { get; set; }

	}
	

}


