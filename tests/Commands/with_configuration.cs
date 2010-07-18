using Machine.Specifications;
using Relax.Config;
using Symbiote.Core;
using Symbiote.StructureMap;

namespace Relax.Tests.Commands
{
    public abstract class with_configuration
    {
        private Establish context = () =>
                                        {
                                            RelaxConfiguration.Configure<StructureMapAdapter>(x => x.UseDefaults());
                                        };
    }
}