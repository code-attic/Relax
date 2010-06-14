using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Relax;
using Symbiote.Core;
using Symbiote.Daemon;
using Symbiote.Log4Net;
using Symbiote.Eidetic;
using Symbiote.Restfully;

namespace RelaxDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Assimilate
                .Core()
                .Daemon(x => x
                           .Arguments(args)
                           .Name("relaxdemo")
                           .DisplayName("Relax Demo")
                           .Description("Relax Integration Testing")
                )
                .Relax(x => x.UseDefaults().Server(ConfigurationManager.AppSettings["couchdb"]))
                //.HttpClient(x => x.Server(@"http://localhost:8420/").Timeout(80000))
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
                //.Dependencies(x => x.For(typeof(IRemoteProxy<>)).Use(typeof(RemoteProxy<>)))
                .RunDaemon();
        }
    }
}
