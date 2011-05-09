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
using Relax.Impl.Model;
using Relax.Impl.Serialization;

namespace Relax.Impl.Commands
{
    public class GetAttachmentCommand : BaseCouchCommand
    {
        public virtual Tuple<string, byte[]> GetAttachment<TModel>( object id, string attachmentName )
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id( id )
                    .Attachment( attachmentName );

                return action.GetAttachment( Uri );
            }
            catch ( Exception ex )
            {
                throw Exception( ex,
                                 "An exception occurred trying to retrieve an attachment {0} from a document of type {1} with id {2} at {3}. \r\n\t {4}",
                                 attachmentName,
                                 typeof( TModel ).FullName,
                                 id.ToString(),
                                 Uri.ToString(),
                                 ex );
            }
        }

        public GetAttachmentCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}