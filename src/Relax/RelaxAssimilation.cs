using System;
using Symbiote.Core;

namespace Relax
{
    public static class RelaxAssimilation
    {
        public static IAssimilate Relax(this IAssimilate assimilate, Action<CouchConfigurator> configure)
        {
            var config = new CouchConfigurator();
            configure(config);
            var configuration = config.GetConfiguration();
            RelaxConfiguration.Configure(configuration);
            return assimilate;
        }        
    }
}