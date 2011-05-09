using System;
using Relax.Impl;
using Relax.Impl.Cache;
using Relax.Impl.Http;
using Relax.Impl.Repository;
using Relax.Impl.Serialization;
using Symbiote.Core;
using Symbiote.Core.Cache;
using Symbiote.Core.DI;

namespace Relax.Config
{
    public class CouchDependencies : IDefineStandardDependencies
    {
        public Action<DependencyConfigurator> DefineDependencies()
        {
            var configurator = new CouchConfigurator();
            var configuration = configurator.Configuration;
            return container =>
                       {
                           container.For<ICouchConfiguration>().Use( configuration );
                           container.For<IHttpAction>().Use<HttpAction>();
                           container.For<ICouchCacheProvider>().Use<CouchCacheProvider>();
                           container.For<ICacheKeyBuilder>().Use<CacheKeyBuilder>();
                           container.For<ICouchServer>().Use<CouchDbServer>();
                           container.For<ISerializationProvider>().Use<SerializationProvider>().AsSingleton();
                           container.For<IProvideDocumentMetadata>().Use<DocumentMetadataProvider>().AsSingleton();
                           container.For<IValueProviderCache>().Use<MetadataValueProviderCache>();
                           container.For<IKeyAssociationManager>().Use<DefaultKeyAssociationManager>().
                               AsSingleton();

                           if ( configuration.Cache )
                           {
                               if (!Assimilate.Assimilation.DependencyAdapter.HasPluginFor<ICacheProvider>() )
                               {
                                   throw new CouchConfigurationException(
                                       "You must have an implementation of ICacheProvider configured to use caching in Couch. Consider referencing Symbiote.Eidetic and adding the .Eidetic() call before this in your assimilation to utilize memcached or memcachedb as the cache provider for Couch."
                                       );
                               }
                               container.For<IDocumentRepository>().Use<CachedDocumentRepository>();
                           }
                           else
                           {
                               container.For<IDocumentRepository>().Use<DocumentRepository>();
                           }
                       };
        }
    }
}