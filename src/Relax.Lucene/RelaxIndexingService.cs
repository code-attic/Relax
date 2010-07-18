using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Symbiote.Core.Extensions;
using Symbiote.Daemon;
using Symbiote.JsonRpc.Host;
using Symbiote.Lucene;


namespace Relax.Lucene
{
    public class RelaxIndexingService
        : IDaemon
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }
        protected ILuceneServiceFactory luceneServiceFactory { get; set; }
        protected IDocumentRepository repository { get; set; }
        protected IJsonRpcHost server { get; set; }
        protected object _lock = new object();
        public bool firstRunComplete { get;set; }
        
        public void Start()
        {
            server.Start();
            configuration
                .Databases
                .ForEach(x => repository.HandleUpdates(x, 0, OnChange, null));
        }

        public void Stop()
        {
            server.Stop();
            configuration
                .Databases
                .ForEach(x => repository.StopChangeStreaming(x));
        }

        protected void OnChange(string database, ChangeRecord record)
        {
            if(!firstRunComplete)
            {
                lock(_lock)
                {
                    IndexDocument(record, database);
                    firstRunComplete = true;
                }
            }
            else
            {
                IndexDocument(record, database);
            }
        }

        protected void IndexDocument(ChangeRecord record, string database)
        {
            "Indexing document id '{0}', sequence {1}"
                .ToInfo<RelaxIndexingService>(record.Id, record.Sequence);

            var indexer = luceneServiceFactory.GetIndexingObserverForIndex(database);
            var visitor = new JsonVisitor();
            visitor.Subscribe(indexer);
            visitor.Accept(record.Document);
        }

        public RelaxIndexingService(
            IRelaxLuceneConfiguration configuration,
            IDocumentRepository respository,
            ILuceneServiceFactory serviceFactory,
            IJsonRpcHost server)
        {
            this.configuration = configuration;
            this.repository = respository;
            this.luceneServiceFactory = serviceFactory;
            this.server = server;
        }
    }
}
