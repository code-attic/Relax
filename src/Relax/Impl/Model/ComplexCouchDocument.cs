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
using Symbiote.Core.Extensions;
using Symbiote.Core.Serialization;
using Newtonsoft.Json;

namespace Relax.Impl.Model
{
    [Serializable]
    [JsonObject( MemberSerialization.OptOut )]
    public abstract class ComplexCouchDocument<TModel, TKey> : BaseDocument, ICouchDocument<TKey>
        where TModel : ComplexCouchDocument<TModel, TKey>
    {
        protected Func<TModel, TKey> DocumentIdGetter = x => x._documentId;
        protected Action<TModel, TKey> DocumentIdSetter = ( x, k ) => x._documentId = k;
        protected TKey _documentId;

        [JsonProperty( PropertyName = "_id" )]
        public virtual TKey DocumentId
        {
            get { return DocumentIdGetter( this as TModel ); }
            set { DocumentIdSetter( this as TModel, value ); }
        }

        [JsonProperty( PropertyName = "_rev" )]
        public virtual string DocumentRevision { get; set; }

        public virtual object GetDocumentId()
        {
            return DocumentId;
        }

        public virtual string GetDocumentIdAsJson()
        {
            var typeCode = typeof( TKey );
            if ( typeCode.IsValueType && typeCode.Namespace.StartsWith( "System" ) )
            {
                return DocumentId.ToString();
            }
            else
            {
                return DocumentId.ToJson( false );
            }
        }

        public virtual void UpdateRevFromJson( string jsonRev )
        {
            DocumentRevision = jsonRev;
        }

        protected virtual TModel KeyGetter( Func<TModel, TKey> getter )
        {
            DocumentIdGetter = getter;
            return this as TModel;
        }

        protected virtual TModel KeySetter( Action<TModel, TKey> setter )
        {
            DocumentIdSetter = setter;
            return this as TModel;
        }

        public virtual void UpdateKeyFromJson( string jsonKey )
        {
            var documentId = jsonKey.FromJson<TKey>();
            DocumentId = object.Equals( documentId, default(TKey) )
                             ? "\"{0}\"".AsFormat( jsonKey ).FromJson<TKey>()
                             : documentId;
        }
    }
}