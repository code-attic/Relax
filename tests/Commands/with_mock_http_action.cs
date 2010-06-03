using Machine.Specifications;
using Moq;
using Relax.Impl.Http;
using StructureMap;

namespace Relax.Tests.Commands
{
    public abstract class with_mock_http_action : with_configuration
    {
        protected static Mock<IHttpAction> mockAction;
        protected static IHttpAction action { get { return mockAction.Object; }}

        private Establish context = () =>
                                        {
                                            mockAction = new Mock<IHttpAction>();
                                            ObjectFactory.Configure(x => x.For<IHttpAction>().Use(y => action));
                                        };
    }
}