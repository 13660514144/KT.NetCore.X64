using KT.Common.Core.Utils;
using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Unit.ClientApp.Dao.Base;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.Daos
{
    public class PassRightDao : BaseDataDao<TurnstileUnitPassRightEntity>, IPassRightDao
    {
        private TurnstileUnitContext _context;
        private ILogger _logger;

        public PassRightDao(TurnstileUnitContext context,
            ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TurnstileUnitPassRightEntity> GetByCardNubmerAndCardDeviceId(string cardNumber, string cardDeviceId)
        {
            var timeNow = DateTimeUtil.UtcNowMillis();
            _logger.LogInformation("结束授权查询：cardNumber:{0} cardDeviceId:{1} ", cardNumber, cardDeviceId);

            var rights = await _context.PassRights
                .Include(x => x.Details)
                .ThenInclude(x => x.RightGroup.Details)
                .Where(x => x.CardNumber == cardNumber)
                .ToListAsync();

            if (rights?.FirstOrDefault() == null)
            {
                _logger.LogInformation("根据卡号查询不到权限！");
                return null;
            }

            foreach (var item in rights)
            {
                var details = await _context.PassRightDetails.Where(x => x.PassRight.Id == item.Id).OrderByDescending(x => x.EditedTime).ToListAsync();

                if (details?.FirstOrDefault() == null)
                {
                    _logger.LogInformation("权限关联权限组为空：rightId:{0} ", item.Id);
                    continue;
                }

                foreach (var obj in details)
                {
                    var rightGroupDetails = await _context.RightGroupDetails.Where(x => x.RightGroupId == obj.RightGroupId).ToListAsync();
                    if (rightGroupDetails?.FirstOrDefault() == null)
                    {
                        _logger.LogInformation("权限组明细为空：rightGroupId:{0} ", obj.Id);
                        continue;
                    }
                    foreach (var rightGroupDetail in rightGroupDetails)
                    {
                        if (rightGroupDetail.CardDeviceId != cardDeviceId && rightGroupDetail.CardDevice?.Id != cardDeviceId)
                        {
                            _logger.LogInformation($"设备Id不相等：inputCardDeviceId:{cardDeviceId} rightCardDeviceId:{rightGroupDetail.CardDevice?.Id}/{ rightGroupDetail.CardDeviceId} ");
                            continue;
                        }
                        _logger.LogInformation("正常权限记录：rightGroupId:{0} ", obj.Id);
                        //正常通行 
                        return item;
                    }
                }
            }
            return null;
        }


        //           foreach (var item in rights)
        //            {
        //                //item.Details = await _context.PassRightDetails.Where(x => x.PassRight.Id == item.Id).OrderByDescending(x => x.EditedTime).ToListAsync();
        //                item.Details.ForEach(x => x.PassRight = null);

        //                _logger.LogInformation($"卡号查询到的通行权限：passRight:{JsonConvert.SerializeObject(item, JsonUtil.JsonPrintSettings)} ");

        //                if (item.Details == null || item.Details.FirstOrDefault() == null)
        //                {
        //                    _logger.LogInformation("权限关联权限组为空：rightId:{0} ", item.Id);
        //                    continue;
        //                }

        //                foreach (var obj in item.Details)
        //                {
        //                    //var rightGroupDetails = await _context.RightGroupDetails.Where(x => x.RightGroupId == obj.RightGroupId).ToListAsync();

        //                    if (obj.RightGroup?.Details?.FirstOrDefault() == null)
        //                    {
        //                        _logger.LogInformation("权限组明细为空：rightGroupId:{0} ", obj.Id);
        //                        continue;
        //                    }

        //obj.RightGroup.Details.ForEach(x => x.RightGroup = null);
        //_logger.LogInformation($"权限组关联读卡器明细：rightGroupDetails:{JsonConvert.SerializeObject(obj.RightGroup.Details, JsonUtil.JsonPrintSettings)} ");

        //foreach (var rightGroupDetail in obj.RightGroup.Details)
        //{
        //    if (rightGroupDetail.CardDevice?.Id != cardDeviceId)
        //    {
        //        _logger.LogInformation("设备Id不相等：inputCardDeviceId:{cardDeviceId} rightCardDeviceId:{rightGroupDetail.CardDevice?.Id} ");
        //        continue;
        //    }
        //    _logger.LogInformation("正常权限记录：rightGroupId:{0} ", obj.Id);
        //    //正常通行 
        //    return item;
        //}
        //                }
        //            }
        //            return null;
        //        }

        public async Task<PageData<TurnstileUnitPassRightEntity>> GetPageWithDetailsAsync(int page, int size)
        {
            var pageData = new PageData<TurnstileUnitPassRightEntity>();
            pageData.Page = page;
            pageData.Size = size;

            var results = await _context.PassRights
                .Include(x => x.Details)
                .Skip((page - 1) * size)
                .Take(size)
                .OrderByDescending(x => x.EditedTime)
                .ToListAsync();

            pageData.List = results;

            return pageData;
        }

        public async Task<TurnstileUnitPassRightEntity> GetWithDetailsByIdAsync(string id)
        {
            var result = await _context.PassRights
                  .Include(x => x.Details)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
