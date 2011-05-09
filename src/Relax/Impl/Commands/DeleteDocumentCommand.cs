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
    public class DeleteDocumentCommand : BaseCouchCommand
    {
        public void DeleteDocument<TModel>( object id, object rev )
        {
            try
            {
                CreateUri<TModel>()
                    .IdAndRev( id, rev );

                action.Delete( Uri );
            }
            catch ( Exception ex )
            {
                throw Exception( ex,
                                 "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}",
                                 typeof( TModel ).FullName,
                                 id,
                                 Uri,
                                 ex );
            }
        }

        public void DeleteDocument<TModel>( TModel model )
        {
            object id = null;
            object rev = null;

            try
            {
                id = model.GetDocumentIdAsJson();
                rev = model.GetDocumentRevision();

                DeleteDocument<TModel>( id, rev );
            }
            catch ( RelaxException rex )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw Exception( ex,
                                 "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}",
                                 typeof( TModel ).FullName,
                                 id,
                                 Uri,
                                 ex );
            }
        }

        public DeleteDocumentCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}