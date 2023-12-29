namespace SystemStores.Interfaces
{
    public interface ITypeMapper<TSource, TTarget>
    {
        TTarget Map(TSource source, TTarget target);
    }
}
