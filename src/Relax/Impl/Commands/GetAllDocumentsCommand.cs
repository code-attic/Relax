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
    public class GetAllDocumentsCommand : BaseCouchCommand
    {
        public CommandResult GetDocuments<TModel>()
        {
            try
            {
                CreateUri<TModel>()
                    .ListAll()
                    .IncludeDocuments();

                var commandResult = Get();
                commandResult.ApplyDesignDocumentFilter();
                return commandResult;
            }
            catch ( Exception ex )
            {
                var couchEx = Exception(
                    ex,
                    "An exception occurred trying to retrieve all documents of type {0} at {1}. \r\n\t {2}",
                    typeof( TModel ).FullName,
                    Uri.ToString(),
                    ex
                    );

                if ( configuration.Throw404Exceptions )
                    throw couchEx;

                return new CommandResult( "" );
            }
        }

        public GetAllDocumentsCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}