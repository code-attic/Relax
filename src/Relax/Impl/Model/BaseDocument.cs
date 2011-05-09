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
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Relax.Impl.Model
{
    public abstract class BaseDocument : IHaveAttachments
    {
        public JObject _attachments { get; set; }

        //[JsonProperty( PropertyName = "$doc_type" )]
        //internal virtual string UnderlyingDocumentType
        //{
        //    get { return GetType().Name; }
        //    set
        //    {
        //        //do nothing, this is effectively read only in the model
        //    }
        //}

        [JsonIgnore]
        public virtual IEnumerable<string> Attachments
        {
            get { return _attachments.Root.Children().Select( x => (x as JProperty).Name ); }
        }

        public virtual void AddAttachment( string attachmentName, string contentType, long contentLength )
        {
            var attachment = new
                                 {
                                     Stub = true,
                                     ContentType = contentType,
                                     ContentLength = contentLength
                                 };

            if ( !_attachments.Properties().Any( x => x.Name == attachmentName ) )
            {
                var jsonStub = new JProperty( attachmentName, JToken.FromObject( attachment ) );
                _attachments.Add( jsonStub );
            }
            else
            {
                _attachments.Property( attachmentName ).Value = JToken.FromObject( attachment );
            }
        }

        public virtual void RemoveAttachment( string attachmentName )
        {
            if ( _attachments.Properties().Any( x => x.Name == attachmentName ) )
            {
                _attachments.Remove( attachmentName );
            }
        }

        protected BaseDocument()
        {
            _attachments = JObject.FromObject( new object() );
        }
    }
}