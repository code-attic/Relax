using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Json
{
    public class DesignDocumentFilter
    {
        protected Dictionary<Type, Action<JToken>> typeProcessor { get; set; }

        public DesignDocumentFilter()
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

        public string Filter(string root)
        {
            var token = JToken.ReadFrom(new JsonTextReader(new StringReader(root)));
            Process(token);
            return token.ToString();
        }

        protected void ProcessValue(JToken token)
        {
            var jValue = token as JValue;
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
            if (property.Name == "_id" && property.Value.ToString().StartsWith(@"""_design"))
            {
                try
                {
                    property.Parent.Parent.Parent.Remove();
                }
                catch (Exception e)
                {
                    // do nothing here
                    // this means we've gotten back some odd fluke
                    // of a json result from CouchDB which magically includes
                    // a design document at an unexpected level of the hierarchy
                }
            }
            else
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
