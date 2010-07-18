using Machine.Specifications;
using Relax.Config;
using Relax.Impl;
using Relax.Impl.Http;
using StructureMap;
using Symbiote.Core;
using Symbiote.StructureMap;

namespace Relax.Tests
{
    public abstract class with_configuration
    {
        protected static ICouchConfiguration configuration;
        private Establish context = () =>
                                        {
                                            Assimilate.Core<StructureMapAdapter>().Relax(x => x.UseDefaults());
                                            configuration = new CouchConfiguration();
                                        };
        protected static void WireUpCommandMock(IHttpAction commandMock)
        {
            ObjectFactory.Configure(x => x.For<IHttpAction>().Use(commandMock));
        }
    }
}