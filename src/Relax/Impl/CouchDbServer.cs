using System;
using System.Collections.Generic;
using Relax.Impl.Commands;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Symbiote.Core.Extensions;


namespace Relax.Impl
{
    public class CouchDbServer
        : BaseCouchDbController, ICouchServer
    {
        protected IDocumentRepository _repository;

        public void CleanViews<TModel>()
        {
            var command = _commandFactory.GetServerCommand();
            command.CleanViews<TModel>();
        }

        public void CompactDatabase<TModel>()
        {
            var command = _commandFactory.GetServerCommand();
            command.CompactDatabase<TModel>();
        }

        public void CompactView<TModel>(string testview)
        {
            var command = _commandFactory.GetServerCommand();
            command.CompactView<TModel>(testview);
        }

        public void CopyDatabase<TModel>(CouchUri targetUri)
        {
            var command = _commandFactory.GetServerCommand();
            command.CopyDatabase<TModel>(targetUri);
        }

        public void CopyDatabase(CouchUri sourceUri, CouchUri targetUri)
        {
            var command = _commandFactory.GetServerCommand();
            command.CopyDatabase(sourceUri, targetUri);
        }

        public IList<string> DatabaseList
        {
            get
            {
                var command = _commandFactory.GetServerCommand();
                return command.GetDatabaseList();
            }
        }

        public virtual void DeleteDatabase<TModel>()
        {
            var command = _commandFactory.GetServerCommand();
            command.DeleteDatabase<TModel>();
        }

        public void Replicate<TModel>(CouchUri targetUri) 
        {
            var command = _commandFactory.GetServerCommand();
            command.Replicate<TModel>(targetUri);
        }

        public void Replicate(CouchUri sourceUri, CouchUri targetUri)
        {
            var command = _commandFactory.GetServerCommand();
            command.Replicate(sourceUri, targetUri);
        }

        public CouchDbServer(ICouchConfiguration configuration, IDocumentRepository repository)
        {
            _configuration = configuration;
            _commandFactory = new CouchCommandFactory();
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