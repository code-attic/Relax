using Symbiote.Core.Extensions;

namespace Symbiote.Relax.Impl
{
    public class CacheKeyBuilder : ICacheKeyBuilder
    {
        public string GetKey<TModel>(object id)
        {
            return "{0}_{1}"
                .AsFormat(typeof(TModel).FullName, id);
        }

        public string GetKey<TModel>(object id, object rev)
        {
            return "{0}_{1}_{2}"
                .AsFormat(typeof(TModel).FullName, id, rev);
        }

        public string GetListKey<TModel>()
        {
            return "{0}_list"
                .AsFormat(typeof(TModel).FullName);
        }

        public string GetListKey<TModel>(int page, int size)
        {
            return "{0}_{1}_{2}"
                .AsFormat(typeof(TModel).FullName, page, size);
        }
    }
}