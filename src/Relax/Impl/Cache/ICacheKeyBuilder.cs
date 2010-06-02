namespace Relax.Impl.Cache
{
    public interface ICacheKeyBuilder
    {
        string GetKey<TModel>(object id);
        string GetKey<TModel>(object id, object rev);
        string GetListKey<TModel>();
        string GetListKey<TModel>(int page, int size);
    }
}