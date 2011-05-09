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
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Json
{
    public class JsonUrlEncoder
    {
        protected Dictionary<Type, Action<JToken>> typeProcessor { get; set; }

        protected void Process( JToken jObject )
        {
            typeProcessor[jObject.GetType()]( jObject );
        }

        public string Encode( string root )
        {
            var token = JToken.ReadFrom( new JsonTextReader( new StringReader( root ) ) );
            Process( token );
            return token.ToString( Formatting.None );
        }

        protected void ProcessValue( JToken token )
        {
            var jValue = token as JValue;
            if ( jValue.Type == JTokenType.String )
                jValue.Value = HttpUtility.UrlEncode( jValue.Value.ToString() );
        }

        protected void ProcessArray( JToken token )
        {
            var array = token as JArray;
            if ( array.Children().Count() == 0 )
                return;

            array
                .Children()
                .ForEach( x => Process( x ) );
        }

        protected void ProcessProperty( JToken token )
        {
            var property = token as JProperty;
            Process( property.Value );
        }

        protected void ProcessObject( JToken token )
        {
            var jObject = token as JObject;

            if ( jObject.HasValues )
                jObject
                    .Children()
                    .ForEach( x => Process( x ) );
        }

        public JsonUrlEncoder()
        {
            typeProcessor = new Dictionary<Type, Action<JToken>>
                                {
                                    {typeof( JObject ), ProcessObject},
                                    {typeof( JProperty ), ProcessProperty},
                                    {typeof( JArray ), ProcessArray},
                                    {typeof( JValue ), ProcessValue}
                                };
        }
    }
}