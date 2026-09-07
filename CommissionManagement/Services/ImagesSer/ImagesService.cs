using CommissionManagement.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace CommissionManagement.Services.ImagesSer
{
    public class ImagesService : IImagesService
    {
        //注入網站環境服務
        private readonly IWebHostEnvironment _environment;

        public ImagesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<(string ImagePath, string ThumbPath)> UploadAndProcess(Stream fileStream, string contentType)
        {
            var allowedTypes = new[] {"image/jpeg", "image/png", "image/webp" };

            if(!allowedTypes.Contains(contentType.ToLower()))
            {
                throw new ArgumentException("不支援的檔案格式，僅限上傳 JPG, PNG 或 WEBP 圖片。");
            }

            //WebRootPath取得根目錄wwwroot的路徑
            //若無法取得WebRootPath，則使用ContentRootPath取得專案根目錄，再組合出wwwroot的路徑
            //Path.Combine組合路徑
            var webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath,"wwwroot");
            var mainsFolder = Path.Combine(webRootPath,"uploads","mains");
            var thumbsFolder = Path.Combine(webRootPath,"uploads","thumbs");

            //如果uploads/mains資料夾不存在就建立
            if (!Directory.Exists(mainsFolder))
            {
                Directory.CreateDirectory(mainsFolder);
            }
            if (!Directory.Exists(thumbsFolder))
            {
                Directory.CreateDirectory(thumbsFolder);
            }
            //生成唯一的檔案名稱，避免同名檔案上傳覆蓋
            var fileGuid = Guid.NewGuid().ToString();
            //生成完整檔名
            var fileName = $"{fileGuid}.webp";

            var mainFilePath = Path.Combine(mainsFolder, fileName);
            var thumbFilePath = Path.Combine(thumbsFolder, fileName);

            //利用ImageSharp套件讀取圖片建立image變數代表
            using var image = await Image.LoadAsync(fileStream);

            //把image圖片複製一份，並進行縮放處理
            //ResizeMode.Max代表原圖比例縮放，使最長邊不超過指定尺寸
            //ResizeMode.Crop代表按比例縮放後，從正中央裁切成指定尺寸
            using (var mainImage = image.Clone(x => x.Resize(new ResizeOptions {Size= new Size(720,1280),Mode = ResizeMode.Max })))
            {
                //使用WebpEncoder將圖片轉成WebP格式儲存，並設定壓縮品質為80
                await mainImage.SaveAsync(mainFilePath, new WebpEncoder { Quality = 80 });
            }
            using (var thumbImage = image.Clone(x => x.Resize(new ResizeOptions {Size= new Size(150,150),Mode = ResizeMode.Crop })))
            {
                await thumbImage.SaveAsync(thumbFilePath, new WebpEncoder { Quality = 75 });
            }

            return ($"/uploads/mains/{fileName}", $"/uploads/thumbs/{fileName}");
        }
    }
}
