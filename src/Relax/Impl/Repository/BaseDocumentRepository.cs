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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Relax.Config;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Model;
using Relax.Impl.Serialization;

namespace Relax.Impl.Repository
{
    public abstract class BaseDocumentRepository : IDocumentRepository
    {
        protected ConcurrentDictionary<string, IHttpAction> _continuousUpdateCommands =
            new ConcurrentDictionary<string, IHttpAction>();

        protected ICouchConfiguration configuration { get; set; }
        protected CouchCommandFactory commandFactory { get; set; }

        public virtual bool DeleteAttachment<TModel>( TModel model, string attachmentName )
            where TModel : IHaveAttachments
        {
            var command = commandFactory.CreateDeleteAttachmentCommand();
            var response = command.DeleteAttachment( model, attachmentName );
            return true;
        }

        public virtual bool DeleteDocument<TModel>( object id )
        {
            var deleteCommand = commandFactory.CreateDeleteCommand();
            var getCommand = commandFactory.CreateGetDocumentCommand();

            var getResult = getCommand.GetDocument<TModel>( id );
            var doc = getCommand.Deserialize<TModel>( getResult.Json );
            deleteCommand.DeleteDocument( doc );
            return true;
        }

        public virtual bool DeleteDocument<TModel>( object id, string rev )
        {
            var deleteCommand = commandFactory.CreateDeleteCommand();
            deleteCommand.DeleteDocument<TModel>( id, rev );
            return true;
        }

        public virtual IList<TModel> FromView<TModel>( string designDocument, string viewName, Action<ViewQuery> query )
        {
            var command = commandFactory.CreateGetFromViewCommand();
            var response = command.GetFromView<TModel>( designDocument, viewName, query );
            var docs = command.DeserializeView<TModel>( response.Json ).GetList().ToList();
            return docs;
        }

        public virtual TModel Get<TModel>( object id, string revision )
        {
            var command = commandFactory.CreateGetDocumentCommand();
            var response = command.GetDocument<TModel>( id, revision );
            var doc = command.Deserialize<TModel>( response.Json );
            return doc;
        }

        public virtual TModel Get<TModel>( object id )
        {
            var command = commandFactory.CreateGetDocumentCommand();
            var response = command.GetDocument<TModel>( id );
            var doc = command.Deserialize<TModel>( response.Json );
            return doc;
        }

        public virtual IList<TModel> GetAll<TModel>()
        {
            var command = commandFactory.CreateGetAllDocumentsCommand();
            var response = command.GetDocuments<TModel>();
            var docs = command.DeserializeView<TModel>( response.Json ).GetList().ToList();
            return docs;
        }

        public virtual IList<TModel> GetAll<TModel>( int pageSize, int pageNumber )
        {
            var command = commandFactory.CreateGetDocumentsPagedCommand();
            var response = command.GetDocumentsPaged<TModel>( pageSize, pageNumber );
            var docs = command.DeserializeView<TModel>( response.Json ).GetList().ToList();
            return docs;
        }

        public virtual IList<TModel> GetAllByKeys<TModel>( object[] ids )
        {
            var command = commandFactory.CreateGetDocumentsByIdsCommand();
            var response = command.GetDocuments<TModel>( ids );
             var docs = command.DeserializeView<TModel>( response.Json ).GetList().ToList();
            return docs;
        }

        public virtual IList<TModel> GetAllBetweenKeys<TModel>( object startingWith, object endingWith )
        {
            var command = commandFactory.CreateGetDocumentsInRangeCommand();
            var response = command.GetDocumentsInRange<TModel>( startingWith, endingWith );
            var docs = command.DeserializeView<TModel>( response.Json ).GetList().ToList();
            return docs;
        }

        public IList<TModel> GetAllByCriteria<TModel>( Expression<Func<TModel, bool>> criteria )
        {
            var command = commandFactory.CreateQueryCommand();
            var ids = command.GetDocumentIdsForQuery( criteria );
            return GetAllByKeys<TModel>( ids );
        }

        public virtual Tuple<string, byte[]> GetAttachment<TModel>( object id, string attachmentName )
            where TModel : IHaveAttachments
        {
            var command = commandFactory.CreateGetAttachmentCommand();
            var response = command.GetAttachment<TModel>( id, attachmentName );
            return response;
        }

        public virtual bool Save<TModel>( TModel model )
        {
            var command = commandFactory.CreateSaveDocumentCommand();
            var response = command.Save( model );
            return true;
        }

        public virtual bool Save<TModel>( object id, TModel model )
        {
            var command = commandFactory.CreateSaveDocumentCommand();
            var response = command.Save( model );
            return true;
        }

        public virtual bool SaveAll<TModel>( IEnumerable<TModel> list )
        {
            var command = commandFactory.CreateSaveDocumentsCommand();
            var response = command.SaveAll( list );
            return true;
        }

        public virtual bool SaveAttachment<TModel>( TModel model, string attachmentName, string contentType,
                                                    byte[] content )
            where TModel : IHaveAttachments
        {
            var command = commandFactory.CreateSaveAttachmentCommand();
            var response = command.SaveAttachment( model, attachmentName, contentType, content );
            return true;
        }

        public virtual void HandleUpdates<TModel>( int since, Action<string, ChangeRecord> onUpdate,
                                                   AsyncCallback updatesInterrupted )
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            HandleUpdates( database, since, onUpdate, updatesInterrupted );
        }

        public virtual void HandleUpdates( string database, int since, Action<string, ChangeRecord> onUpdate,
                                           AsyncCallback updatesInterrupted )
        {
            var command = commandFactory.CreateStreamCommand();
            _continuousUpdateCommands[database] = command.BeginStreaming( database, since, onUpdate, updatesInterrupted );
        }

        public virtual void StopChangeStreaming<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            StopChangeStreaming( database );
        }

        public virtual void StopChangeStreaming( string database )
        {
            IHttpAction command;
            if ( _continuousUpdateCommands.TryGetValue( database, out command ) )
            {
                command.StopContinousResponse();
                _continuousUpdateCommands.TryRemove( database, out command );
            }
        }

        public void Dispose()
        {
        }

        protected BaseDocumentRepository( ICouchConfiguration configuration, ISerializationProvider serializer )
        {
            this.configuration = configuration;
            commandFactory = new CouchCommandFactory( configuration, serializer );
        }
    }
}