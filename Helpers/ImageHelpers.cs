using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace dotnet_store.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> SaveResizedImageAsync(IFormFile file, string folderPath, int maxWidth, int maxHeight)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Yüklenen dosya geçersiz.");

            var fileName = Path.GetRandomFileName() + ".jpg";
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(maxWidth, maxHeight),
                    Mode = ResizeMode.Max, // oranı korur
                    Sampler = KnownResamplers.Lanczos3
                }));

                await image.SaveAsJpegAsync(savePath, new JpegEncoder
                {
                    Quality = 90
                });
            }

            return fileName;
        }
    }
}
