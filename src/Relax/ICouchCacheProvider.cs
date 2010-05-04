using System;
using System.Collections.Generic;

namespace Symbiote.Relax.Impl
{
    public interface ICouchCacheProvider
    {
        void AddCrossReference(string key, string cacheKey);

        void InvalidateItem<TModel>(string affectedKey)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void Delete<TModel>(object key, Action<object> delete)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void Delete<TModel>(object key, object rev, Action<object, object> delete)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void DeleteAll<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        TModel Get<TModel>(object key, object rev, Func<object, object, TModel> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        TModel Get<TModel>(object key, Func<object, TModel> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        IList<TModel> GetAll<TModel>(Func<IList<TModel>> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        IList<TModel> GetAll<TModel>(int pageNumber, int pageSize, Func<int, int, IList<TModel>> retrieve)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void Save<TModel>(TModel model, Action<TModel> save)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void Save<TModel>(IEnumerable<TModel> list, Action<IEnumerable<TModel>> save)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
    }
}