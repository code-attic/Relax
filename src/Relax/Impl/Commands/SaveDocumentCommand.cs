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
using Symbiote.Core.Serialization;

namespace Relax.Impl.Commands
{
    public class SaveDocumentCommand :
        BaseCouchCommand,
        ISaveDocument
    {
        public virtual CommandResult Save<TModel>( TModel model )
        {
            var databaseName = configuration.GetDatabaseNameForType<TModel>();
            return Save( databaseName, model );
        }

        public virtual CommandResult Save( string databaseName, object model )
        {
            try
            {
                CreateUri( databaseName )
                    .Id( model.GetDocumentId() );

                var body = model.ToJson();
                var result = Put( body );
                model.SetDocumentRevision( result.GetResultAs<SaveResponse>().Revision );
                return result;
            }
            catch ( Exception ex )
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to save a document of type {0} at {1}. \r\n\t {2}",
                    model.GetType().FullName,
                    Uri.ToString(),
                    ex
                    );
            }
        }

        public SaveDocumentCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) : base( action, configuration, serializer )
        {
        }
    }
}