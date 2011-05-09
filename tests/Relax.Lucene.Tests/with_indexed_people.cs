using System.Threading.Tasks;
using Machine.Specifications;
using Symbiote.Lucene;

namespace Relax.Lucene.Tests
{
    public abstract class with_indexed_people : with_domain_model
    {
        protected static ILuceneSearchProvider provider;

        private Establish context = () =>
                                        {

                                            provider = luceneServiceFactory.GetSearchProviderForIndex("test");
                                            provider.IndexWriter.DeleteAll();
                                            provider.IndexWriter.ExpungeDeletes();
                                            Parallel.ForEach(personDocuments, IndexJson);
                                        };
        
        protected static void IndexJson(string x)
        {
            var observer = luceneServiceFactory.GetIndexingObserverForIndex("test");
            var visitor = new JsonVisitor();
            visitor.Subscribe(observer);
            visitor.Accept(x);
        }
    }
}