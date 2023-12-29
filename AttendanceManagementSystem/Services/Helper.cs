using System.Linq;
using SystemDatabase;

namespace System.Web.Mvc
{
	public static class staticclass
	{
		public static string DateNp(this DateTime date)
		{
			EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
			{
				try
				{
					return db.ifc_GetDateInfo(date).FirstOrDefault().Nepdate;
				}
				catch (Exception)
				{
					return "";
				}
			}
		}

		public static string DateNp(this DateTime? date)
		{
			EAttendanceSystemDBEntities db = new EAttendanceSystemDBEntities();
			{
				try
				{
                    if (date == null)
                    {
						date = DateTime.Now;
                    }
					return db.ifc_GetDateInfo(date).FirstOrDefault().Nepdate;
				}
				catch (Exception)
				{
					return "";
				}
			}
		}
	}
}