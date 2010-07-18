using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Relax;
using Relax.Lucene;
using Symbiote.Core;
using Symbiote.Daemon;
using Symbiote.Log4Net;
using Symbiote.Lucene;
using Symbiote.JsonRpc.Host;
using Symbiote.StructureMap;

namespace CouchDBIndexService
{
    class Program
    {
        static void Main(string[] args)
        {
            Assimilate
                .Core<StructureMapAdapter>()
                .Relax(x => x.UseDefaults().Preauthorize("admin", "p@ssw0rd"))
                .Lucene(x => x.UseDefaults())
                .RelaxLuceneService(x => x.UseDefaults().IndexDatabase("post"))
                .JsonRpcHost(x => x.UseDefaults().HostService<IRelaxQueryService>())
                .Dependencies(x => x.For<IRelaxQueryService>().Use<RelaxQueryService>())
                .Daemon(x => x
                    .Name("Relax.Lucene")
                    .DisplayName("Relax Lucene Service")
                    .Description("A Lucene indexing and query service for CouchDB")
                    .Arguments(args)
                    )
                .AddConsoleLogger<RelaxIndexingService>(x => x.Info().MessageLayout(p => p.TimeStamp().Message()))
                .RunDaemon();
        }
    }
}
