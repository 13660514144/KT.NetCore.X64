using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KT.Common.Data.Daos
{
    public interface IBaseDataDao<T> where T : BaseEntity
    {
        Task SaveChangesAsync();
        //! 检查是否存在持久实体对象
        Task<bool> HasInstanceByIdAsync(string id);
        Task<bool> HasInstanceAsync(Expression<Func<T, bool>> where);
        Task<bool> HasRelevanceInstanceAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity;

        //! 提取单个对象的方法
        Task<T> FindIdAsync(string oid);
        Task<T> SelectByIdAsync(string oid);
        Task<T> SelectFirstByLambdaAsync(Expression<Func<T, bool>> where);
        Task<List<T>> SelectByLambdaAsync(Expression<Func<T, bool>> where);
        Task<List<T>> SelectPageAsync(int page, int size);
        Task<List<T>> SelectPageByLambdaAsync(Expression<Func<T, bool>> where, int page, int size);
        Task<List<T>> SelectPageByLambdasAsync(List<Expression<Func<T, bool>>> wheres, int page, int size);
        Task<List<T>> SelectOrderPageByLambdaAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int page, int size);
        Task<List<T>> SelectOrderByDescendingPageByLambdasAsync(List<Expression<Func<T, bool>>> wheres, Expression<Func<T, object>> order, int page, int size);

        //! 根据 id ，提取实例，如果没有，创建一个新的实例，并执行持久化
        Task<T> SelectOrCreatePersistenceAsync(string id);
        //! 根据 id ，提取实例，如果没有，创建一个新的实例，但不执行持久化
        Task<T> SelectOrCreateNotPersistenceAsync(string id);

        //! 添加对象的方法
        Task InsertAsync(T bo, bool isSave = true);

        //! 删除对象的方法
        Task DeleteAsync(T bo, bool isSave = true);
        Task DeleteByIdAsync(string oid, bool isSave = true);
        Task<List<T>> DeleteByLambdaAsync(Expression<Func<T, bool>> where, bool isSave = true);
        Task DeleteRelevenceObjectAsync<T2>(T2 relevanceBo, bool isSave = true) where T2 : BaseEntity;
        Task DeleteRelevenceObjectsAsync<T2>(List<T2> relevanceBo, bool isSave = true) where T2 : BaseEntity;
        Task<List<T2>> DeleteRelevenceObjectsByLambdaAsync<T2>(Expression<Func<T2, bool>> where, bool isSave = true) where T2 : BaseEntity;

        //! 更新对象的方法
        Task AttachAsync(T bo, bool isSave = true);
        Task UpdateAsync(T bo, bool isSave = true);

        //! 提取对象集合的方法
        Task<List<T>> SelectAllAsync();
        Task<T> SelectLastAsync();
        IQueryable<T> SelectAll();
        IQueryable<T> SelectAllOrdered(Expression<Func<T, bool>> order);
        IQueryable<T> SelectMany(Expression<Func<T, bool>> where);
        IQueryable<T> SelectManyOrdered<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order);

        //! 按照分页方式提取对象集合的方法
        IQueryable<T> SelectAllOrderedPaged<TKey>(
            Expression<Func<T, TKey>> order,
            int page,
            int pageSize,
            ref int pageAmount,
            ref int boAmount,
            ref bool isDesc);
        IQueryable<T> SelectManyOrderedPaged<Tkey>(
            Expression<Func<T, bool>> where,
            Expression<Func<T, Tkey>> order,
            int page,
            int pageSize,
            ref int pageAmount,
            ref int boAmount,
            ref bool isDesc);
        IQueryable<T> SelectAllCommonWay(
          ref int pageAmount,
          ref int boAmount,
          Expression<Func<T, bool>> where = null,
          Expression<Func<T, bool>> order = null,
          bool? isDesc = null,
          int? page = null,
          int? pageSize = null);

        //! 数据处理约束管理方法
        Task<bool> CanInsertAsync(Expression<Func<T, bool>> where);

        //! 用于处理一些附加对象的方法
        Task<T2> SelectRelevanceObjectAsync<T2>(string id) where T2 : BaseEntity;
        Task<T2> SelectRelevanceObjectAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity;
        Task<List<T2>> SelectRelevanceObjectsAsync<T2>() where T2 : BaseEntity;
        Task<List<T2>> SelectRelevanceObjectsAsync<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity;

        IQueryable<T2> SelectRelevanceObjects<T2>(Expression<Func<T2, bool>> where) where T2 : BaseEntity;
        IQueryable<T2> SelectRelevanceObjects<T2>(
            Expression<Func<T2, bool>> where = null,
            Expression<Func<T2, bool>> order = null,
            bool? isDesc = null) where T2 : BaseEntity;

        Task InsertRelevanceAsync<T2>(T2 relevanceBo, bool isSave = true) where T2 : BaseEntity;
        Task UpdateRelevanceAsync<T2>(T2 relevanceBo, bool isSave = true) where T2 : BaseEntity;
    }
}
