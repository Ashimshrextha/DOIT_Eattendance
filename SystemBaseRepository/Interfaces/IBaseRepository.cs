using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SystemBaseRepository.Interfaces
{
     interface IBaseRepository<TEntity, TModel>
    {
        //Mapping Database EntityModel to User defined Model
        Task<TModel> Map_EntityToModelAsync(TEntity entity);

        //Mapping User Defined Model To Database EntityModel
        Task<TEntity> Map_ModelToEntityAsync(TModel model);

        //Get Data through User defined Model
        Task<TModel> Get_ModelByIdAsync(object id);

        //Get Database EntityModel 
        Task<TEntity> Get_EntityByIdAsync(object id);

        //Get Database EntityModel in pagination List
        Task<IPagedList<TEntity>> GetAllAsync(int? page, int? pageSize, string sort , string sortdir);

        //Check if exists or not
        Task<bool> Exists(object primaryKey);

        //Saving data to Database 
        Task<dynamic> Commit(TModel entityModel, object mode, object primaryKey, bool retIdentity);
    }
}
