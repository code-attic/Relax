using System;
using System.Collections.Generic;
using Relax.Impl;
using Relax.Impl.Commands;
using Relax.Impl.Model;

namespace Relax
{
    public interface IDocumentRepository : IDisposable
    {
        void DeleteDocument<TModel>(object id);

        void DeleteDocument<TModel>(object id, object rev);

        void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments;

        IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query);

        TModel Get<TModel>(object id, object revision);

        TModel Get<TModel>(object id);

        IList<TModel> GetAll<TModel>();

        IList<TModel> GetAll<TModel>(int pageSize, int pageNumber);

        IList<TModel> GetAllByKeys<TModel>(object[] ids);

        IList<TModel> GetAllBetweenKeys<TModel>(object startingWith, object endingWith);

        Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments;

        void Save<TModel>(TModel model);

        void SaveAll<TModel>(IEnumerable<TModel> list);

        void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments;

        void HandleUpdates<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback updatesInterrupted);

        void StopChangeStreaming<TModel>();
    }
}