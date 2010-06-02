using System;
using System.Collections.Generic;
using Relax.Impl;
using Relax.Impl.Http;

namespace Relax
{
    public interface ICouchServer
        : IDisposable
    {
        void CleanViews<TModel>();
        void CreateDatabase<TModel>();
        void CompactDatabase<TModel>();
        void CompactView<TModel>(string testview);
        void CopyDatabase<TModel>(CouchUri targetUri);
        void CopyDatabase(CouchUri sourceUri, CouchUri targetUri);
        bool DatabaseExists<TModel>();
        IList<string> DatabaseList { get; }
        void DeleteDatabase<TModel>();
        void Replicate<TModel>(CouchUri targetUri);
        void Replicate(CouchUri sourceUri, CouchUri targetUri);

        IDocumentRepository Repository { get; }
    }
}