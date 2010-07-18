using System;
using System.Configuration;
using Relax;
using Symbiote.Core;
using Symbiote.Daemon;
using Symbiote.JsonRpc.Client;
using Symbiote.JsonRpc.Client.Impl.Rpc;
using Symbiote.Log4Net;
using Symbiote.StructureMap;
using Relax.Lucene.SearchProvider;

namespace RelaxDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Assimilate
                .Core<StructureMapAdapter>()
                .Relax(x => x.UseDefaults().Server(ConfigurationManager.AppSettings["couchdb"]))
                .RelaxLuceneSearchProvider(@"http://localhost:8420/", TimeSpan.FromSeconds(10))
                .AddColorConsoleLogger<ChangeWatcher>(x => x
                                                               .Info()
                                                               .DefineColor()
                                                               .Text.IsGreen().ForAllOutput()
                                                               .MessageLayout(m => m.Message().Date().Newline())
                )
                .AddConsoleLogger<RelaxDemoService>(x => x
                                                             .Info()
                                                             .MessageLayout(m => m.Message().Newline())
                )
                .Dependencies(x => x.For(typeof(IRemoteProxy<>)).Use(typeof(RemoteProxy<>)))
                .Daemon(x => x
                           .Arguments(args)
                           .Name("relaxdemo")
                           .DisplayName("Relax Demo")
                           .Description("Relax Integration Testing")
                )
                .RunDaemon();
        }
    }
}
