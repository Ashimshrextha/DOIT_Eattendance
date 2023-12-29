using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemStores.Interfaces;

namespace SystemStores.GenericMapper
{
    public class EnumerableTypeMapper<TSource, TTarget> : ITypeMapper<TSource, TTarget> where TSource : class where TTarget : class
    {
        public TTarget Map(TSource source, TTarget target)
        {
            if ((object)source == null)
                return default(TTarget);
            Type genericArgument = typeof(TTarget).GetGenericArguments()[0];
            object instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericArgument));
            MethodInfo method = instance.GetType().GetMethod("Add");
            foreach (object source1 in (object) source as IEnumerable)
            {
                object target1 = Creator.Create(genericArgument);
                method.Invoke(instance, new object[1]
                {
          MapperUtility.Map(source1, target1, source1.GetType(), genericArgument)
                });
            }
            return (TTarget)instance;
        }
    }
}
