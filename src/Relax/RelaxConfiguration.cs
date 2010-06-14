using System;
using Relax.Config;
using Relax.Impl;
using Relax.Impl.Cache;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Relax.Impl.Serialization;
using StructureMap;
using Symbiote.Core;
using Symbiote.Core.Cache;
using Symbiote.Restfully;
using Symbiote.Restfully.Impl.Rpc;

namespace Relax
{
    public class RelaxConfiguration
    {
        public static void Configure(Action<CouchConfigurator> configure)
        {
            var config = new CouchConfigurator();
            configure(config);
            Configure(config.GetConfiguration());
        }

        public static void Configure(ICouchConfiguration configuration)
        {
            Assimilate
                .Core()
                .HttpServiceClient(x => x.Server(configuration.RelaxQueryServiceUrl).Timeout(10000))
                .Dependencies(x => x.For(typeof (IRemoteProxy<>)).Use(typeof (RemoteProxy<>)));
            ObjectFactory.Configure(c =>
            {
                c.For<ICouchConfiguration>().Use(configuration);
                c.For<IHttpAction>().Use<HttpAction>();
                c.For<ICouchCacheProvider>().Use<CouchCacheProvider>();
                c.For<ICacheKeyBuilder>().Use<CacheKeyBuilder>();
                c.For<ICouchServer>().Use<CouchDbServer>();
                c.For<DocumentConventions>().Use(configuration.Conventions);
                if (configuration.Cache)
                {
                    if (!ObjectFactory.Container.Model.HasDefaultImplementationFor<ICacheProvider>())
                    {
                        throw new RelaxConfigurationException(
                            "You must have an implementation of ICacheProvider configured to use caching in Relax. Consider referencing Symbiote.Eidetic and adding the .Eidetic() call before this in your assimilation to utilize memcached or memcachedb as the cache provider for Relax."
                        );
                    }

                    c.For<IDocumentRepository>().Use<CachedDocumentRepository>();
                }
                else
                {
                    c.For<IDocumentRepository>().Use<DocumentRepository>();
                }
            });
        }
    }
}
