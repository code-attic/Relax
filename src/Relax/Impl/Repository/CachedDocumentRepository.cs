using System.Collections.Generic;
using Relax.Impl.Cache;
using Relax.Impl.Configuration;

namespace Relax.Impl.Repository
{
    public class CachedDocumentRepository
        : BaseDocumentRepository
    {
        protected ICouchCacheProvider _cache;
        protected IDocumentRepository _repository;
        protected ICacheKeyBuilder _builder;

        public CachedDocumentRepository(ICouchCacheProvider cacheProvider)
        {
            _cache = cacheProvider;
        }

        public override void DeleteDocument<TModel>(object id, object rev)
        {
            _cache.Delete<TModel>(id, rev, base.DeleteDocument<TModel>);
        }

        public override void DeleteDocument<TModel>(object id)
        {
            _cache.Delete<TModel>(id, base.DeleteDocument<TModel>);
        }

        public override TModel Get<TModel>(object id, object revision)
        {
            return _cache.Get(id, revision, base.Get<TModel>);
        }

        public override TModel Get<TModel>(object id)
        {
            return _cache.Get(id, base.Get<TModel>);
        }

        public override IList<TModel> GetAll<TModel>()
        {
            return _cache.GetAll(base.GetAll<TModel>);
        }

        public override IList<TModel> GetAll<TModel>(int pageSize, int pageNumber)
        {
            return _cache.GetAll(pageNumber, pageSize, base.GetAll<TModel>);
        }

        public override void Save<TModel>(TModel model)
        {
            _cache.Save(model, base.Save);
        }

        public override void SaveAll<TModel>(IEnumerable<TModel> list)
        {
            _cache.Save(list, base.SaveAll);
        }
    }
}