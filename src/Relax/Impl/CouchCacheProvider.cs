using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Symbiote.Core.Cache;
using Symbiote.Core.Extensions;


namespace Relax.Impl
{
    class CouchCacheProvider : ICouchCacheProvider
    {
        protected ICacheProvider _cache;
        protected ICacheKeyBuilder _keyBuilder;
        protected ConcurrentDictionary<string, ConcurrentStack<string>> _crossReferences
            = new ConcurrentDictionary<string, ConcurrentStack<string>>();

        public void AddCrossReference(string key, string cacheKey)
        {
            ConcurrentStack<string> list = null;
            if (!_crossReferences.TryGetValue(key, out list))
            {
                list = new ConcurrentStack<string>();
                _crossReferences[key] = list;
            }
            list.Push(cacheKey);
        }

        public void InvalidateItem<TModel>(string affectedKey)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            ConcurrentStack<string> relatedKeys = null;
            if (_crossReferences.TryGetValue(affectedKey, out relatedKeys))
            {
                string key = null;
                while (relatedKeys.TryPop(out key))
                {
                    _cache.Remove(key);
                }
            }
        }

        public void Delete<TModel>(object key, Action<object> delete)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key);
            InvalidateItem<TModel>(key.ToString());
            delete(key);
        }

        public void Delete<TModel>(object key, object rev, Action<object, object> delete)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            InvalidateItem<TModel>(key.ToString());
            delete(key, rev);
        }

        public void DeleteAll<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            _crossReferences.Keys.ForEach(InvalidateItem<TModel>);
        }

        public TModel Get<TModel>(object key, object rev, Func<object, object, TModel> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            var result = _cache.Get<TModel>(cacheKey);
            if (result == null)
            {
                result = retrieve(key, rev);
                _cache.Store(cacheKey, result);
                AddCrossReference(result.GetIdAsJson(), cacheKey);
            }
            return result;
        }

        public TModel Get<TModel>(object key, Func<object, TModel> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key);
            var result = _cache.Get<TModel>(cacheKey);
            if (result == null)
            {
                result = retrieve(key);
                _cache.Store(cacheKey, result);
                AddCrossReference(result.GetIdAsJson(), cacheKey);
            }
            return result;
        }

        public IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetListKey<TModel>();
            var result = _cache.Get<IList<TModel>>(cacheKey);
            if (result == null)
            {
                result = retrieve();
                _cache.Store(cacheKey, result);
                result.ForEach(x => AddCrossReference(x.GetIdAsJson(), cacheKey));
            }
            return result;
        }

        public IList<TModel> GetAll<TModel>(int pageSize, int pageNumber, Func<int, int, IList<TModel>> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var cacheKey = _keyBuilder.GetListKey<TModel>(pageNumber, pageSize);
            var result = _cache.Get<IList<TModel>>(cacheKey);
            if (result == null)
            {
                result = retrieve(pageNumber, pageSize);
                _cache.Store(cacheKey, result);
                result.ForEach(x => AddCrossReference(x.GetIdAsJson(), cacheKey));
            }
            return result;
        }

        public void Save<TModel>(TModel model, Action<TModel> save)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            InvalidateItem<TModel>(model.GetIdAsJson());
            save(model);
            CacheSavedModel(model);
        }

        protected void CacheSavedModel<TModel>(TModel model)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var simpleKey = _keyBuilder.GetKey<TModel>(model.GetIdAsJson());
            var revKey = _keyBuilder.GetKey<TModel>(model.GetIdAsJson(), model.GetRevAsJson());
            _cache.Store(simpleKey, model);
            _cache.Store(revKey, model);
        }

        public void Save<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            list.ForEach(x => InvalidateItem<TModel>(x.GetIdAsJson()));
            save(list);
            list.ForEach(CacheSavedModel);
        }

        public CouchCacheProvider(ICacheProvider cache, ICacheKeyBuilder keyBuilder)
        {
            _cache = cache;
            _keyBuilder = keyBuilder;
        }
    }
}
