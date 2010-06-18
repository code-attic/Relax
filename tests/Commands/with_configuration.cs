using Machine.Specifications;
using Relax.Config;
using Symbiote.Core;

namespace Relax.Tests.Commands
{
    public abstract class with_configuration
    {
        private Establish context = () =>
                                        {
                                            RelaxConfiguration.Configure(x => x.UseDefaults());
                                        };
    }
}