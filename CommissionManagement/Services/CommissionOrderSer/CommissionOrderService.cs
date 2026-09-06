using CommissionManagement.DTO.CommissionOrderDTO;
using CommissionManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CommissionManagement.Services.CommissionOrderSer
{
    public class CommissionOrderService : ICommissionOrderService
    {
        private readonly CommissionContext _context;

        public CommissionOrderService(CommissionContext context)
        {
            _context = context;
        }

        public async Task<DrawResultDTO> DrawOrdersAsync(DrawDTO drawDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var waitingOrders = await _context.CommissionOrders
                    .Where(o => o.PeriodId == drawDto.PeriodId && o.SelectionStatus == 1)
                    .ToListAsync();

                var drawCountLimit = await _context.CommissionPeriods
                    .Where(p => p.Id == drawDto.PeriodId)
                    .Select(p => p.MaxWinners)
                    .FirstOrDefaultAsync();

                if(drawCountLimit != drawDto.DrawCount)
                {
                    throw new InvalidOperationException("抽籤人數與設定不符");
                }

                int totalNumber = waitingOrders.Count;
                if(totalNumber == 0)
                {
                    throw new InvalidOperationException("該委託期無等待抽選的委託單");
                }

                var random = new Random();
                //打亂順序排列
                var shuffledOrders = waitingOrders.OrderBy(o => random.Next()).ToList();
                //選出最小值數量 避免抽籤人數大於總人數出問題
                int actualDrawCount = Math.Min(drawDto.DrawCount, totalNumber);
                //從亂數清單中索引值0開始取出指定數量的委託單
                var selectedOrders = shuffledOrders.Take(actualDrawCount).ToList();


                foreach (var order in selectedOrders)
                {
                    order.SelectionStatus = 2;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new DrawResultDTO
                {
                    TotalNumber = totalNumber,
                    SelectedNumber = selectedOrders.Count,
                    SelectedOrderIds = selectedOrders.Select(o => o.Id).ToList()
                };
                
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateOrder(int Id, OrderUpdateDTO updateDto)
        {
            var order = await _context.CommissionOrders.FirstOrDefaultAsync(o => o.Id == Id);
            if(order == null)
            {
                return false;
            }

            if(updateDto.PaymentStatus > 3 || updateDto.PaymentStatus < 0)
            {
                return false;
            }

            if(updateDto.WorkStatus > 5 || updateDto.WorkStatus < 0)
            {
                return false;
            }
            if(updateDto.ScheduledDate.HasValue && updateDto.ScheduledDate.Value < DateOnly.FromDateTime(DateTime.Today))
            {
                return false;
            }

            order.PaymentStatus = updateDto.PaymentStatus;
            order.WorkStatus = updateDto.WorkStatus;
            order.ScheduledDate = updateDto.ScheduledDate;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ShowAllOrder>> ShowOrderAdmin(int periodId)
        {
            var orders = await (
                from o in _context.CommissionOrders
                join p in _context.CommissionPeriods on o.PeriodId equals p.Id
                join s in _context.SocialPlatforms on o.SocialId equals s.Id into socialGroup
                from s in socialGroup.DefaultIfEmpty()
                join c in _context.CommissionTypes on o.CommissionTypeId equals c.Id
                where o.PeriodId == periodId && o.SelectionStatus == 2
                select new ShowAllOrder
                {
                    Id = o.Id,
                    OrderCode = o.OrderCode,
                    Title = p.Title,
                    Nickname = o.Nickname,
                    Email = o.Email,
                    SocialName = s != null ? s.SocialName : null,
                    TypeName = c.TypeName,
                    CommissionSetting = o.CommissionSetting,
                    PaymentStatus = o.PaymentStatus,
                    WorkStatus = o.WorkStatus,
                    SelectionStatus = o.SelectionStatus,
                    AdminNote = o.AdminNote,
                    ScheduledDate = o.ScheduledDate,
                    CreatedAt = o.CreatedAt
                }).ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<ShowOrderGuest>> ShowOrderGuest()
        {
            var latestTwoPrriodIds = await _context.CommissionPeriods
                .OrderByDescending(p => p.Id)
                .Select(p => p.Id)
                .Take(2)
                .ToListAsync();

            if(latestTwoPrriodIds.Count == 0)
            {
                return new List<ShowOrderGuest>();
            }
            

            var orders = await (
                from o in _context.CommissionOrders
                join s in _context.SocialPlatforms on o.SocialId equals s.Id into socialGroup
                from s in socialGroup.DefaultIfEmpty()
                join c in _context.CommissionTypes on o.CommissionTypeId equals c.Id
                where latestTwoPrriodIds.Contains(o.PeriodId) && o.SelectionStatus == 2
                select new ShowOrderGuest
                {
                    OrderCode = o.OrderCode,
                    Nickname = o.Nickname,
                    SocialName = s != null ? s.SocialName : null,
                    TypeName = c.TypeName,
                    PaymentStatus = o.PaymentStatus,
                    WorkStatus = o.WorkStatus,
                    ScheduledDate = o.ScheduledDate
                }).ToListAsync();

            return orders;
        }
    }
}
