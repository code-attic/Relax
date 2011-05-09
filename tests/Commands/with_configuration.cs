using Machine.Specifications;
using Symbiote.Core;

namespace Relax.Tests.Commands
{
    public abstract class with_configuration
    {
        private Establish context = () =>
                                        {
                                            Assimilate.Initialize();
                                        };
    }
}