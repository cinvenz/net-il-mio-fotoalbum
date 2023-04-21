namespace net_il_mio_fotoalbum.Models
{
	public class Photo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; } = string.Empty;
		public byte[]? ImageFile { get; set; }
		public string ImgSrc => ImageFile is null
			? Image
			: $"data:image/png;base64,{Convert.ToBase64String(ImageFile)}";
		public bool Visible { get; set; }

		public int CategoryId { get; set; }
		//public Category? Category { get; set; }

	}
	

}
