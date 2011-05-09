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
    public class SaveAttachmentCommand : BaseCouchCommand
    {
        public virtual CommandResult SaveAttachment<TModel>( TModel model, string attachmentName, string contentType,
                                                             byte[] content )
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id( model.GetDocumentIdAsJson() )
                    .Attachment( attachmentName )
                    .Revision( model.GetDocumentRevision() );

                var result = new CommandResult( action.SaveAttachment( Uri, contentType, content ) );
                model.SetDocumentRevision( result.JsonObject["rev"].ToString() );
                model.AddAttachment( attachmentName, contentType, content.LongLength );
                return result;
            }
            catch ( Exception ex )
            {
                throw Exception( ex,
                                 "An exception occurred trying to save an attachment {0} to a document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}",
                                 attachmentName,
                                 typeof( TModel ).FullName,
                                 model.GetDocumentIdAsJson(),
                                 model.GetDocumentRevision(),
                                 Uri.ToString(),
                                 ex );
            }
        }

        public SaveAttachmentCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) 
            : base( action, configuration, serializer )
        {
        }
    }
}