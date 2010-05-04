using System;
using System.Collections.Generic;
using Symbiote.Core.Extensions;

namespace Symbiote.Relax.Impl
{
    public class CouchDbServer
        : BaseCouchDbController, ICouchServer
    {
        protected IDocumentRepository _repository;

        public void CleanViews<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            try
            {
                var uri = BaseURI<TModel>().CleanupViews();
                var command = _commandFactory.GetCommand();
                command.Post(uri);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CompactDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            try
            {
                var uri = BaseURI<TModel>().Compact();
                var command = _commandFactory.GetCommand();
                command.Post(uri);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CompactView<TModel>(string testview)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            try
            {
                var uri = BaseURI<TModel>().CompactView(testview);
                var command = _commandFactory.GetCommand();
                command.Post(uri);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CopyDatabase<TModel>(CouchUri targetUri)
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            try
            {
                var serverUri = BaseURI().Replicate();
                var sourceUri = BaseURI<TModel>();
                var request = ReplicationCommand.Once(sourceUri, targetUri);
                var command = _commandFactory.GetCommand();
                command.Post(serverUri, request.ToJson(false));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CopyDatabase(CouchUri sourceUri, CouchUri targetUri)
        {
            try
            {
                var serverUri = BaseURI().Replicate();
                var request = ReplicationCommand.Once(sourceUri, targetUri);
                var command = _commandFactory.GetCommand();
                command.Post(serverUri, request.ToJson(false));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<string> DatabaseList
        {
            get
            {
                var uri = CouchUri.Build(
                    _configuration.Protocol,
                    _configuration.Server,
                    _configuration.Port,
                    "_all_dbs");
                var command = _commandFactory.GetCommand();
                var json = command.Get(uri);
                return new List<string>(json.FromJson<string[]>());
            }
        }

        public virtual void DeleteDatabase<TModel>()
            where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            var database = "";
            var uri = BaseURI<TModel>();
            try
            {
                database = _configuration.GetDatabaseNameForType<TModel>();
                var command = _commandFactory.GetCommand();
                command.Delete(uri);
                _databaseExists[database] = false;
            }
            catch (Exception ex)
            {
                "An exception occurred trying to delete the database {0} at {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>(database, uri.ToString(), ex);
                throw;
            }
        }

        public void Replicate<TModel>(CouchUri targetUri) where TModel : class, IHandleJsonDocumentId, IHandleJsonDocumentRevision
        {
            try
            {
                var serverUri = BaseURI().Replicate();
                var sourceUri = BaseURI<TModel>();
                var request = ReplicationCommand.Continuous(sourceUri, targetUri);
                var command = _commandFactory.GetCommand();
                command.Post(serverUri, request.ToJson(false));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Replicate(CouchUri sourceUri, CouchUri targetUri)
        {
            try
            {
                var serverUri = BaseURI().Replicate();
                var request = ReplicationCommand.Continuous(sourceUri, targetUri);
                var command = _commandFactory.GetCommand();
                command.Post(serverUri, request.ToJson(false));
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public CouchDbServer(ICouchConfiguration configuration, ICouchCommandFactory commandFactory, IDocumentRepository repository)
        {
            _configuration = configuration;
            _commandFactory = commandFactory;
            _repository = repository;
        }

        public IDocumentRepository Repository
        {
            get { return _repository; }
        }

        public void Dispose()
        {
            _repository.Dispose();   
        }
    }
}