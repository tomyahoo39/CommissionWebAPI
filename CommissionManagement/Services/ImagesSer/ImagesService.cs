using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace CommissionManagement.Services.ImagesSer
{
    public class ImagesService : IImagesService
    {
        private static readonly HashSet<string> AllowedTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/webp"
        };

        private readonly Cloudinary _cloudinary;

        public ImagesService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<(string ImagePath, string ThumbPath)> UploadAndProcess(Stream fileStream, string contentType)
        {
            if (!AllowedTypes.Contains(contentType))
            {
                throw new ArgumentException("不支援的檔案格式，僅限上傳 JPG, PNG 或 WEBP 圖片。");
            }

            if(fileStream.CanSeek && fileStream.Position != 0)
            {
                fileStream.Position = 0;
            }

            var detectedType = DetectImageContentType(fileStream);
            if (detectedType == null || !string.Equals(detectedType, contentType, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("檔案格式驗證失敗，請上傳有效的 JPG、PNG 或 WEBP 圖片。");
            }

            if (fileStream.CanSeek)
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

        private static string? DetectImageContentType(Stream stream)
        {
            Span<byte> header = stackalloc byte[12];
            var read = stream.Read(header);

            if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            {
                return "image/jpeg";
            }

            if (read >= 8 &&
                header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
                header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
            {
                return "image/png";
            }

            if (read >= 12 &&
                header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46 &&
                header[8] == 0x57 && header[9] == 0x45 && header[10] == 0x42 && header[11] == 0x50)
            {
                return "image/webp";
            }

            return null;
        }
    }
}
