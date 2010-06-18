using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Json
{
    public class JsonUrlEncoder
    {
        protected Dictionary<Type, Action<JToken>> typeProcessor { get; set; }

        public JsonUrlEncoder()
        {
            typeProcessor = new Dictionary<Type, Action<JToken>> {
                    {typeof(JObject),ProcessObject},
                    {typeof(JProperty),ProcessProperty},
                    {typeof(JArray),ProcessArray},
                    {typeof(JValue),ProcessValue}
                };
        }

        protected void Process(JToken jObject)
        {
            typeProcessor[jObject.GetType()](jObject);
        }

        public string Encode(string root)
        {
            var token = JToken.ReadFrom(new JsonTextReader(new StringReader(root)));
            Process(token);
            return token.ToString(Formatting.None);
        }

        protected void ProcessValue(JToken token)
        {
            var jValue = token as JValue;
            if(jValue.Type == JTokenType.String)
                jValue.Value = HttpUtility.UrlEncode(jValue.Value.ToString());
        }

        protected void ProcessArray(JToken token)
        {
            var array = token as JArray;
            if (array.Children().Count() == 0)
                return;

            array
                .Children()
                .ForEach(x => Process(x));
        }

        protected void ProcessProperty(JToken token)
        {
            var property = token as JProperty;
            Process(property.Value);
        }

        protected void ProcessObject(JToken token)
        {
            var jObject = token as JObject;

            if (jObject.HasValues)
                jObject
                    .Children()
                    .ForEach(x => Process(x));
        }
    }
}
