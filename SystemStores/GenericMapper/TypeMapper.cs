using Omu.ValueInjecter;
using SystemStores.Interfaces;

namespace SystemStores.GenericMapper
{
    public class TypeMapper<TSource, TTarget> : ITypeMapper<TSource, TTarget>
    {
        public virtual TTarget Map(TSource source, TTarget target)
        {
            ((object)target).InjectFrom(new object[1]
            {
        (object) source
            }).InjectFrom<NullablesToNormal>(new object[1]
            {
        (object) source
            }).InjectFrom<NormalToNullables>(new object[1]
            {
        (object) source
            }).InjectFrom<IntToEnum>(new object[1]
            {
        (object) source
            }).InjectFrom<EnumToInt>(new object[1]
            {
        (object) source
            }).InjectFrom<MapperInjection>(new object[1]
            {
        (object) source
            });
            return target;
        }
    }
}
