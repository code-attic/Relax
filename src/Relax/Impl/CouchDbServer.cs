using System;
using System.Collections.Generic;
using Relax.Impl.Commands;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Symbiote.Core.Extensions;


namespace Relax.Impl
{
    public class CouchDbServer : ICouchServer
    {
        protected IDocumentRepository repository { get; set; }
        protected CouchCommandFactory commandFactory { get; set; }

        public void CleanViews<TModel>()
        {
            var command = commandFactory.GetServerCommand();
            command.CleanViews<TModel>();
        }

        public void CompactDatabase<TModel>()
        {
            var command = commandFactory.GetServerCommand();
            command.CompactDatabase<TModel>();
        }

        public void CompactView<TModel>(string testview)
        {
            var command = commandFactory.GetServerCommand();
            command.CompactView<TModel>(testview);
        }

        public void CopyDatabase<TModel>(CouchUri targetUri)
        {
            var command = commandFactory.GetServerCommand();
            command.CopyDatabase<TModel>(targetUri);
        }

        public void CopyDatabase(CouchUri sourceUri, CouchUri targetUri)
        {
            var command = commandFactory.GetServerCommand();
            command.CopyDatabase(sourceUri, targetUri);
        }

        public void CreateDatabase<TModel>()
        {
            UtilityExtensions.CreateDatabase<TModel>();
        }

        public IList<string> DatabaseList
        {
            get
            {
                var command = commandFactory.GetServerCommand();
                return command.GetDatabaseList();
            }
        }

        public bool DatabaseExists<TModel>()
        {
            return UtilityExtensions.DoesDatabaseExist<TModel>();
        }

        public virtual void DeleteDatabase<TModel>()
        {
            var command = commandFactory.GetServerCommand();
            command.DeleteDatabase<TModel>();
        }

        public void Replicate<TModel>(CouchUri targetUri) 
        {
            var command = commandFactory.GetServerCommand();
            command.Replicate<TModel>(targetUri);
        }

        public void Replicate(CouchUri sourceUri, CouchUri targetUri)
        {
            var command = commandFactory.GetServerCommand();
            command.Replicate(sourceUri, targetUri);
        }

        public CouchDbServer(IDocumentRepository repository)
        {
            commandFactory = new CouchCommandFactory();
            this.repository = repository;
        }

        public IDocumentRepository Repository
        {
            get { return repository; }
        }

        public void Dispose()
        {
            repository.Dispose();   
        }
    }
}