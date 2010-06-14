using System;
using System.Linq;
using Symbiote.Lucene;

namespace Relax.Lucene
{
    public class RelaxQueryService : IRelaxQueryService
    {
        protected IRelaxLuceneConfiguration configuration { get; set; }
        protected ILuceneServiceFactory luceneServiceFactory { get; set; }
        protected IDocumentRepository repository { get; set; }

        public string[] GetDocumentIdsForQuery(string database, string luceneQuery)
        {
            var searchProvider = luceneServiceFactory.GetSearchProviderForIndex(database);
            var documents = searchProvider.GetDocumentsForQuery(luceneQuery).ToList();
            var ids = documents.Select(x => x.Item2.Get("_id").ToString());
            return ids.ToArray();
        }

        public RelaxQueryService(
            IRelaxLuceneConfiguration configuration,
            IDocumentRepository respository,
            ILuceneServiceFactory serviceFactory)
        {
            this.configuration = configuration;
            this.repository = respository;
            this.luceneServiceFactory = serviceFactory;
        }
    }
}