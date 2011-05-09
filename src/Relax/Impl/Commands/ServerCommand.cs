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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Serialization;
using Symbiote.Core.Serialization;

namespace Relax.Impl.Commands
{
    public class ServerCommand : BaseCouchCommand
    {
        public void CleanViews<TModel>()
        {
            CreateUri<TModel>()
                .CleanupViews();
            Post();
        }

        public void CompactDatabase<TModel>()
        {
            CreateUri<TModel>()
                .Compact();
            Post();
        }

        public void CompactView<TModel>( string view )
        {
            CreateUri<TModel>()
                .CompactView( view );
            Post();
        }

        public void CopyDatabase<TModel>( CouchUri targetUri )
        {
            try
            {
                CreateUri().Replicate();
                var sourceUri = configuration.NewUri<TModel>();
                var request = ReplicationCommand.Once( sourceUri, targetUri );
                var body = request.ToJson( false );
                Post( body );
            }
            catch ( WebException ex )
            {
                //do nothing, it's a timeout
                if ( !ex.Message.Contains( "timed out" ) )
                    throw;
            }
        }

        public void CopyDatabase( CouchUri sourceUri, CouchUri targetUri )
        {
            try
            {
                CreateUri().Replicate();
                var request = ReplicationCommand.Once( sourceUri, targetUri );
                var body = request.ToJson( false );
                Post( body );
            }
            catch ( WebException ex )
            {
                //do nothing, it's a timeout
                if ( !ex.Message.Contains( "timed out" ) )
                    throw;
            }
        }

        public void CreateDatabase<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            CreateDatabase( database );
        }

        public void CreateDatabase( string database )
        {
            Uri = configuration.Preauthorize
                      ? new CouchUri( configuration.User, configuration.Password, configuration.Protocol,
                                      configuration.Server, configuration.Port, database )
                      : new CouchUri( configuration.Protocol, configuration.Server, configuration.Port, database );
            Put();
        }

        public bool DatabaseExists<TModel>()
        {
            return DatabaseExists( configuration.GetDatabaseNameForType<TModel>() );
        }

        public bool DatabaseExists( string database )
        {
            try
            {
                Uri = new CouchUri( configuration.Protocol, configuration.Server, configuration.Port, database );
                var response = action.Get( Uri );
                return !string.IsNullOrEmpty( response ) && !response.StartsWith( "{\"error\"" );
            }
            catch ( Exception ex )
            {
                throw Exception(
                    ex,
                    "An exception occurred checking for the existence of database {0} at {1}. \r\n\t {2}",
                    database,
                    Uri,
                    ex
                    );
            }
        }

        public IList<string> GetDatabaseList()
        {
            CreateUri( "_all_dbs" );
            var result = Get();
            return result.GetResultAs<string[]>().ToList();
        }

        public void DeleteDatabase<TModel>()
        {
            CreateUri<TModel>();
            Delete();
        }

        public void Replicate<TModel>( CouchUri targetUri )
        {
            CreateUri().Replicate();
            var sourceUri = configuration.NewUri<TModel>();
            var request = ReplicationCommand.Continuous( sourceUri, targetUri );
            var body = request.ToJson( false );
            Post( body );
        }

        public void Replicate( CouchUri sourceUri, CouchUri targetUri )
        {
            CreateUri().Replicate();
            var request = ReplicationCommand.Continuous( sourceUri, targetUri );
            var body = request.ToJson( false );
            Post( body );
        }

        public ServerCommand( IHttpAction action, ICouchConfiguration configuration, ISerializationProvider serializer ) : base( action, configuration, serializer )
        {
        }
    }
}