using Machine.Specifications;
using Relax.Impl.Commands;

namespace Relax.Tests.Commands
{
    public abstract class with_command_factory : with_mock_http_action
    {
        protected static CouchCommandFactory factory;

        private Establish context = () =>
                                        {
                                            factory = new CouchCommandFactory();
                                        };
    }
}