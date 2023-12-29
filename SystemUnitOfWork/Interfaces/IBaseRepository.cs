using PagedList;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemUnitOfWork.Interfaces
{
    public interface IBaseRepository<TEntity, TModel> where TEntity : class
    {
        //mapp EntityModel to Model
        TModel MapEntityToModel(TEntity entityModel);

        //map Model To EntityModel
        TEntity MapModelToEntity(TModel model);

        //Get EntityModel data in list  in async
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        //Get EntityModel data single in async
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        //Get By Id
        Task<bool> Exits(Expression<Func<TEntity, bool>> match);

        Task<TEntity> GetByIdAsync(object id);

        Task<TModel> GetModelByIdAsync(object id);
        Task<TModel> GetModelFindAsync(Expression<Func<TEntity, bool>> condition);
        Task<IPagedList<TEntity>> GetPagedList(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> condition);

        IUnitOfWork UnitOfWork { get; }

        Task<dynamic> CommitSaveChangesAsync(TModel entityModel, CRUDType mode, object id = null, bool retIdentity = false);

        Task<dynamic> CommitAsync(TModel entityModel, CRUDType mode, object id = null);
        Task<dynamic> DeleteByIdAsync(object id);
        Task<dynamic> UpdateAsync(TEntity entity);

        Task<IEnumerable<T>> ExecuteProcedure<T>(string query, params object[] param);
        Task<ICollection<TEntity>> ExecuteProcedure(string query, params object[] param);
    }
}
