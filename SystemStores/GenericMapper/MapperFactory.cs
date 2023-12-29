using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStores.Interfaces;

namespace SystemStores.GenericMapper
{
    public static class MapperFactory
    {
        private static readonly IDictionary<Type, object> Mappers = (IDictionary<Type, object>)new Dictionary<Type, object>();

        public static ITypeMapper<TSource, TTarget> GetMapper<TSource, TTarget>()
        {
            if (MapperFactory.Mappers.ContainsKey(typeof(ITypeMapper<TSource, TTarget>)))
                return MapperFactory.Mappers[typeof(ITypeMapper<TSource, TTarget>)] as ITypeMapper<TSource, TTarget>;
            if (!typeof(TSource).IsEnumerable() || !typeof(TTarget).IsEnumerable())
                return (ITypeMapper<TSource, TTarget>)new TypeMapper<TSource, TTarget>();
            return (ITypeMapper<TSource, TTarget>)Activator.CreateInstance(typeof(EnumerableTypeMapper<,>).MakeGenericType(typeof(TSource), typeof(TTarget)));
        }

        public static void AddMapper<TS, TT>(ITypeMapper<TS, TT> o)
        {
            MapperFactory.Mappers.Add(typeof(ITypeMapper<TS, TT>), (object)o);
        }

        public static void ClearMappers()
        {
            MapperFactory.Mappers.Clear();
        }
    }
}
