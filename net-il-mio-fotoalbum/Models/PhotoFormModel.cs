using Azure;
using Microsoft.Extensions.Hosting;

namespace net_il_mio_fotoalbum.Models
{
	public class PhotoFormModel
	{
		public Photo Photo { get; set; } = new Photo { Image = "https://picsum.photos/200/300" };
		public IFormFile? ImageFormFile { get; set; }
		//public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
		//public List<int> SelectedTagIds { get; set; } = new();

		public void SetImageFileFromFormFile()
		{
			if (ImageFormFile is null) return;

			using var stream = new MemoryStream();
			ImageFormFile!.CopyTo(stream);
			Photo.ImageFile = stream.ToArray();
		}
	}
}
