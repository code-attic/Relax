namespace Symbiote.Relax.Impl
{
    public interface ICacheKeyBuilder
    {
        string GetKey<TModel>(object id);
        string GetKey<TModel>(object id, object rev);
        string GetListKey<TModel>();
        string GetListKey<TModel>(int page, int size);
    }
}