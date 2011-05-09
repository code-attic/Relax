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
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Serialization;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class BaseCouchCommand
    {
        public virtual CouchUri Uri { get; protected set; }
        protected virtual IHttpAction action { get; set; }
        protected ICouchConfiguration configuration { get; set; }
        protected ISerializationProvider serializer { get; set; }

        public virtual CommandResult Get()
        {
            return new CommandResult( action.Get( Uri ) );
        }

        public virtual CommandResult Delete()
        {
            return new CommandResult( action.Delete( Uri ) );
        }

        public virtual CommandResult Post()
        {
            return new CommandResult( action.Post( Uri ) );
        }

        public virtual CommandResult Post( string body )
        {
            return new CommandResult( action.Post( Uri, body ) );
        }

        public virtual CommandResult Put()
        {
            return new CommandResult( action.Put( Uri ) );
        }

        public virtual CommandResult Put( string body )
        {
            return new CommandResult( action.Put( Uri, body ) );
        }

        public virtual CouchUri CreateUri( string database )
        {
            Uri = configuration.NewUri( database );
            return Uri;
        }

        public virtual CouchUri CreateUri()
        {
            Uri = configuration.NewUri();
            return Uri;
        }

        public virtual CouchUri CreateUri<TModel>()
        {
            Uri = configuration.NewUri<TModel>();
            return Uri;
        }

        public virtual TModel Deserialize<TModel>( string json )
        {
            return serializer.Deserialize<TModel>( json );
        }

        public virtual ViewResult<TModel> DeserializeView<TModel>( string json )
        {
            return serializer.DeserializeList<TModel>( json );
        }

        public virtual RelaxException Exception( Exception ex, string format, params object[] args )
        {
            format.ToError<IDocumentRepository>( args );
            return new RelaxException( format.AsFormat( args ), ex );
        }

        protected BaseCouchCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer )
        {
            this.action = action;
            this.configuration = configuration;
            this.serializer = serializer;
        }
    }
}