using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace CommissionManagement.Services.ImagesSer
{
    public class ImagesService : IImagesService
    {
        private readonly Cloudinary _cloudinary;

        public ImagesService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<(string ImagePath, string ThumbPath)> UploadAndProcess(Stream fileStream, string contentType)
        {
            var allowedTypes = new[] {"image/jpeg", "image/png", "image/webp" };

            if(!allowedTypes.Contains(contentType.ToLower()))
            {
                throw new ArgumentException("不支援的檔案格式，僅限上傳 JPG, PNG 或 WEBP 圖片。");
            }

            if(fileStream.CanSeek && fileStream.Position != 0)
            {
                fileStream.Position = 0;
            }

            var fileGuid = Guid.NewGuid().ToString();

            var mainTransformation = new Transformation()
                .Width(1400)
                .Height(1400)
                .Crop("limit")
                .Quality(80)
                .FetchFormat("webp");

            var thumbTransformation = new Transformation()
                .Width(600)
                .Height(600)
                .Crop("limit")
                .Quality(75)
                .FetchFormat("webp");

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileGuid, fileStream),
                Folder = "uploads",
                PublicId = fileGuid,
                EagerTransforms = new List<Transformation>
                {
                    mainTransformation,
                    thumbTransformation
                }
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResult.Error != null)
            {
                throw new Exception($"上傳圖片失敗: {uploadResult.Error.Message}");
            }

            var mainUrl = uploadResult.Eager[0].SecureUrl.ToString();
            var thumbUrl = uploadResult.Eager[1].SecureUrl.ToString();

            return (mainUrl, thumbUrl);
        }
    }
}
