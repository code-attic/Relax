using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Relax.Impl.Commands;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Model;
using StructureMap;

namespace Relax.Impl.Repository
{
    public abstract class BaseDocumentRepository : IDocumentRepository
    {
        protected ConcurrentDictionary<Type, IHttpAction> _continuousUpdateCommands =
            new ConcurrentDictionary<Type, IHttpAction>();

        protected CouchCommandFactory commandFactory { get; set; }

        public virtual void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments
        {
            var command = commandFactory.GetDeleteAttachmentCommand();
            var response = command.DeleteAttachment(model, attachmentName);
        }

        public virtual void DeleteDocument<TModel>(object id)
        {
            var deleteCommand = commandFactory.GetDeleteCommand();
            var getCommand = commandFactory.GetGetDocumentCommand();

            var doc = getCommand.GetDocument<TModel>(id);
            deleteCommand.DeleteDocument(doc);
        }

        public virtual void DeleteDocument<TModel>(object id, object rev)
        {
            var deleteCommand = commandFactory.GetDeleteCommand();
            deleteCommand.DeleteDocument<TModel>(id,rev);
        }

        public virtual IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query)
        {
            var command = commandFactory.GetViewCommand();
            var response = command.GetFromView<TModel>(designDocument, viewName, query);
            return response.GetResultAs<ViewResult<TModel>>().GetList().ToList();
        }

        public virtual TModel Get<TModel>(object id, object revision)
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocument<TModel>(id, revision);
            return result.GetResultAs<TModel>();
        }

        public virtual TModel Get<TModel>(object id)
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocument<TModel>(id);
            return result.GetResultAs<TModel>();
        }

        public virtual IList<TModel> GetAll<TModel>()
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocuments<TModel>();
            return result.GetResultAs<ViewResult<TModel>>().GetList().ToList();
        }

        public virtual IList<TModel> GetAll<TModel>(int pageSize, int pageNumber)
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocuments<TModel>(pageSize, pageNumber);
            return result.GetResultAs<ViewResult<TModel>>().GetList().ToList();
        }

        public virtual IList<TModel> GetAllByKeys<TModel>(object[] ids)
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocuments<TModel>(ids);
            return result.GetResultAs<ViewResult<TModel>>().GetList().ToList();
        }

        public virtual IList<TModel> GetAllBetweenKeys<TModel>(object startingWith, object endingWith)
        {
            var command = commandFactory.GetGetDocumentCommand();
            var result = command.GetDocuments<TModel>(startingWith, endingWith);
            return result.GetResultAs<ViewResult<TModel>>().GetList().ToList();
        }

        public virtual Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments
        {
            var command = commandFactory.GetGetAttachmentCommand();
            var result = command.GetAttachment<TModel>(id, attachmentName);
            return result;
        }

        public virtual void Save<TModel>(TModel model)
        {
            var command = commandFactory.GetSaveCommand();
            var result = command.Save(model);
        }

        public virtual void SaveAll<TModel>(IEnumerable<TModel> list)
        {
            var command = commandFactory.GetSaveCommand();
            var result = command.SaveAll(list);
        }

        public virtual void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments
        {
           var command = commandFactory.GetSaveAttachmentCommand();
           var result = command.SaveAttachment(model, attachmentName, contentType, content);
        }

        public virtual void HandleUpdates<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback updatesInterrupted)
        {
            var command = commandFactory.GetStreamCommand();
            _continuousUpdateCommands[typeof(TModel)] = command.BeginStreaming<TModel>(since, onUpdate, updatesInterrupted);
        }

        public virtual void StopChangeStreaming<TModel>()
        {
            IHttpAction command;
            var key = typeof (TModel);
            if(_continuousUpdateCommands.TryGetValue(key, out command))
            {
                command.StopContinousResponse();
                _continuousUpdateCommands.TryRemove(key, out command);
            }
        }
        
        public void Dispose()
        {

        }

        protected BaseDocumentRepository()
        {
            commandFactory = new CouchCommandFactory();
        }
    }
}