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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Relax.Impl.Model;
using Symbiote.Core;
using Symbiote.Core.Collections;

namespace Relax.Impl.Serialization
{
    public class MetadataValueProviderCache : IValueProviderCache
    {
        DocumentMetadataProvider MetadataProvider { get; set; }
        public IKeyAccessor KeyAccess { get; set; }
        public Dictionary<string, Func<DocumentMetadata, object>> Readers { get; set; }
        public Dictionary<string, Action<DocumentMetadata, object>> Writers { get; set; }
        public ExclusiveConcurrentDictionary<string, MetadataValueProvider> ValueProviders { get; set; }

        public MetadataValueProvider GetValueProviderFor( string memberName )
        {
            return ValueProviders.ReadOrWrite( memberName, () => new MetadataValueProvider( 
                                                                     KeyAccess, 
                                                                     MetadataProvider, 
                                                                     Readers[ memberName ], 
                                                                     Writers[ memberName ] )
                );
        }

        public MetadataValueProviderCache( DocumentMetadataProvider metadataProvider, IKeyAccessor keyAccess )
        {
            MetadataProvider = metadataProvider;
            KeyAccess = keyAccess;
            ValueProviders = new ExclusiveConcurrentDictionary<string, MetadataValueProvider>();
            Readers = new Dictionary<string, Func<DocumentMetadata, object>>()
                          {
                              {"_id", x => x._id },
                              {"_rev", x => x._rev },
                              {"_attachments", x => x._attachments },
                          };

            Writers = new Dictionary<string, Action<DocumentMetadata, object>>()
                          {
                              {"_id", ( x, v ) => x._id = v.ToString() },
                              {"_rev", ( x, v ) => x._rev = v.ToString() },
                              {"_attachments", ( x, v ) => x._attachments = (JObject) v },
                          };
        }
    }
}