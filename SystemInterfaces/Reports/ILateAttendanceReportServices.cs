﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemInterfaces.Reports
{
    public interface ILateAttendanceReportServices<TEntity>
	{
		Task<ICollection<TEntity>> GetResult(Func<TEntity, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus,long? idHREemployee, DateTime date);
		Task<IPagedList<TEntity>> GetPagedList(Func<TEntity, bool> condition, long? idHRCompany, long? idHRCompanyDivision, int? idJobStatus,long? idHREmployee, DateTime date, int? pageNumber, int? pageSize, string orderingBy, string orderingDirection, string searchKey = "");
	}
}
