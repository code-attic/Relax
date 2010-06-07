using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class CommandResult
    {
        public string Json { get; set; }

        public JObject JsonObject
        {
            get
            {
                if(!string.IsNullOrEmpty(Json))
                {
                    return JObject.Parse(Json);
                }
                return new JObject();
            }
        }

        public TResult GetResultAs<TResult>()
        {
            if (string.IsNullOrEmpty(Json))
                return default(TResult);

            return FilterOnDocumentType(Json,typeof(TResult))
                .FromJson<TResult>();
        }

        public object GetResultAs(Type resultType)
        {
            if (string.IsNullOrEmpty(Json))
                return null;

            return FilterOnDocumentType(Json, resultType)
                .FromJson(resultType);
        }

        public CommandResult(string json)
        {
            Json = FilterOutDesignDocuments(json);
        }

        public virtual string FilterOnDocumentType(string json, Type expectedType)
        {
            if(json == null)
            {
                return "";
            }

            try
            {
                var jsonDoc = JObject.Parse(json);
                if(jsonDoc["rows"] != null)
                {
                    if (jsonDoc["rows"] != null)
                        jsonDoc["rows"]
                            .Children()
                            .Where(x => x["doc"]["$doc_type"].ToString() != expectedType.Name)
                            .ForEach(x => x.Remove());
                }
                return jsonDoc.ToString();
            }
            catch (Exception ex)
            {
                return json;
            }
        }

        public virtual string FilterOutDesignDocuments(string json)
        {
            if(json == null)
            {
                return "";    
            }

            try
            {
                var jsonDoc = JObject.Parse(json);
                if(jsonDoc["rows"] != null)
                    jsonDoc["rows"]
                        .Children()
                        .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design"))
                        .ForEach(x => x.Remove());
                return jsonDoc.ToString();
            }
            catch (Exception e)
            {
                return json;
            }
        }
    }
}