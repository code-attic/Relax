using Machine.Specifications;

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