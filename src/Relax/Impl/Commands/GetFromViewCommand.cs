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
    public class GetFromViewCommand : BaseCouchCommand
    {
        public CommandResult GetFromView<TModel>( string designDocument, string viewName, Action<ViewQuery> query )
        {
            try
            {
                CreateUri<TModel>()
                    .Design( designDocument )
                    .View( viewName );

                var viewQuery = new ViewQuery( Uri );
                query( viewQuery );
                var commandResult = Get();
                commandResult.ApplyDesignDocumentFilter();
                return commandResult;
            }
            catch ( Exception ex )
            {
                throw Exception(
                    ex,
                    "An exception occurred trying to retrieve a documents of type {0} from design document {1} and view {2} at {3}. \r\n\t {4}",
                    typeof( TModel ).FullName,
                    designDocument,
                    viewName,
                    Uri.ToString(),
                    ex
                    );
            }
        }

        public GetFromViewCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}