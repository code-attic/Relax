using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Relax.Impl.Model;
using Symbiote.Core.Cache;
using Symbiote.Core.Extensions;
using Relax.Impl.Commands;

namespace Relax.Impl.Cache
{
    public class CouchCacheProvider : ICouchCacheProvider
    {
        protected ICacheProvider _cache;
        protected ICacheKeyBuilder _keyBuilder;
        protected IKeyAssociationManager _associationManager;

        public void AddCrossReference(string key, string cacheKey)
        {
            _associationManager.AddKeyAssociation(key, cacheKey);
        }

        public virtual void InvalidateItem<TModel>(string affectedKey)
        {
            _associationManager
                .GetAssociations(affectedKey)
                .ForEach(x => _cache.Remove(x));
        }

        public virtual void Delete<TModel>(object key, Action<object> delete)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key);
            InvalidateItem<TModel>(key.ToString());
            delete(key);
        }

        public virtual void Delete<TModel>(object key, object rev, Action<object, object> delete)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            InvalidateItem<TModel>(key.ToString());
            delete(key, rev);
        }

        public virtual void DeleteAll<TModel>()
        {
            _associationManager.GetAllKeys().ForEach(InvalidateItem<TModel>);
        }

        public virtual TModel Get<TModel>(object key, object rev, Func<object, object, TModel> retrieve)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            var result = _cache.Get<TModel>(cacheKey);
            if (result == null)
            {
                result = retrieve(key, rev);
                _cache.Store(cacheKey, result);
                AddCrossReference(result.GetDocumentIdAsJson(), cacheKey);
            }
            return result;
        }

        public virtual TModel Get<TModel>(object key, Func<object, TModel> retrieve)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key);
            var result = _cache.Get<TModel>(cacheKey);
            if (result == null)
            {
                result = retrieve(key);
                _cache.Store(cacheKey, result);
                AddCrossReference(result.GetDocumentIdAsJson(), cacheKey);
            }
            return result;
        }

        public virtual IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve)
        {
            var cacheKey = _keyBuilder.GetListKey<TModel>();
            var result = _cache.Get<IList<TModel>>(cacheKey);
            if (result == null)
            {
                result = retrieve();
                _cache.Store(cacheKey, result);
                result.ForEach(x => AddCrossReference(x.GetDocumentIdAsJson(), cacheKey));
            }
            return result;
        }

        public virtual IList<TModel> GetAll<TModel>(int pageSize, int pageNumber, Func<int, int, IList<TModel>> retrieve)
        {
            var cacheKey = _keyBuilder.GetListKey<TModel>(pageNumber, pageSize);
            var result = _cache.Get<IList<TModel>>(cacheKey);
            if (result == null)
            {
                result = retrieve(pageNumber, pageSize);
                _cache.Store(cacheKey, result);
                result.ForEach(x => AddCrossReference(x.GetDocumentIdAsJson(), cacheKey));
            }
            return result;
        }

        public IList<TModel> GetAll<TModel>(object startingWith, object endingWith, Func<object,object,IList<TModel>> retrieve)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(startingWith, endingWith);
            var result = _cache.Get<IList<TModel>>(cacheKey);
            if(result == null)
            {
                result = retrieve(startingWith, endingWith);
                _cache.Store(cacheKey, result);
                result.ForEach(x => AddCrossReference(x.GetDocumentIdAsJson(), cacheKey));
            }
            return result;
        }

        public virtual void Save<TModel>(TModel model, Action<TModel> save)
        {
            InvalidateItem<TModel>(model.GetDocumentIdAsJson());
            save(model);
            CacheSavedModel(model);
        }

        protected virtual void CacheSavedModel<TModel>(TModel model)
        {
            var simpleKey = _keyBuilder.GetKey<TModel>(model.GetDocumentIdAsJson());
            var revKey = _keyBuilder.GetKey<TModel>(model.GetDocumentIdAsJson(), model.GetDocumentRevision());
            _cache.Store(simpleKey, model);
            _cache.Store(revKey, model);
        }

        public virtual void Save<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save)
        {
            list.ForEach(x => InvalidateItem<TModel>(x.GetDocumentIdAsJson()));
            save(list);
            list.ForEach(CacheSavedModel);
        }

        public CouchCacheProvider(ICacheProvider cache, IKeyAssociationManager keyAssociationManager, ICacheKeyBuilder keyBuilder)
        {
            _cache = cache;
            _keyBuilder = keyBuilder;
            _associationManager = keyAssociationManager;
        }
    }
}
