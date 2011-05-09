using Machine.Specifications;
using StructureMap;
using Symbiote.Core;
using Symbiote.Lucene;
using Symbiote.StructureMap;

namespace Relax.Lucene.Tests
{
    public abstract class with_lucene
    {
        protected static ILuceneServiceFactory luceneServiceFactory;

        private Establish context = () =>
                                        {
                                            Assimilate.Core<StructureMapAdapter>().Relax().Lucene();
                                            luceneServiceFactory = ObjectFactory.GetInstance<ILuceneServiceFactory>();
                                        };
    }
}