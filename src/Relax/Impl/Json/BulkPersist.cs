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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Symbiote.Core.Extensions;
using Symbiote.Core.Serialization;
using Newtonsoft.Json;

namespace Relax.Impl.Json
{
    public class BulkPersist
    {
        protected static readonly string pattern = @"(?<=values[""][:])(.+)(?=[\}])";

        [JsonProperty( PropertyName = "all_or_nothing" )]
        public bool AllOrNothing { get; set; }

        [JsonProperty( PropertyName = "non_atomic" )]
        public bool NonAtomic { get; set; }

        [JsonProperty( PropertyName = "docs" )]
        public object[] Documents { get; set; }

        public override string ToString()
        {
            var regex = new Regex( pattern );
            var docList = Documents.ToJson();
            var array = regex.Match( docList ).Value;
            return "{{\"all_or_nothing\":{0}, \"non_atomic\":{1}, \"docs\":{2} }}"
                .AsFormat( AllOrNothing.ToString().ToLower(), NonAtomic.ToString().ToLower(), array );
        }

        public BulkPersist()
        {
        }

        public BulkPersist( IEnumerable<object> docs )
        {
            Documents = docs.ToArray();
        }

        public BulkPersist( bool allOrNothing, bool nonAtomic, IEnumerable<object> docs )
        {
            AllOrNothing = allOrNothing;
            NonAtomic = nonAtomic;
            Documents = docs.ToArray();
        }
    }
}