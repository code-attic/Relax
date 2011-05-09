using Machine.Specifications;
using Moq;
using Relax.Config;
using Symbiote.Core;
using Symbiote.Core.Cache;
using It = Machine.Specifications.It;

namespace Relax.Tests.Assimilation
{

    [Subject("Assimilation")]
    public class when_assimilating_with_caching
    {
        private Because of = () =>
        {
            Assimilate.Initialize();
            var rememberMock = new Mock<ICacheProvider>().Object;
            Assimilate.Dependencies( x => x.For<ICacheProvider>().Use( rememberMock ) );
            CouchInit.Configure(x => x.Cache());
        };

        private It should_use_CouchConfiguration_for_ICouchConfiguration =
            () => Assimilate
                    .Assimilation
                    .DependencyAdapter
                    .GetDefaultTypeFor<ICouchConfiguration>()
                    .ShouldEqual(typeof(CouchConfiguration));

        private It should_use_DocumentRepository_for_IDocumentRepository =
            () => Assimilate
                    .Assimilation
                    .DependencyAdapter
                    .HasPluginFor(typeof (IDocumentRepository))
                    .ShouldBeTrue();

        private It should_have_couch_server_configured =
            () => Assimilate
                    .Assimilation
                    .DependencyAdapter
                    .HasPluginFor<ICouchServer>()
                    .ShouldBeTrue();
    }
}
