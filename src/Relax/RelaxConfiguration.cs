using System;
using Relax.Impl;
using StructureMap;
using Symbiote.Core.Cache;

namespace Relax
{
    public static class RelaxConfiguration
    {
        public static void Configure(Action<CouchConfigurator> configure)
        {
            var config = new CouchConfigurator();
            configure(config);
            Configure(config.GetConfiguration());
        }

        public static void Configure(ICouchConfiguration configuration)
        {
            ObjectFactory.Configure(c =>
            {
                c.For<ICouchConfiguration>().Use(configuration);
                c.For<ICouchCommand>().Use<CouchCommand>();
                c.For<ICouchCommandFactory>().Use<CouchCommandFactory>();
                c.For<ICouchCacheProvider>().Use<CouchCacheProvider>();
                c.For<ICacheKeyBuilder>().Use<CacheKeyBuilder>();
                c.For<ICouchServer>().Use<CouchDbServer>();
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
