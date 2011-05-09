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

namespace Relax.Config
{
    public interface ICouchConfiguration
    {
        bool Cache { get; set; }
        DateTime CacheExpiration { get; set; }
        TimeSpan CacheLimit { get; set; }
        string CouchQueryServiceUrl { get; set; }
        string DefaultDatabaseName { get; set; }
        bool IncludeTypeSpecification { get; set; }
        int MetadataCacheLimit { get; set; }
        string Password { get; set; }
        int Port { get; set; }
        bool Preauthorize { get; set; }
        string Protocol { get; set; }
        string Server { get; set; }
        bool Throw404Exceptions { get; set; }
        int TimeOut { get; set; }
        string User { get; set; }
        string GetDatabaseNameForType<T>();
        void SetDatabaseNameForType<T>( string databaseName );
    }
}