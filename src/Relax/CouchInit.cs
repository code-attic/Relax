// /* 
// Copyright 2008-2011 Alex Robson
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// */
using System;
using Relax.Config;
using Relax.Impl.Repository;
using Symbiote.Core;
using Symbiote.Core.Cache;

namespace Relax
{
    public class CouchInit
    {
        public static void Configure( Action<CouchConfigurator> configure )
        {
            Assimilate.Initialize();
            var config = new CouchConfigurator();
            configure( config );
            Configure( config.Configuration );
        }

        
        public static void Configure( ICouchConfiguration configuration )
        {
            Assimilate.Dependencies( c =>
                                         {
                                             if ( configuration.Cache )
                                             {
                                                 if (
                                                     !Assimilate.Assimilation.DependencyAdapter.HasPluginFor
                                                          <ICacheProvider>() )
                                                 {
                                                     throw new CouchConfigurationException(
                                                         "You must have an implementation of ICacheProvider configured to use caching in Couch. Consider referencing Symbiote.Eidetic and adding the .Eidetic() call before this in your assimilation to utilize memcached or memcachedb as the cache provider for Couch."
                                                         );
                                                 }

                                                 c.For<IDocumentRepository>().Use<CachedDocumentRepository>();
                                             }
                                             else
                                             {
                                                 c.For<IDocumentRepository>().Use<DocumentRepository>();
                                             }
                                         } );
        }
    }
}