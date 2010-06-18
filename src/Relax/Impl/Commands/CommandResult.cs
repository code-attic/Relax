using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class CommandResult
    {
        public string Json { get; set; }

        public JToken JsonObject
        {
            get
            {
                if(!string.IsNullOrEmpty(Json))
                {
                    return JToken.ReadFrom(new JsonTextReader(new StringReader(Json)));
                }
                return new JObject();
            }
        }

        public TResult GetResultAs<TResult>()
        {
            if (string.IsNullOrEmpty(Json))
                return default(TResult);

            return Json.FromJson<TResult>();
        }

        public void ApplyDesignDocumentFilter()
        {
            var filter = new DesignDocumentFilter();
            Json = filter.Filter(Json);
        }

        public CommandResult(string json)
        {
            Json = json;
        }
    }
}