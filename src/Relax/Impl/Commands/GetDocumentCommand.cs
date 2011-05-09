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
using Relax.Impl.Serialization;

namespace Relax.Impl.Commands
{
    public class GetDocumentCommand : BaseCouchCommand
    {
        public CommandResult GetDocument<TModel>( object id )
        {
            try
            {
                CreateUri<TModel>()
                    .Id( id );
                return Get();
            }
            catch ( Exception ex )
            {
                var couchEx = Exception( ex,
                                         "An exception occurred trying to retrieve a document of type {0} with id {1} at {2}. \r\n\t {3}",
                                         typeof( TModel ).FullName,
                                         id,
                                         Uri.ToString(),
                                         ex
                    );

                if ( configuration.Throw404Exceptions )
                    throw couchEx;

                return new CommandResult( "" );
            }
        }

        public CommandResult GetDocument<TModel>( object id, object rev )
        {
            try
            {
                CreateUri<TModel>()
                    .IdAndRev( id, rev );
                return Get();
            }
            catch ( Exception ex )
            {
                var couchEx = Exception( ex,
                                         "An exception occurred trying to retrieve a document of type {0} with id {1} and rev {2} at {3}. \r\n\t {4}",
                                         typeof( TModel ).FullName,
                                         id,
                                         rev,
                                         Uri.ToString(),
                                         ex
                    );

                if ( configuration.Throw404Exceptions )
                    throw couchEx;

                return new CommandResult( "" );
            }
        }

        public GetDocumentCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}