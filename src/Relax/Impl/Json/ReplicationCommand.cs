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
using Newtonsoft.Json;
using Relax.Impl.Http;

namespace Relax.Impl.Json
{
    [Serializable]
    [JsonObject( MemberSerialization.OptOut )]
    public class ReplicationCommand
    {
        [JsonProperty( "source" )]
        public string SourceUri { get; set; }

        [JsonProperty( "target" )]
        public string TargetUri { get; set; }

        [JsonProperty( "continuous" )]
        public bool ContinuousReplication { get; set; }

        [JsonProperty( "create_target" )]
        public bool CreateTarget { get; set; }

        public static ReplicationCommand Once( CouchUri source, CouchUri target )
        {
            return new ReplicationCommand
                       {
                           SourceUri = source.ToString(),
                           TargetUri = target.ToString(),
                           CreateTarget = true
                       };
        }

        public static ReplicationCommand Continuous( CouchUri source, CouchUri target )
        {
            return new ReplicationCommand
                       {
                           SourceUri = source.ToString(),
                           TargetUri = target.ToString(),
                           ContinuousReplication = true,
                           CreateTarget = true
                       };
        }
    }
}