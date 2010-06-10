using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbiote.Core;

namespace Relax.Lucene
{
    public static class RelaxLuceneAssimilation
    {
        public static IAssimilate RelaxLuceneService(this IAssimilate assimilate, Action<RelaxLuceneConfigurator> configure)
        {
            var configurator = new RelaxLuceneConfigurator();
            configure(configurator);
            var configuration = configurator.GetConfiguration();

            assimilate
                .Dependencies(x =>
                                  {
                                      x.For<IRelaxLuceneConfiguration>().Use(configuration);

                                  });

            return assimilate;
        }
    }
}
