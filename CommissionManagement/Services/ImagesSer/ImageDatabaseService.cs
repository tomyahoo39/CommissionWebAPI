using CommissionManagement.DTO.ImagesDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.ImagesSer
{
    public class ImageDatabaseService : IImageDatabaseService
    {
        private readonly IImagesService _service;
        private readonly CommissionContext _context;

        public ImageDatabaseService(IImagesService service, CommissionContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task UploadNewImage(ImageUploadDto dto)
        {
            var category = await _context.CommissionTypes.AnyAsync(c => c.Id == dto.CommissionTypeId);
            if (!category)
            {
                throw new KeyNotFoundException("該委託分類不存在");
            }

            using var stream = dto.File.OpenReadStream();
            var (imagePath, thumbPath) = await _service.UploadAndProcess(stream, dto.File.ContentType);

            var maxSortOrder = await _context.Images
                .Where(i => i.CommissionTypeId == dto.CommissionTypeId)
                .MaxAsync(img => (int?)img.SortOrder) ?? 0;

            var newImage = new Image
            {
                CommissionTypeId = dto.CommissionTypeId,
                Title = dto.Title,
                ImagePath = imagePath,
                ThumbPath = thumbPath,
                SortOrder = maxSortOrder + 1,
                IsVisible = true,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            };
            await _context.Images.AddAsync(newImage);
            await _context.SaveChangesAsync();
        }
    }
}
