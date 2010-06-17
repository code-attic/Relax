using Newtonsoft.Json;

namespace Relax.Impl.Json
{
    public class ViewRow<TModel>
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonIgnore()]
        public TModel Model
        {
            get
            {
                var type = typeof(TModel);
                if(type.IsClass)
                {
                    var value = Document as object;
                    return value == null ? Value : Document;
                }
                else
                {
                    return Document.Equals(default(TModel)) ? Value : Document;
                }
            }
        }

        [JsonProperty(PropertyName = "doc")]
        protected TModel Document { get; set; }

        [JsonProperty(PropertyName = "value")]
        protected TModel Value { get; set; }
    }
}