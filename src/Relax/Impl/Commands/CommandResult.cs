using System.Linq;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class CommandResult<TResult>
    {
        public TResult Result { get; set; }
        public string Json { get; set; }

        public CommandResult(string json)
        {
            Json = FilterOutDesignDocuments(json);
            Result = json.FromJson<TResult>();
        }

        public virtual string FilterOutDesignDocuments(string json)
        {
            var jsonDoc = JObject.Parse(json);
            if(jsonDoc["rows"] != null)
                IEnumerableExtenders.ForEach<JToken>(jsonDoc["rows"]
                                         .Children()
                                         .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design")), x => x.Remove());
            return jsonDoc.ToString();
        }
    }
}