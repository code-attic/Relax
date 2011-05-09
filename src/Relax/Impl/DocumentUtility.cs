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
using Relax.Config;
using Relax.Impl.Model;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Impl
{
    public class DocumentUtility
    {
        protected ICouchConfiguration configuration { get; set; }
        protected IKeyAccessor KeyAccessor { get; set; }
        protected IProvideDocumentMetadata MetadataProvider { get; set; }

        public virtual string GetDocumentIdAsJson( object instance )
        {
            var doc = instance as IHaveDocumentId;
            if ( doc != null )
            {
                return doc.GetDocumentIdAsJson();
            }
            else
            {
                return KeyAccessor.GetId( instance, instance.GetType() );
            }
        }

        public virtual object GetDocumentId( object instance )
        {
            var doc = instance as IHaveDocumentId;
            if ( doc != null )
            {
                return doc.GetDocumentId();
            }
            else
            {
                return KeyAccessor.GetId( instance, instance.GetType() );
            }
        }

        public virtual string GetDocumentRevision( object instance )
        {
            var doc = instance as IHaveDocumentRevision;
            if ( doc != null )
            {
                return doc.DocumentRevision;
            }
            else
            {
                var key = KeyAccessor.GetId( instance, instance.GetType() );
                var metadata = MetadataProvider.GetMetadata( key );
                return metadata == null ? null : metadata._rev;
            }
        }

        public virtual void SetDocumentRevision( string revision, object instance )
        {
            var doc = instance as IHaveDocumentRevision;
            if ( doc != null )
            {
                doc.UpdateRevFromJson( revision );
            }
            else
            {
                var key = KeyAccessor.GetId( instance, instance.GetType() );
                var metadata = MetadataProvider.GetMetadata( key ) ??
                               new DocumentMetadata() { _id = key, _rev = revision };
                MetadataProvider.SetMetadata( key, metadata );
            }
        }

        public virtual bool IsDocument( object instance )
        {
            return instance.GetType().GetInterface( "ICouchDocument`1" ) != null;
        }

        public DocumentUtility( ICouchConfiguration couchConfiguration, IKeyAccessor keyAccessor, IProvideDocumentMetadata metadataProvider )
        {
            configuration = couchConfiguration;
            KeyAccessor = keyAccessor;
            MetadataProvider = metadataProvider;
        }
    }
}