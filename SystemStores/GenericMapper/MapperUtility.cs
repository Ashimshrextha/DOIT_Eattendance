using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStores.GenericMapper
{
    public static class MapperUtility
    {
        public static TTarget Map<TSource, TTarget>(TSource source, TTarget target)
        {
            target = MapperFactory.GetMapper<TSource, TTarget>().Map(source, target);
            return target;
        }

        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            TTarget target = (TTarget)Creator.Create(typeof(TTarget));
            return MapperFactory.GetMapper<TSource, TTarget>().Map(source, target);
        }

        public static object Map(object source, object target, Type sourceType, Type targetType)
        {
            target = target ?? Creator.Create(targetType);
            object obj = typeof(MapperFactory).GetMethod("GetMapper").MakeGenericMethod(sourceType, targetType).Invoke((object)null, (object[])null);
            return obj.GetType().GetMethod(nameof(Map)).Invoke(obj, new object[2]
            {
        source,
        target
            });
        }
    }
}
