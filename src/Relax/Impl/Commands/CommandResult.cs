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
using System.IO;
using Relax.Impl.Json;
using Symbiote.Core.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Relax.Impl.Commands
{
    public class CommandResult
    {
        public string Json { get; set; }

        public JToken JsonObject
        {
            get
            {
                if ( !string.IsNullOrEmpty( Json ) )
                {
                    return JToken.ReadFrom( new JsonTextReader( new StringReader( Json ) ) );
                }
                return new JObject();
            }
        }

        public TResult GetResultAs<TResult>()
        {
            if ( string.IsNullOrEmpty( Json ) )
                return default(TResult);

            return Json.FromJson<TResult>();
        }

        public void ApplyDesignDocumentFilter()
        {
            var filter = new DesignDocumentFilter();
            Json = filter.Filter( Json );
        }

        public CommandResult( string json )
        {
            Json = json;
        }
    }
}