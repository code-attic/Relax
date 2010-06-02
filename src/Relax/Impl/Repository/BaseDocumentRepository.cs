using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public abstract class BaseDocumentRepository
        : BaseCouchDbController, IDocumentRepository
    {
        protected ConcurrentDictionary<Type, IHttpAction> _continuousUpdateCommands =
            new ConcurrentDictionary<Type, IHttpAction>();
        
        public void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments
        {
            var command = _commandFactory.GetAttachmentCommand();
            var response = command.DeleteAttachment(model, attachmentName);
        }

        public virtual void DeleteDocument<TModel>(object id)
        {
            var deleteCommand = _commandFactory.GetDeleteCommand();
            var getCommand = _commandFactory.GetGetDocumentCommand();

            var doc = getCommand.GetDocument<TModel>(id);
            deleteCommand.DeleteDocument(doc);
        }

        public virtual void DeleteDocument<TModel>(object id, object rev)
        {
            var deleteCommand = _commandFactory.GetDeleteCommand();
            deleteCommand.DeleteDocument<TModel>(id,rev);
        }

        public IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query)
        {
            var command = _commandFactory.GetViewCommand();
            var response = command.GetFromView<TModel>(designDocument, viewName, query);
            return response.Result.GetList().ToList();
        }

        public virtual TModel Get<TModel>(object id, object revision)
        {
            var command = _commandFactory.GetGetDocumentCommand();
            var result = command.GetDocument<TModel>(id, revision);
            return result.Result;
        }

        public virtual TModel Get<TModel>(object id)
        {
            var command = _commandFactory.GetGetDocumentCommand();
            var result = command.GetDocument<TModel>(id);
            return result.Result;
        }

        public virtual IList<TModel> GetAll<TModel>()
        {
            var command = _commandFactory.GetGetDocumentsCommand();
            var result = command.GetDocuments<TModel>();
            return result.Result.GetList().ToList();
        }

        public virtual IList<TModel> GetAll<TModel>(int pageSize, int pageNumber)
        {
            var command = _commandFactory.GetGetDocumentsCommand();
            var result = command.GetDocuments<TModel>(pageSize, pageNumber);
            return result.Result.GetList().ToList();
        }

        public virtual IList<TModel> GetAllByKeys<TModel>(object[] ids)
        {
            var command = _commandFactory.GetGetDocumentsCommand();
            var result = command.GetDocuments<TModel>(ids);
            return result.Result.GetList().ToList();
        }

        public Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments
        {
            var command = _commandFactory.GetAttachmentCommand();
            var result = command.GetAttachment<TModel>(id, attachmentName);
            return result;
        }

        public virtual void Save<TModel>(TModel model)
        {
            var command = _commandFactory.GetSaveCommand();
            var result = command.Save(model);
        }

        public virtual void SaveAll<TModel>(IEnumerable<TModel> list)
        {
            var command = _commandFactory.GetSaveCommand();
            var result = command.Save(list);
        }

       public void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments
        {
           var command = _commandFactory.GetAttachmentCommand();
           var result = command.SaveAttachment(model, attachmentName, contentType, content);
        }

        public virtual void HandleUpdates<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback updatesInterrupted)
        {
            var command = _commandFactory.GetStreamCommand();
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
        
        protected BaseDocumentRepository(ICouchConfiguration configuration)
        {
            _configuration = configuration;
            _commandFactory = new CouchCommandFactory();
        }

        protected BaseDocumentRepository(string configurationName)
        {
            _configuration = ObjectFactory.GetNamedInstance<ICouchConfiguration>(configurationName);
            _commandFactory = new CouchCommandFactory();
        }

        public void Dispose()
        {

        }
    }
}