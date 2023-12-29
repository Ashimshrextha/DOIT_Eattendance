using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GenericMapper
{
    public static class Creator
    {
        public static object Create(Type type)
        {
            if (type.IsEnumerable())
                return Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]));
            if (type.IsInterface)
                throw new Exception("don't know any implementation of this type: " + type.Name);
            return Activator.CreateInstance(type);
        }
    }
}
