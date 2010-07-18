using Machine.Specifications;
using Moq;
using Relax.Config;
using Relax.Impl;
using StructureMap;
using Symbiote.Core.Cache;
using Symbiote.StructureMap;
using It = Machine.Specifications.It;

namespace Relax.Tests.Assimilation
{

    [Subject("Assimilation")]
    public class when_assimilating_with_caching
    {
        private Because of = () => 
        {
            var rememberMock = new Mock<ICacheProvider>().Object;
            ObjectFactory.Configure(x => x.For<ICacheProvider>().Use(rememberMock));
            RelaxConfiguration.Configure<StructureMapAdapter>(x => x.UseDefaults().Cache());
        };

        private It should_use_CouchConfiguration_for_ICouchConfiguration =
            () => ObjectFactory
                    .Container
                    .Model
                    .DefaultTypeFor<ICouchConfiguration>()
                    .ShouldEqual(typeof(CouchConfiguration));

        private It should_use_DocumentRepository_for_IDocumentRepository =
            () => ObjectFactory
                      .Container
                      .Model
                      .HasImplementationsFor(typeof (IDocumentRepository))
                      .ShouldBeTrue();

        private It should_have_couch_server_configured = 
            () => ObjectFactory
                    .Container
                    .Model
                    .HasDefaultImplementationFor<ICouchServer>();
    }
}
