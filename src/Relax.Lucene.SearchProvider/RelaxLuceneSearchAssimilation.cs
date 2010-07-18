using System;
using Relax.Impl;
using Symbiote.Core;
using Symbiote.JsonRpc.Client;

namespace Relax.Lucene.SearchProvider
{
    public static class RelaxLuceneSearchAssimilation
    {
        public static IAssimilate RelaxLuceneSearchProvider(this IAssimilate assimilate, string searchServerUri, TimeSpan timeout)
        {
            return assimilate
                .JsonRpcClient(x => x.Server(searchServerUri).Timeout((int)timeout.TotalMilliseconds))
                .Dependencies(x => x.For<IDocumentSearchProvider>().Use<RelaxLuceneSearchProvider>());
        }
    }
}