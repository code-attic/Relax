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
using Newtonsoft.Json.Serialization;
using Relax.Impl.Model;
using Symbiote.Core;

namespace Relax.Impl.Serialization
{
    public class MetadataValueProvider : IValueProvider
    {
        public IKeyAccessor KeyAccess { get; set; }
        public DocumentMetadataProvider MetadataProvider { get; set; }
        public Func<DocumentMetadata, object> Getter { get; set; }
        public Action<DocumentMetadata, object> Setter { get; set; }

        public void SetValue( object target, object value )
        {
            var type = target.GetType();
            if ( !KeyAccess.HasAccessFor( type ) ) 
                return;
            
            var key = KeyAccess.GetId( target, type );
            var metadata = MetadataProvider.GetMetadata( key );
            Setter( metadata, value );
        }

        public object GetValue( object target )
        {
            var type = target.GetType();
            object value = null;
            if( KeyAccess.HasAccessFor( type ) )
            {
                var key = KeyAccess.GetId( target, type );
                var metadata = MetadataProvider.GetMetadata( key );
                value = Getter( metadata );
            }
            return value;
        }

        public MetadataValueProvider( 
            IKeyAccessor keyAccess, 
            DocumentMetadataProvider metadataProvider, 
            Func<DocumentMetadata, object> getter, 
            Action<DocumentMetadata, object> setter )
        {
            KeyAccess = keyAccess;
            MetadataProvider = metadataProvider;
            Getter = getter;
            Setter = setter;
        }
    }
}