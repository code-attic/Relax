using System;
using System.Collections.Generic;
using Relax.Impl;

namespace Relax
{
    public interface IDocumentRepository
        : IDisposable
    {
        void DeleteDocument<TModel>(object id) 
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        void DeleteDocument<TModel>(object id, object rev)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments;

        IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        TModel Get<TModel>(object id, object revision)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        TModel Get<TModel>(object id)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        IList<TModel> GetAll<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        IList<TModel> GetAll<TModel>(int pageSize, int pageNumber)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        IList<TModel> GetAllByKeys<TModel>(object[] ids)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments;
        
        void Save<TModel>(TModel model)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        void SaveAll<TModel>(IEnumerable<TModel> list)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;

        void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments;

        void HandleUpdates<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback updatesInterrupted)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
        
        void StopChangeStreaming<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision;
    }
}