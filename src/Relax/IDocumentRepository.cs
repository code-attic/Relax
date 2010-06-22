using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Relax.Impl;
using Relax.Impl.Commands;
using Relax.Impl.Model;

namespace Relax
{
    public interface IDocumentRepository : IDisposable
    {
        void DeleteDocument<TModel>(object id);

        void DeleteDocument<TModel>(object id, string rev);

        void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments;

        IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query);

        TModel Get<TModel>(object id, string revision);

        TModel Get<TModel>(object id);

        IList<TModel> GetAll<TModel>();

        IList<TModel> GetAll<TModel>(int pageSize, int pageNumber);

        IList<TModel> GetAllByKeys<TModel>(object[] ids);

        IList<TModel> GetAllBetweenKeys<TModel>(object startingWith, object endingWith);

        Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments;

        IList<TModel> GetAllByCriteria<TModel>(Expression<Func<TModel, bool>> criteria);

        void Save<TModel>(TModel model);

        void SaveAll<TModel>(IEnumerable<TModel> list);

        void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments;

        void HandleUpdates<TModel>(int since, Action<string, ChangeRecord> onUpdate, AsyncCallback updatesInterrupted);

        void HandleUpdates(string database, int since, Action<string, ChangeRecord> onUpdate, AsyncCallback updatesInterrupted);

        void StopChangeStreaming(string database);

        void StopChangeStreaming<TModel>();
    }
}