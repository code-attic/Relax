using System;
using Relax.Config;
using Relax.Impl;
using Relax.Impl.Cache;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Relax.Impl.Serialization;
using Symbiote.Core;
using Symbiote.Core.Cache;
using Symbiote.Core.DI;

namespace Relax
{
    public class RelaxConfiguration
    {
        public static void Configure<TDepedencyAdapter>(Action<CouchConfigurator> configure)
            where TDepedencyAdapter : class, IDependencyAdapter, new()
        {
            Assimilate.Core<TDepedencyAdapter>();
            var config = new CouchConfigurator();
            configure(config);
            Configure(config.GetConfiguration());
        }

        public static void Configure(IDependencyAdapter dependencyAdapter, Action<CouchConfigurator> configure)
        {
            Assimilate.Core(dependencyAdapter);
            var config = new CouchConfigurator();
            configure(config);
            Configure(config.GetConfiguration());
        }

        public static void Configure(ICouchConfiguration configuration)
        {
            Assimilate.Dependencies(c =>
            {
                c.For<ICouchConfiguration>().Use(configuration);
                c.For<IHttpAction>().Use<HttpAction>();
                c.For<ICouchCacheProvider>().Use<CouchCacheProvider>();
                c.For<ICacheKeyBuilder>().Use<CacheKeyBuilder>();
                c.For<ICouchServer>().Use<CouchDbServer>();
                c.For<IKeyAssociationManager>().Use<DefaultKeyAssociationManager>().AsSingleton();
                c.For<DocumentConventions>().Use(configuration.Conventions);
                if (configuration.Cache)
                {
                    if (!Assimilate.Assimilation.DependencyAdapter.HasPluginFor<ICacheProvider>())
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
