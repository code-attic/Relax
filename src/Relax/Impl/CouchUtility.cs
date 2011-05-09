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
using System.Net;
using Relax.Config;
using Relax.Impl.Commands;
using Relax.Impl.Http;
using Relax.Impl.Serialization;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public class CouchUtility
    {
        protected ConcurrentDictionary<string, bool> _databaseExists = new ConcurrentDictionary<string, bool>();
        protected ICouchConfiguration configuration { get; set; }
        protected CouchCommandFactory commandFactory { get; set; }

        public virtual void EnsureDatabaseExists<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            EnsureDatabaseExists( database );
        }

        public virtual void CreateDatabase<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            CreateDatabase( database );
        }

        public virtual bool DatabaseExists<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            return DatabaseExists( database );
        }

        public virtual void EnsureDatabaseExists( string database )
        {
            if ( string.IsNullOrEmpty( database ) )
                return;

            var dbCreated = false;
            var shouldCheckCouch = false;
            ServerCommand command = null;
            try
            {
                shouldCheckCouch = !_databaseExists.TryGetValue( database, out dbCreated );
                if ( shouldCheckCouch && !dbCreated )
                {
                    command = commandFactory.CreateServerCommand();
                    command.CreateDatabase( database );
                    _databaseExists[database] = true;
                }
            }
            catch ( WebException webEx )
            {
                if ( webEx.Message.Contains( "(412) Precondition Failed" ) )
                {
                    _databaseExists[database] = true;
                }
                else
                {
                    "An exception occurred while trying to check for the existence of database {0} at uri {1}. \r\n\t {2}"
                        .ToError<IDocumentRepository>( database, command.Uri, webEx );
                    throw;
                }
            }
            catch ( Exception ex )
            {
                "An exception occurred while trying to check for the existence of database {0} at uri {1}. \r\n\t {2}"
                    .ToError<IDocumentRepository>( database, command.Uri, ex );
                throw;
            }
        }

        public virtual void CreateDatabase( string database )
        {
            var command = commandFactory.CreateServerCommand();
            command.CreateDatabase( database );
            _databaseExists[database] = true;
        }

        public virtual bool DatabaseExists( string database )
        {
            var command = commandFactory.CreateServerCommand();
            var exists = command.DatabaseExists( database );
            _databaseExists[command.Uri.DatabaseName] = exists;
            return exists;
        }

        public virtual CouchUri NewUri()
        {
            var uri = configuration.Preauthorize
                          ? CouchUri.Build(
                              configuration.User,
                              configuration.Password,
                              configuration.Protocol,
                              configuration.Server,
                              configuration.Port )
                          : CouchUri.Build(
                              configuration.Protocol,
                              configuration.Server,
                              configuration.Port );
            return uri;
        }

        public virtual CouchUri NewUri<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            return NewUri( database );
        }

        public virtual CouchUri NewUri( string database )
        {
            var uri = configuration.Preauthorize
                          ? CouchUri.Build(
                              configuration.User,
                              configuration.Password,
                              configuration.Protocol,
                              configuration.Server,
                              configuration.Port,
                              database )
                          : CouchUri.Build(
                              configuration.Protocol,
                              configuration.Server,
                              configuration.Port,
                              database );
            EnsureDatabaseExists( database );
            return uri;
        }

        public virtual string GetDatabaseForType<TModel>()
        {
            return configuration.GetDatabaseNameForType<TModel>();
        }

        public CouchUtility( ICouchConfiguration couchConfiguration, ISerializationProvider serializer )
        {
            configuration = couchConfiguration;
            commandFactory = new CouchCommandFactory( couchConfiguration, serializer );
        }
    }
}