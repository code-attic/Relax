using Machine.Specifications;
using Relax.Config;
using Relax.Impl.Http;
using Symbiote.Core;

namespace Relax.Tests
{
    public abstract class with_configuration
    {
        protected static ICouchConfiguration configuration;
        private Establish context = () =>
                                        {
                                            Assimilate.Initialize();
                                            configuration = new CouchConfiguration();
                                        };

        protected static void WireUpCommandMock(IHttpAction commandMock)
        {
            var mock = Assimilate.GetAllInstancesOf<IHttpAction>();
            
            Assimilate.Dependencies( x => x.For<IHttpAction>().Use( commandMock ) );
        }
    }
}