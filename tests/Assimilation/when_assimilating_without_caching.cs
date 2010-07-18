using Machine.Specifications;
using Relax.Config;
using Relax.Impl;
using StructureMap;
using Symbiote.Core;
using Symbiote.StructureMap;

namespace Relax.Tests.Assimilation
{
    [Subject("Assimilation")]
    public class when_assimilating_without_caching
    {
        private Because of = () => RelaxConfiguration.Configure<StructureMapAdapter>(x => x.UseDefaults());

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
                      .HasImplementationsFor(typeof(IDocumentRepository))
                      .ShouldBeTrue();

        private It should_have_couch_server_configured =
            () => ObjectFactory
                    .Container
                    .Model
                    .HasDefaultImplementationFor<ICouchServer>();
    }
}