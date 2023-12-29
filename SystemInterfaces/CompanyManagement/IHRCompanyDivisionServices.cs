﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInterfaces.CompanyManagement
{
    public interface IHRCompanyDivisionServices<TEntity>
    {
        Task<IPagedList<TEntity>> GetsBySearchKey(int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey);
    }
}
