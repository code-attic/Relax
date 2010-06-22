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

        public virtual void InvalidateItem(string affectedKey)
        {
            _associationManager
                .GetAssociations(affectedKey)
                .ForEach(x => _cache.Remove(x));
        }

        public virtual void Delete<TModel>(object key, Action<object> delete)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key);
            InvalidateItem(cacheKey);
            delete(key);
        }

        public virtual void Delete<TModel>(object key, string rev, Action<object, string> delete)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            InvalidateItem(cacheKey);
            delete(key, rev);
        }
        
        public virtual TModel Get<TModel>(object key, string rev, Func<object, string, TModel> retrieve)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(key, rev);
            var result = _cache.Get<TModel>(cacheKey);
            if (result == null)
            {
                result = retrieve(key, rev);
                _cache.Store(cacheKey, result);
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
            }
            return result;
        }

        public virtual IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve)
        {
            var listCacheKey = _keyBuilder.GetListKey<TModel>();
            var result = _cache.Get<IList<TModel>>(listCacheKey);
            if (result == null)
            {
                result = retrieve();
                _cache.Store(listCacheKey, result);
                result.ForEach(x =>
                                   {
                                       var cacheKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId());
                                       var cacheRevKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId(),
                                                                                    x.GetDocumentRevision());
                                       AddCrossReference(cacheKey, listCacheKey);
                                       AddCrossReference(cacheRevKey, listCacheKey);
                                   });
            }
            return result;
        }

        public virtual IList<TModel> GetAll<TModel>(int pageSize, int pageNumber, Func<int, int, IList<TModel>> retrieve)
        {
            var listCacheKey = _keyBuilder.GetListKey<TModel>(pageNumber, pageSize);
            var result = _cache.Get<IList<TModel>>(listCacheKey);
            if (result == null)
            {
                result = retrieve(pageNumber, pageSize);
                _cache.Store(listCacheKey, result);
                result.ForEach(x =>
                {
                    var cacheKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId());
                    var cacheRevKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId(),
                                                                 x.GetDocumentRevision());
                    AddCrossReference(cacheKey, listCacheKey);
                    AddCrossReference(cacheRevKey, listCacheKey);
                });
            }
            return result;
        }

        public IList<TModel> GetAll<TModel>(object startingWith, object endingWith, Func<object,object,IList<TModel>> retrieve)
        {
            var listCacheKey = _keyBuilder.GetRangeKey<TModel>(startingWith, endingWith);
            var result = _cache.Get<IList<TModel>>(listCacheKey);
            if(result == null)
            {
                result = retrieve(startingWith, endingWith);
                _cache.Store(listCacheKey, result);
                result.ForEach(x =>
                {
                    var cacheKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId());
                    var cacheRevKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId(),
                                                                 x.GetDocumentRevision());
                    AddCrossReference(cacheKey, listCacheKey);
                    AddCrossReference(cacheRevKey, listCacheKey);
                });
            }
            return result;
        }

        public virtual void Save<TModel>(TModel model, Action<TModel> save)
        {
            var cacheKey = _keyBuilder.GetKey<TModel>(model.GetDocumentId());
            var cacheRevKey = _keyBuilder.GetKey<TModel>(model.GetDocumentId(), model.GetDocumentRevision());
            InvalidateItem(cacheKey);
            InvalidateItem(cacheRevKey);
            save(model);
            CacheSavedModel(model);
        }

        protected virtual void CacheSavedModel<TModel>(TModel model)
        {
            var simpleKey = _keyBuilder.GetKey<TModel>(model.GetDocumentId());
            var revKey = _keyBuilder.GetKey<TModel>(model.GetDocumentId(), model.GetDocumentRevision());
            _cache.Store(simpleKey, model);
            _cache.Store(revKey, model);
        }

        public virtual void Save<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save)
        {
            list.ForEach(x =>
                             {
                                 var cacheKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId());
                                 var cacheRevKey = _keyBuilder.GetKey<TModel>(x.GetDocumentId(), x.GetDocumentRevision());
                                 InvalidateItem(cacheKey);
                                 InvalidateItem(cacheRevKey);
                             });
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
