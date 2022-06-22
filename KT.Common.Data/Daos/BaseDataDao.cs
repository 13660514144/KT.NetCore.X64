using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace KT.Common.Data.Daos
{
    /// <summary>
    /// CodeFirst 独立的针对 Service 方法的实现，其实际实现时通过继承的方式处理
    /// </summary>
    public abstract class BaseDataDao<T> : IBaseDataDao<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseDataDao(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            //_context.SaveChanges();
        }
        public async Task<bool> HasInstanceAsync(Expression<Func<T, bool>> where)
        {
            if (await _dbSet.FirstOrDefaultAsync(where) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> HasInstanceByIdAsync(string id)
        {
            if (id == "0" || id == IdUtil.EmptyId())
            {
                return false;
            }
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return entity != null;
        }

        public async Task<bool> HasRelevanceInstanceAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>() as IQueryable<T2>;
            if (await dbSet.FirstOrDefaultAsync(where) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> SelectByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)
                || id == "0"
                || id == "-1"
                || id == IdUtil.EmptyId())
            {
                return null;
            }
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FindIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)
                || id == "0"
                || id == "-1"
                || id == IdUtil.EmptyId())
            {
                return null;
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> SelectFirstByLambdaAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.FirstOrDefaultAsync(where);
        }

        public async Task<List<T>> SelectByLambdaAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task<List<T>> SelectPageAsync(int page, int size)
        {
            return await _dbSet.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<List<T>> SelectPageByLambdaAsync(Expression<Func<T, bool>> where, int page, int size)
        {
            return await _dbSet.Where(where).Skip((page - 1) * size).Take(size).ToListAsync();
        }
        public async Task<List<T>> SelectPageByLambdasAsync(List<Expression<Func<T, bool>>> wheres, int page, int size)
        {
            var query = _dbSet as IQueryable<T>;
            if (wheres?.Any() == true)
            {
                foreach (var where in wheres)
                {
                    query = query.Where(where);
                }
            }

            query = query.Skip((page - 1) * size).Take(size);

            return await query.ToListAsync();
        }
        public async Task<List<T>> SelectOrderByDescendingPageByLambdasAsync(List<Expression<Func<T, bool>>> wheres, Expression<Func<T, object>> order, int page, int size)
        {
            var query = _dbSet as IQueryable<T>;
            if (wheres?.Any() == true)
            {
                foreach (var where in wheres)
                {
                    query = query.Where(where);
                }
            }

            query = query.OrderByDescending(order).Skip((page - 1) * size).Take(size);

            return await query.ToListAsync();
        }

        public async Task<List<T>> SelectOrderPageByLambdaAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int page, int size)
        {
            return await _dbSet.Where(where).OrderBy(order).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<T> SelectLastAsync()
        {
            var entity = await _dbSet.OrderByDescending(x => x.EditedTime).FirstOrDefaultAsync();
            return entity;
        }

        public IQueryable<T> SelectAllOrdered(Expression<Func<T, bool>> order)
        {
            return _dbSet.OrderBy(order);
        }

        public async Task<T> SelectOrCreatePersistenceAsync(string id)
        {
            var fObject = await this.SelectByIdAsync(id);
            if (fObject != null)
            {
                return fObject;
            }
            else
            {
                fObject = Activator.CreateInstance<T>();
                await this.InsertAsync(fObject);
                return fObject;
            }
        }

        public async Task<T> SelectOrCreateNotPersistenceAsync(string id)
        {
            var fObject = await this.SelectByIdAsync(id);
            if (fObject != null)
            {
                return fObject;
            }
            else
            {
                fObject = Activator.CreateInstance<T>();
                return fObject;
            }
        }

        public async Task InsertAsync(T bo, bool isSave = true)
        {
            if (string.IsNullOrEmpty(bo.Id) || bo.Id == "0" || bo.Id == IdUtil.EmptyId())
            {
                bo.Id = IdUtil.NewId();
            }
            var time = DateTimeUtil.UtcNowMillis();
            if (bo.CreatedTime <= 0)
            {
                bo.CreatedTime = time;
            }
            if (bo.EditedTime <= 0)
            {
                bo.EditedTime = time;
            }

            await _dbSet.AddAsync(bo);

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
                
            }
        }

        public async Task DeleteAsync(T bo, bool isSave = true)
        {
            _dbSet.Remove(bo);
            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }

        public async Task DeleteByIdAsync(string oid, bool isSave = true)
        {
            var bo = await _dbSet.FirstOrDefaultAsync(x => x.Id == oid);
            if (bo == null)
            {
                return;
            }
            _dbSet.Remove(bo);

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }
        public async Task<List<T>> DeleteByLambdaAsync(Expression<Func<T, bool>> where, bool isSave = true)
        {
            var entities = await _dbSet.Where(where).ToListAsync();

            if (entities?.FirstOrDefault() != null)
            {
                _dbSet.RemoveRange(entities);

                if (isSave)
                {
                    await _context.SaveChangesAsync();
                    //_context.SaveChanges();
                }
            }

            return entities;
        }

        public async Task DeleteRelevenceObjectAsync<T2>(T2 relevanceBo, bool isSave = true) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            dbSet.Remove(relevanceBo);

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }

        public async Task DeleteRelevenceObjectsAsync<T2>(List<T2> relevanceBos, bool isSave = true) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            dbSet.RemoveRange(relevanceBos);

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }

        public async Task<List<T2>> DeleteRelevenceObjectsByLambdaAsync<T2>(Expression<Func<T2, bool>> where, bool isSave = true) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            var tempCollection = dbSet as IQueryable<T2>;
            var entities = await tempCollection.Where(where).ToListAsync();

            if (entities?.FirstOrDefault() != null)
            {
                dbSet.RemoveRange(entities);

                if (isSave)
                {
                    await _context.SaveChangesAsync();
                    //_context.SaveChanges();
                }
            }

            return entities;
        }

        public async Task AttachAsync(T bo, bool isSave = true)
        {
            if (string.IsNullOrEmpty(bo.Id) || bo.Id == "0" || bo.Id == IdUtil.EmptyId())
            {
                throw CustomException.Run("修改错误：Id主键不能为空！");
            }
            if (bo.EditedTime <= 0)
            {
                bo.EditedTime = DateTimeUtil.UtcNowMillis();
            }

            _dbSet.Attach(bo);
            _context.Entry(bo).State = EntityState.Modified;

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }
        public async Task UpdateAsync(T bo, bool isSave = true)
        {
            if (string.IsNullOrEmpty(bo.Id) || bo.Id == "0" || bo.Id == IdUtil.EmptyId())
            {
                throw CustomException.Run("修改错误：Id主键不能为空！");
            }
            if (bo.EditedTime <= 0)
            {
                bo.EditedTime = DateTimeUtil.UtcNowMillis();
            }

            _dbSet.Update(bo);

            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }

        public async Task<List<T>> SelectAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> SelectAll()
        {
            return _dbSet as IQueryable<T>;
        }


        public IQueryable<T> SelectMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public IQueryable<T> SelectManyOrdered<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order)
        {
            return _dbSet.Where(where).OrderBy(order);
        }

        public IQueryable<T> SelectAllOrderedPaged<TKey>(Expression<Func<T, TKey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _dbSet as IQueryable<T>;
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            if (boAmount > pageAmount * pageSize)
            {
                pageAmount = pageAmount + 1;
            }
            return searchResult.OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> SelectManyOrderedPaged<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> order, int page, int pageSize, ref int pageAmount, ref int boAmount, ref bool isDesc)
        {
            var searchResult = _dbSet.Where(where);
            boAmount = searchResult.Count();
            pageAmount = boAmount / pageSize;
            if (boAmount > pageAmount * pageSize)
            {
                pageAmount = pageAmount + 1;
            }

            if (!isDesc)
            {
                return searchResult.OrderBy(order).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return searchResult.OrderByDescending(order).Skip((page - 1) * pageSize).Take(pageSize);
            }
        }

        public IQueryable<T> SelectAllCommonWay(
            ref int pageAmount,
            ref int boAmount,
            Expression<Func<T, bool>> where = null,
            Expression<Func<T, bool>> order = null,
            bool? isDesc = null,
            int? page = null,
            int? pageSize = null)
        {
            pageAmount = 0;
            boAmount = 0;
            var searchResult = _dbSet as IQueryable<T>;

            if (where != null && order != null)
            {
                searchResult = searchResult.Where(where);
                if (isDesc != null)
                {
                    searchResult = searchResult.OrderByDescending(order);
                }
                else
                {
                    searchResult = searchResult.OrderBy(order);
                }
            }
            else
            {
                if (where != null)
                {
                    searchResult = searchResult.Where(where);
                }
                else
                {
                    if (order != null)
                    {
                        if (isDesc != null)
                        {
                            searchResult = searchResult.OrderByDescending(order);
                        }
                        else
                        {
                            searchResult = searchResult.OrderBy(order);
                        }
                    }
                }
            }

            boAmount = searchResult.Count();
            if (pageAmount == -1)
            {
                pageAmount = -1;
            }

            if (page != null)
            {
                var p = (int)page;
                var ps = (int)pageSize;
                pageAmount = searchResult.Count() / ps;
                searchResult = searchResult.Skip((p - 1) * ps).Take(ps);
            }

            return searchResult;
        }

        public async Task<bool> CanInsertAsync(Expression<Func<T, bool>> where)
        {
            var count = await _dbSet.CountAsync(where);
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<T2> SelectRelevanceObjectAsync<T2>(string id) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<T2> SelectRelevanceObjectAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>() as IQueryable<T2>;
            return await dbSet.Where(where).FirstOrDefaultAsync();
        }

        public async Task<List<T2>> SelectRelevanceObjectsAsync<T2>() where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>() as IQueryable<T2>;
            return await dbSet.ToListAsync();
        }

        public async Task<List<T2>> SelectRelevanceObjectsAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>() as IQueryable<T2>;
            return await dbSet.Where(where).ToListAsync();
        }

        public IQueryable<T2> SelectRelevanceObjects<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            var searchResult = dbSet as IQueryable<T2>;

            return searchResult.Where(where);
        }

        public IQueryable<T2> SelectRelevanceObjects<T2>(
            Expression<Func<T2, bool>> where = null,
            Expression<Func<T2, bool>> order = null,
            bool? isDesc = null) where T2 : BaseEntity
        {
            var dbSet = _context.Set<T2>();
            var searchResult = dbSet as IQueryable<T2>;
            if (where != null && order != null)
            {
                searchResult = searchResult.Where(where);
                if (isDesc != null)
                {
                    searchResult = searchResult.OrderByDescending(order);
                }
                else
                {
                    searchResult = searchResult.OrderBy(order);
                }
            }
            else
            {
                if (where != null)
                {
                    searchResult = searchResult.Where(where);
                }
                else
                {
                    if (order != null)
                    {
                        if (isDesc != null)
                        {
                            searchResult = searchResult.OrderByDescending(order);
                        }
                        else
                        {
                            searchResult = searchResult.OrderBy(order);
                        }
                    }
                }
            }

            return searchResult;
        }


        public async Task InsertRelevanceAsync<T2>(T2 relevanceBo, bool isSave) where T2 : BaseEntity
        {
            if (relevanceBo.Id == "0" || relevanceBo.Id == IdUtil.EmptyId())
            {
                relevanceBo.Id = IdUtil.NewId();
            }
            var time = DateTimeUtil.UtcNowMillis();
            if (relevanceBo.CreatedTime <= 0)
            {
                relevanceBo.CreatedTime = time;
            }
            if (relevanceBo.EditedTime <= 0)
            {
                relevanceBo.EditedTime = time;
            }
            var dbSet = _context.Set<T2>();
            await dbSet.AddAsync(relevanceBo);
            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }

        public async Task UpdateRelevanceAsync<T2>(T2 relevanceBo, bool isSave) where T2 : BaseEntity
        {
            if (relevanceBo.Id == "0" || relevanceBo.Id == IdUtil.EmptyId())
            {
                throw CustomException.Run("修改错误：Id主键不能为空！");
            }
            if (relevanceBo.EditedTime <= 0)
            {
                relevanceBo.EditedTime = DateTimeUtil.UtcNowMillis();
            }
            var dbSet = _context.Set<T2>();
            dbSet.Attach(relevanceBo);
            _context.Entry(relevanceBo).State = EntityState.Modified;
            if (isSave)
            {
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
            }
        }        
    }
}
