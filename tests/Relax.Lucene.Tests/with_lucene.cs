using Machine.Specifications;
using StructureMap;
using Symbiote.Core;
using Symbiote.Lucene;

namespace Relax.Lucene.Tests
{
    public abstract class with_lucene
    {
        protected static ILuceneServiceFactory luceneServiceFactory;

        private Establish context = () =>
                                        {
                                            LuceneAssimilation.Lucene(Assimilate.Core(), x => x.UseDefaults());
                                            luceneServiceFactory = ObjectFactory.GetInstance<ILuceneServiceFactory>();
                                        };
    }
}