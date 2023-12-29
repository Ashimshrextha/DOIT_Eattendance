using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SystemDatabase;
using SystemStores.GlobalModels;

namespace SystemServices.SystemAuthentication
{
    public class AccountRepo
    {
 
        public static bool CheckPermission(string Area, string Controller, string Action)
        {
            UserSessionModel model = ((UserSessionModel)HttpContext.Current.Session["UserSession"]);
            var params1 = new SqlParameter() { ParameterName = "@p1", SqlDbType = SqlDbType.Int, Value = model.IdHREmployeeRole };
            var params2 = new SqlParameter() { ParameterName = "@p2", SqlDbType = SqlDbType.BigInt, Value = model.IdHREmployee };
            var params3 = new SqlParameter() { ParameterName = "@p3", SqlDbType = SqlDbType.NVarChar, Value = Area };
            if (model.IdHREmployeeRole == null) params1.Value = DBNull.Value;
            if (model.IdHREmployee == null) params2.Value = DBNull.Value;
            if (Area == null) params3.Value = DBNull.Value;
            object[] myObjArray = { params1, params2, params3, new SqlParameter() { ParameterName = "@p4", SqlDbType = SqlDbType.NVarChar, Value = Controller }, new SqlParameter() { ParameterName = "@p5", SqlDbType = SqlDbType.NVarChar, Value = Action } };
            using (EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities())
            {
                return db.Database.SqlQuery<bool>("select dbo.f_CheckPermission (@p1,@p2,@p3,@p4,@p5)", myObjArray).FirstOrDefault();
            }
        }
    }
}
