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
        protected ConcurrentDictionary<Type, ICouchCommand> _continuousUpdateCommands =
            new ConcurrentDictionary<Type, ICouchCommand>();
        
        public void DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments
        {
            var uri = BaseURI<TModel>()
                .Id(model.GetIdAsJson())
                .Attachment(attachmentName)
                .Revision(model.GetRevAsJson());

            try
            {
                var command = _commandFactory.GetCommand();
                var updatedJson = command.Delete(uri);
                var updated = updatedJson.FromJson<SaveResponse>();
                model.UpdateRevFromJson(updated.Revision.ToJson(_configuration.IncludeTypeSpecification));
                model.RemoveAttachment(attachmentName);
            }
            catch (Exception ex)
            {
                "An exception occurred trying to delete attachment {0} from document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}"
                    .ToError<IDocumentRepository>(
                        attachmentName,
                        typeof(TModel).FullName,
                        model.GetIdAsJson(),
                        model.GetRevAsJson(),
                        uri.ToString(),
                        ex);
                throw;
            }
        }

        public virtual void DeleteDocument<TModel>(object id)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var doc = Get<TModel>(id);

            if(doc != null)
            {
                var uri = BaseURI<TModel>();
                try
                {
                    var command = _commandFactory.GetCommand();
                    uri = uri.IdAndRev(doc.GetIdAsJson(), doc.GetRevAsJson());
                    command.Delete(uri);
                }
                catch (Exception ex)
                {
                    "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}"
                        .ToError<IDocumentRepository>(typeof(TModel).FullName, id.ToString(), uri.ToString(), ex);
                    throw;
                }
            }
        }

        public virtual void DeleteDocument<TModel>(object id, object rev)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>();
            try
            {
                var command = _commandFactory.GetCommand();
                uri = uri.IdAndRev(id, rev);
                command.Delete(uri);
            }
            catch (Exception ex)
            {
                "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}"
                    .ToError<IDocumentRepository>(typeof(TModel).FullName, id.ToString(), uri.ToString(), ex);
                throw;
            }
        }

        public IList<TModel> FromView<TModel>(string designDocument, string viewName, Action<ViewQuery> query) where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>()
                    .Design(designDocument)
                    .View(viewName);
            try
            {
                var viewQuery = new ViewQuery(uri);
                query(viewQuery);
                uri.IncludeDocuments();
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                json = FilterOutDesignDocuments(json);
                var view = (json.FromJson<ViewResult<TModel>>());
                return view.GetList().ToList();
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve a documents of type {0} from design document {1} and view {2} at {3}. \r\n\t {4}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        designDocument,
                        viewName,
                        uri.ToString(),
                        ex);

                if (_configuration.Throw404Exceptions)
                    throw;

                return new List<TModel>();
            }
        }

        public virtual TModel Get<TModel>(object id, object revision)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>().IdAndRev(id, revision);
            
            try
            {
                TModel model = default(TModel);
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                model = json.FromJson<TModel>();
                return model;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve a document of type {0} with id {1} and rev {2} at {3}. \r\n\t {4}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName, 
                        id.ToString(), 
                        revision.ToString(),
                        uri.ToString(),
                        ex);

                if(_configuration.Throw404Exceptions)
                    throw;
                
                return default(TModel);
            }
        }

        public virtual TModel Get<TModel>(object id)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>().Id(id);
            
            try
            {
                TModel model = default(TModel);
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                model = json.FromJson<TModel>();
                return model;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve a document of type {0} with id {1} at {2}. \r\n\t {3}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        id.ToString(),
                        uri.ToString(),
                        ex);

                if (_configuration.Throw404Exceptions)
                    throw;

                return default(TModel);
            }
        }

        public virtual IList<TModel> GetAll<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>()
                .ListAll()
                .IncludeDocuments();
            
            try
            {
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                json = FilterOutDesignDocuments(json);
                List<TModel> list = new List<TModel>();
                var view = (json.FromJson<ViewResult<TModel>>());
                list = view.GetList().ToList();
                return list;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve all documents of type {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        uri.ToString(),
                        ex);

                if (_configuration.Throw404Exceptions)
                    throw;

                return new List<TModel>();
            }
        }

        public virtual IList<TModel> GetAll<TModel>(int pageSize, int pageNumber)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>()
                .ListAll()
                .IncludeDocuments()
                .Skip((pageNumber - 1)*pageSize)
                .Limit(pageSize);
            
            try
            {
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                json = FilterOutDesignDocuments(json);
                List<TModel> list = new List<TModel>();
                var view = (json.FromJson<ViewResult<TModel>>());
                list = view.GetList().ToList();
                return list;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve all documents of type {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        uri.ToString(),
                        ex);

                if (_configuration.Throw404Exceptions)
                    throw;

                return new List<TModel>();
            }
        }

        public virtual IList<TModel> GetAllByKeys<TModel>(object[] ids) 
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>()
                .ListAll()
                .IncludeDocuments();

            try
            {
                var keys = new KeyList() {keys = ids};
                var jsonKeyList = keys.ToJson(_configuration.IncludeTypeSpecification);
                var command = _commandFactory.GetCommand();
                var json = command.Post(uri, jsonKeyList);
                List<TModel> list = new List<TModel>();
                var view = (json.FromJson<ViewResult<TModel>>());
                list = view.GetList().ToList();
                return list;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve a list of documents of type {0} by keys at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        uri.ToString(),
                        ex);

                if (_configuration.Throw404Exceptions)
                    throw;

                return new List<TModel>();
            }
        }

        public Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments
        {
            var uri = BaseURI<TModel>()
                .Id(id)
                .Attachment(attachmentName);

            try
            {
                var command = _commandFactory.GetCommand();
                return command.GetAttachment(uri);
            }
            catch (Exception ex)
            {
                "An exception occurred trying to retrieve an attachment {0} from a document of type {1} with id {2} at {3}. \r\n\t {4}"
                    .ToError<IDocumentRepository>(
                        attachmentName,
                        typeof(TModel).FullName,
                        id.ToString(),
                        uri.ToString(),
                        ex);

                throw;
            }
        }

        public virtual void Save<TModel>(TModel model)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>()
                .Id(model.GetIdAsJson());

            try
            {
                var body = model.ToJson(_configuration.IncludeTypeSpecification);
                var command = _commandFactory.GetCommand();
                var updatedJSON = command.Put(uri, body);
                var updated = updatedJSON.FromJson<SaveResponse>();
                model.UpdateRevFromJson(updated.Revision.ToJson(_configuration.IncludeTypeSpecification));
            }
            catch (Exception ex)
            {
                "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        uri.ToString(),
                        ex);
                throw;
            }
        }

        public virtual void SaveAll<TModel>(IEnumerable<TModel> list)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var uri = BaseURI<TModel>().BulkInsert();
            
            try
            {
                var documentList = new BulkPersist(true, false, list);
                var command = _commandFactory.GetCommand();
                var body = documentList.ToJson(_configuration.IncludeTypeSpecification);

                // remote type tokens...
                body = ScrubBulkPersistOfTypeTokens(body);

                var updatedJson = command.Post(uri, body);
                var updated = updatedJson.FromJson<SaveResponse[]>();
                list
                    .ToList()
                    .ForEach(x =>
                                 {
                                     var update = updated.FirstOrDefault(y => y.Id.ToJson() == x.GetIdAsJson());
                                     if (update != null)
                                         x.UpdateRevFromJson(update.Revision);
                                 });
            }
            catch (Exception ex)
            {
                "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(
                        typeof(TModel).FullName,
                        uri.ToString(),
                        ex);
                throw;
            }
        }

        protected string ScrubBulkPersistOfTypeTokens(string body)
        {
            var jBlob = JObject.Parse(body);

            var hasTypes = jBlob.Children().OfType<JProperty>().FirstOrDefault(x => x.Name == "$type") != null;
            if (hasTypes)
            {
                var allOrNothing = jBlob["all_or_nothing"];
                var nonAtomic = jBlob["non_atomic"];
                var docs = jBlob["docs"]["$values"];

                var newBlob = new JObject(
                    new JProperty("all_or_nothing", allOrNothing),
                    new JProperty("non_atomic", nonAtomic),
                    new JProperty("docs", docs)
                    );

                body = newBlob.ToString();
            }
            return body;
        }

        public void SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision, IHaveAttachments
        {
            var uri = BaseURI<TModel>()
                .Id(model.GetIdAsJson())
                .Attachment(attachmentName)
                .Revision(model.GetRevAsJson());

            try
            {
                var command = _commandFactory.GetCommand();
                var updatedJson = command.SaveAttachment(uri, contentType, content);
                var updated = updatedJson.FromJson<SaveResponse>();
                model.UpdateRevFromJson(updated.Revision.ToJson(_configuration.IncludeTypeSpecification));
                model.AddAttachment(attachmentName, contentType, content.Length);
            }
            catch (Exception ex)
            {
                "An exception occurred trying to save an attachment {0} to a document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}"
                    .ToError<IDocumentRepository>(
                        attachmentName,
                        typeof(TModel).FullName,
                        model.GetIdAsJson(),
                        model.GetRevAsJson(),
                        uri.ToString(),
                        ex);

                throw;
            }
        }

        public virtual void HandleUpdates<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback updatesInterrupted)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var command = _commandFactory.GetCommand();
            Action<CouchUri, int, Action<ChangeRecord>> proxy = command.GetContinuousResponse;
            proxy.BeginInvoke(BaseURI<TModel>(), since, onUpdate, updatesInterrupted, null);
            _continuousUpdateCommands[typeof(TModel)] = command;
        }

        public virtual void StopChangeStreaming<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            ICouchCommand command;
            var key = typeof (TModel);
            if(_continuousUpdateCommands.TryGetValue(key, out command))
            {
                command.StopContinousResponse();
                _continuousUpdateCommands.TryRemove(key, out command);
            }
        }

        protected virtual string FilterOutDesignDocuments(string json)
        {
            var jsonDoc = JObject.Parse(json);
            jsonDoc["rows"]
                .Children()
                .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design"))
                .ForEach(x => x.Remove());
            return jsonDoc.ToString();
        }

        protected BaseDocumentRepository(ICouchConfiguration configuration, ICouchCommandFactory commandFactory)
        {
            _configuration = configuration;
            _commandFactory = commandFactory;
        }

        protected BaseDocumentRepository(string configurationName)
        {
            _configuration = ObjectFactory.GetNamedInstance<ICouchConfiguration>(configurationName);
            _commandFactory = ObjectFactory.GetInstance<ICouchCommandFactory>();
        }

        public void Dispose()
        {

        }
    }

    public class KeyList
    {
        public object[] keys { get; set; }
    }
}