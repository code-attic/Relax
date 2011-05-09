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

namespace Relax.Config
{
    public class CouchConfiguration : ICouchConfiguration
    {
        protected Dictionary<Type, string> _databaseForType = new Dictionary<Type, string>();
        public IResolveDatabaseNames DatabaseResolver { get; set; }

        public string GetDatabaseNameForType<T>()
        {
            string dbname = null;
            if ( DatabaseResolver != null )
            {
                dbname = DatabaseResolver.GetDatabaseNameFor<T>();
            }
            if ( dbname == null )
            {
                var type = typeof( T );
                _databaseForType.TryGetValue( type, out dbname );
            }
            return (string.IsNullOrEmpty( dbname ) ? DefaultDatabaseName : dbname).ToLower();
        }

        public void SetDatabaseNameForType<T>( string databaseName )
        {
            _databaseForType[typeof( T )] = databaseName.ToLower();
        }

        protected bool cache { get; set; }

        public bool Cache { 
            get { return cache; } 
            set { 
                cache = value; 
            } }
        public DateTime CacheExpiration { get; set; }
        public TimeSpan CacheLimit { get; set; }
        public string CouchQueryServiceUrl { get; set; }
        public string DefaultDatabaseName { get; set; }
        public bool IncludeTypeSpecification { get; set; }
        public int MetadataCacheLimit { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool Preauthorize { get; set; }
        public string Protocol { get; set; }
        public string Server { get; set; }
        public bool Throw404Exceptions { get; set; }
        public int TimeOut { get; set; }
        public string User { get; set; }

        public CouchConfiguration()
        {
            Protocol = "http";
            Server = "localhost";
            Port = 5984;
            Preauthorize = false;
            TimeOut = 6000;
            MetadataCacheLimit = 5000;
            IncludeTypeSpecification = true;
            CouchQueryServiceUrl = @"http://localhost:8420/";
            DefaultDatabaseName =
                (System.Reflection.Assembly.GetEntryAssembly() ??
                 System.Reflection.Assembly.GetExecutingAssembly()).GetName().Name.Replace( ".", "" );
        }
    }
}