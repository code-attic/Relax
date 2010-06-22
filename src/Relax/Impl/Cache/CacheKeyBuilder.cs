using System;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Cache
{
    public class CacheKeyBuilder : ICacheKeyBuilder
    {
        public string GetKey<TModel>(object id)
        {
            return "{0}_{1}"
                .AsFormat(typeof(TModel).FullName, GetIdAsValidString(id));
        }

        public string GetKey<TModel>(object id, string rev)
        {
            return "{0}_{1}_{2}"
                .AsFormat(typeof(TModel).FullName, GetIdAsValidString(id), rev);
        }

        public string GetRangeKey<TModel>(object startKey, object endKey)
        {
            return "{0}_{1}_{2}"
                .AsFormat(typeof(TModel).FullName, GetIdAsValidString(startKey), GetIdAsValidString(endKey));
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

        public string GetIdAsValidString(object id)
        {
            var type = id.GetType();
            if(
                type.FullName.StartsWith("System") && 
                (type.GetInterface("IEnumerable") == null || type.Equals(typeof(string))))
            {
                return id.ToString();
            }
            return id.ToJson(false);
        }
    }
}