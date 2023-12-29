namespace SystemInterfaces.Auditable
{
    public interface IEntityId<TKey>
    {
        TKey Id { get; set; }
    }
}
