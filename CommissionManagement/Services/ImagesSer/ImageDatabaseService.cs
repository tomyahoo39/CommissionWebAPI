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

        public async Task<IEnumerable<GetFirstImageDTO>> GetFirstImages()
        {
            var firstImages = await _context.CommissionTypes
                .Where(c => c.IsActive && c.IsHomeVisible)
                .OrderBy(c => c.HomeSortOrder)
                .Take(4)
                .Select(c => new GetFirstImageDTO
                {
                    CommissionTypeId = c.Id,
                    TypeName = c.TypeName,
                    HomeSortOrder = c.HomeSortOrder,
                    Title = c.Images
                    .Where(i => i.IsVisible)
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.Title)
                    .FirstOrDefault(),
                    ThumbPath = c.Images
                    .Where(i => i.IsVisible)
                    .OrderBy(i => i.SortOrder)
                    .Select(i => i.ThumbPath)
                    .FirstOrDefault(),
                })
                .ToListAsync();
            return firstImages;
        }

        public async Task<IEnumerable<GetAllImageDTO>> ShowAllImages(int? commissionTypeId)
        {
            var query = _context.Images.Where(i => i.IsVisible);

            var type = await _context.CommissionTypes
                .OrderByDescending(c => c.Id)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if(commissionTypeId > type || commissionTypeId < 1)
            {
                throw new Exception("輸入委託項目不存在");
            }

            if(commissionTypeId.HasValue && commissionTypeId > 0)
            {
                query = query.Where(i => i.CommissionTypeId == commissionTypeId.Value);
            }

            var images = await query
                .OrderBy(i => i.SortOrder)
                .Select(i => new GetAllImageDTO
                {
                    CommissionTypeId = i.CommissionTypeId,
                    Title = i.Title,
                    ImagePath = i.ImagePath,
                    ThumbPath = i.ThumbPath,
                    SortOrder = i.SortOrder
                })
                .ToListAsync(); 
            return images;
        }

        public async Task<IEnumerable<GetAllImagesAdminDTO>> ShowAllImagesAdmin(int? commissionTypeId)
        {
            var query = _context.Images.AsQueryable();

            var type = await _context.CommissionTypes
                .OrderByDescending(c => c.Id)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (commissionTypeId > type || commissionTypeId < 1)
            {
                throw new Exception("輸入委託項目不存在");
            }

            if (commissionTypeId.HasValue && commissionTypeId > 0)
            {
                query = query.Where(i => i.CommissionTypeId == commissionTypeId.Value);
            }

            var images = await query
                .OrderBy(i => i.SortOrder)
                .Select(i => new GetAllImagesAdminDTO
                {
                    Id = i.Id,
                    CommissionTypeId = i.CommissionTypeId,
                    Title = i.Title,
                    ImagePath = i.ImagePath,
                    ThumbPath = i.ThumbPath,
                    SortOrder = i.SortOrder,
                    IsVisible = i.IsVisible,
                    CreatedAt = i.CreatedAt,
                    UpdatedAt = i.UpdatedAt
                })
                .ToListAsync(); return images;
        }

        public async Task<bool> Update(int id, UpdateImageDTO dto)
        {
            var image = await _context.Images.FindAsync(id);
            if(image == null)
            {
                return false;
            }

            image.CommissionTypeId = dto.CommissionTypeId;
            image.Title = dto.Title;
            image.SortOrder = dto.SortOrder;
            image.IsVisible = dto.IsVisible;
            image.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UploadNewImage(ImageUploadDTO dto)
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
