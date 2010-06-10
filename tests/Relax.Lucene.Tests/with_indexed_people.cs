using Machine.Specifications;
using Symbiote.Lucene;

namespace Relax.Lucene.Tests
{
    public abstract class with_indexed_people : with_domain_model
    {
        protected static ILuceneSearchProvider provider;
        protected static JsonVisitor visitor;

        private Establish context = () =>
                                        {

                                            provider = luceneServiceFactory.GetSearchProviderForIndex("test");
                                            personDocuments.ForEach(x =>
                                                                        {
                                                                            var observer = luceneServiceFactory.GetIndexingObserverForIndex("test");
                                                                            visitor = new JsonVisitor();
                                                                            visitor.Subscribe(observer);
                                                                            visitor.Accept(x);
                                                                        });
                                        };
    }
}