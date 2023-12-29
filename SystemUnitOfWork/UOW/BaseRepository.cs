using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SystemStores.GenericMapper;
using SystemUnitOfWork.Interfaces;
using static SystemStores.ENUMData.EnumGlobal;
using static SystemStores.GlobalMethod.SystemGlobalMethod;

namespace SystemUnitOfWork.UOW
{
    public class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel> where TEntity : class
    {
        public readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            this.UnitOfWork = unitOfWork;
            this._dbSet = this.UnitOfWork.Db.Set<TEntity>();
        }


        public virtual async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return await _dbSet.Where(condition).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(condition);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IPagedList<TEntity>> GetPagedList(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            var paginationValue = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
            var model = _dbSet.OrderBy(paginationValue.OrderingBy + " " + paginationValue.OrderingDirection)
                 .ToPagedList(paginationValue.PageNumber, paginationValue.PageSize);
            await Task.WhenAll();
            return model;
        }

        public virtual async Task<IPagedList<TEntity>> GetPagedList(Expression<Func<TEntity, bool>> condition, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            var paginationValue = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
            var model = _dbSet.Where(condition).OrderBy(paginationValue.OrderingBy + " " + paginationValue.OrderingDirection)
                 .ToPagedList(paginationValue.PageNumber, paginationValue.PageSize);
            await Task.WhenAll();
            return model;
        }
        public virtual async Task<IPagedList<TEntity>> GetPagedLists(Expression<Func<TEntity, bool>> condition, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection)
        {
            var paginationValue = Get_PaginationValue(pageNumber, pageSize, orderingBy, orderingDirection);
            var model = _dbSet.Where(condition).OrderBy(paginationValue.OrderingBy + " " + paginationValue.OrderingDirection)
                 .ToPagedList(paginationValue.PageNumber, paginationValue.PageSize);
            await Task.WhenAll();
            return model;
        }

        public virtual IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters);
        }

        public virtual TModel MapEntityToModel(TEntity entityModel)
        {
            try
            {
                if (entityModel == null)
                {
                    throw new ArgumentNullException("model Null");
                }
                else
                    return MapperUtility.Map<TEntity, TModel>(entityModel);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual TEntity MapModelToEntity(TModel model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model null");
                }
                else
                    return MapperUtility.Map<TModel, TEntity>(model);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual IEnumerable<TModel> MapEntityListToModelList(IEnumerable<TEntity> entityModel)
        {
            try
            {
                return MapperUtility.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entityModel);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual IEnumerable<TEntity> MapModelListToEntityList(IEnumerable<TModel> model)
        {
            try
            {
                return MapperUtility.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(model);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<TModel> GetModelFindAsync(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                var model = await FindAsync(condition);
                if (model != null)
                    return MapEntityToModel(model);
                else
                    return default(TModel);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<TModel> GetModelByIdAsync(object id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException("Invalid Id");
                }
                var model = await GetByIdAsync(id);
                if (model != null)
                {
                    return MapEntityToModel(model);
                }
                else
                    return default(TModel);
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.Where(condition).CountAsync();
        }

        public virtual async Task<dynamic> CommitSaveChangesAsync(TModel entityModel, CRUDType mode, object id = null, bool retIdentity = true)
        {
            try
            {
                dynamic entity = MapModelToEntity(entityModel);
                switch (mode)
                {
                    case CRUDType.CREATE:
                        dynamic insertObject = _dbSet.Add(entity);
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? insertObject.Id : -1;

                    case CRUDType.UPDATE:
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Modified;
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? ((dynamic)entity).Id : -1;

                    case CRUDType.DELETE:
                        entity = await GetByIdAsync(entity.Id);
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Deleted;
                        await this.UnitOfWork.SaveAsync();
                        return retIdentity ? ((dynamic)entity).Id : -1;
                    default:
                        return -1;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task CommitBulkSaveChangesAsync(IEnumerable<TModel> entityModel, CRUDType mode, object id = null, bool retIdentity = true)
        {
            try
            {
                dynamic entity = MapModelListToEntityList(entityModel);
                switch (mode)
                {
                    case CRUDType.CREATE:
                        _dbSet.AddRange(entity);
                        await this.UnitOfWork.SaveAsync();
                        break;
                    case CRUDType.UPDATE:
                        foreach (var item in entity)
                        {
                            _dbSet.Attach(item);
                            UnitOfWork.Db.Entry(item).State = EntityState.Modified;
                        }
                        await this.UnitOfWork.SaveAsync();
                        break;
                    case CRUDType.DELETE:
                        foreach (var item in entity)
                        {
                            _dbSet.Attach(item);
                            UnitOfWork.Db.Entry(item).State = EntityState.Deleted;
                        }
                        //_dbSet.RemoveRange(entity);
                        await this.UnitOfWork.SaveAsync();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<dynamic> CommitAsync(TModel entityModel, CRUDType cRUDType, object id = null)
        {
            try
            {
                dynamic entity = MapModelToEntity(entityModel);
                switch (cRUDType)
                {
                    case CRUDType.CREATE:
                        _dbSet.Add(entity);
                        return (dynamic)entity.Id;

                    case CRUDType.UPDATE:
                        _dbSet.Attach(entity);
                        UnitOfWork.Db.Entry(entity).State = EntityState.Modified;
                        return entity.Id;

                    case CRUDType.DELETE:
                        entity = await GetByIdAsync(id);
                        _dbSet.Attach(entity);
                        _dbSet.Remove(entity);
                        return id;
                    default:
                        return 0;
                }
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<dynamic> DeleteByIdAsync(object id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                return id;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<dynamic> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                UnitOfWork.Db.Entry(entity).State = EntityState.Modified;
                await Task.WhenAll();
                return ((dynamic)entity).Id;
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual Task<bool> Exits(Expression<Func<TEntity, bool>> match)
        {
            try
            {
                return _dbSet.AnyAsync(match);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteProcedure<T>(string query, params object[] param)
        {
            try
            {
                return await UnitOfWork.Db.Database.SqlQuery<T>(query, param).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<T> ExecuteScalarFunction<T>(string query, params object[] param)
        {
            try
            {
                return await UnitOfWork.Db.Database.SqlQuery<T>(query, param).FirstOrDefaultAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public virtual async Task<ICollection<TEntity>> ExecuteProcedure(string query, params object[] param)
        {
            try
            {
                return await _dbSet.SqlQuery(query, param).ToListAsync();
            }
            catch (Exception exp)
            {
                throw new ArgumentException(exp.Message);
            }
        }

        public IUnitOfWork UnitOfWork { get; }

    }
}
