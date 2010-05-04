using System;
using System.Collections.Generic;
using Relax.Impl;

namespace Relax
{
    public interface ICouchServer
        : IDisposable
    {
        void CleanViews<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void CreateDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void CompactDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void CompactView<TModel>(string testview)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void CopyDatabase<TModel>(CouchUri targetUri)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void CopyDatabase(CouchUri sourceUri, CouchUri targetUri);
        bool DatabaseExists<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        IList<string> DatabaseList { get; }
        void DeleteDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void Replicate<TModel>(CouchUri targetUri)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        void Replicate(CouchUri sourceUri, CouchUri targetUri);

        IDocumentRepository Repository { get; }
    }
}