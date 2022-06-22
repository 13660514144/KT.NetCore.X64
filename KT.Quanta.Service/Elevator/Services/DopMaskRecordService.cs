using AutoMapper;
using KT.Common.Data.Models;
using KT.Quanta.Entity.Kone;
using KT.Quanta.IDao.Kone;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Elevator.Services
{
    public class DopMaskRecordService : IDopMaskRecordService
    {
        private readonly IDopMaskRecordDao _dopMaskRecordDao;
        private readonly IMapper _mapper;

        public DopMaskRecordService(IDopMaskRecordDao dopMaskRecordDao,
            IMapper mapper)
        {
            _dopMaskRecordDao = dopMaskRecordDao;
            _mapper = mapper;
        }

        public async Task<PageData<DopMaskRecordModel>> GetListAsync(PageQuery<DopMaskRecordQuery> pageQuery)
        {
            var wheres = new List<Expression<Func<DopMaskRecordEntity, bool>>>();

            var query = _dopMaskRecordDao.SelectAll();
            if (!string.IsNullOrEmpty(pageQuery?.Query?.ElevatorServer))
            {
                wheres.Add(x => x.ElevatorServer == pageQuery.Query.ElevatorServer);
            }
            if (!string.IsNullOrEmpty(pageQuery?.Query?.Type))
            {
                wheres.Add(x => x.Type == pageQuery.Query.Type);
            }
            if (!string.IsNullOrEmpty(pageQuery?.Query?.Operate))
            {
                wheres.Add(x => x.Operate == pageQuery.Query.Operate);
            }
            if (pageQuery?.Query?.IsSuccess.HasValue == true)
            {
                wheres.Add(x => x.IsSuccess == pageQuery.Query.IsSuccess);
            }
            if (pageQuery?.Query?.Status.HasValue == true)
            {
                wheres.Add(x => x.Status == pageQuery.Query.Status);
            }

            var entities = await _dopMaskRecordDao.SelectOrderByDescendingPageByLambdasAsync(wheres, x => x.EditedTime, pageQuery.Page, pageQuery.Size);

            var pageData = new PageData<DopMaskRecordModel>();
            pageData.Page = pageQuery.Page;
            pageData.Size = pageQuery.Size;
            pageData.List = _mapper.Map<List<DopMaskRecordModel>>(entities);

            return pageData;
        }
    }
}
