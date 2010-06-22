namespace Relax.Impl.Cache
{
    public interface ICacheKeyBuilder
    {
        string GetKey<TModel>(object id);
        string GetKey<TModel>(object id, string rev);
        string GetRangeKey<TModel>(object startKey, object endKey);
        string GetListKey<TModel>();
        string GetListKey<TModel>(int page, int size);
    }
}