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
using Symbiote.Core;

namespace Relax.Config
{
    public class CouchConfigurator
    {
        public CouchConfiguration Configuration { get; internal set; }

        public CouchConfigurator Cache()
        {
            Configuration.Cache = true;
            Configuration.CacheExpiration = DateTime.MaxValue;
            return this;
        }

        public CouchConfigurator Cache( DateTime expiration )
        {
            Configuration.Cache = true;
            Configuration.CacheExpiration = expiration;
            return this;
        }

        public CouchConfigurator Cache( TimeSpan timeLimit )
        {
            Configuration.Cache = true;
            Configuration.CacheLimit = timeLimit;
            return this;
        }

        public CouchConfigurator ExcludeTypeSpecificationFromJson()
        {
            Configuration.IncludeTypeSpecification = false;
            return this;
        }

        public CouchConfigurator FailedGetShouldThrowException()
        {
            Configuration.Throw404Exceptions = true;
            return this;
        }

        public CouchConfigurator Https()
        {
            Configuration.Protocol = "https";
            return this;
        }

        public CouchConfigurator LimitMetadataCachingTo( int itemCount )
        {
            Configuration.MetadataCacheLimit = itemCount;
            return this;
        }

        public CouchConfigurator Port( int port )
        {
            Configuration.Port = port;
            return this;
        }

        public CouchConfigurator Preauthorize( string username, string password )
        {
            Configuration.Preauthorize = true;
            Configuration.User = username;
            Configuration.Password = password;
            return this;
        }

        public CouchConfigurator CouchQueryServiceUrl( string url )
        {
            Configuration.CouchQueryServiceUrl = url;
            return this;
        }

        public CouchConfigurator Server( string server )
        {
            Configuration.Server = server;
            return this;
        }

        public CouchConfigurator TimeOut( int timeOut )
        {
            Configuration.TimeOut = timeOut;
            return this;
        }

        public CouchConfigurator DefaultDatabase( string databaseName )
        {
            Configuration.DefaultDatabaseName = databaseName;
            return this;
        }

        public CouchConfigurator AssignDatabaseToType<T>( string databaseName )
        {
            Configuration.SetDatabaseNameForType<T>( databaseName );
            return this;
        }

        public CouchConfigurator UseDatabaseTypeResolver<T>()
            where T : IResolveDatabaseNames
        {
            Configuration.DatabaseResolver = Assimilate.GetInstanceOf<T>();
            return this;
        }

        public CouchConfigurator()
        {
            Configuration = new CouchConfiguration();
        }
    }
}