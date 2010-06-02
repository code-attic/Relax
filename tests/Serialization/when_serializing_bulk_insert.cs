using System.Linq;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Serialization
{
    public class when_serializing_bulk_insert : with_bulk_insert
    {
        protected static string json;
        protected static JToken jBlob;
        protected static JObject newObj;
        protected static string newJson;
        protected static BulkPersist newAction;

        private Because of = () =>
                                 {
                                     json = bulkAction.ToJson(true);
                                     jBlob = JObject.Parse(json);

                                     var hasTypes = jBlob.Children().OfType<JProperty>().FirstOrDefault(x => x.Name == "$type") != null;

                                     var allOrNothing = jBlob["all_or_nothing"];
                                     var nonAtomic = jBlob["non_atomic"];
                                     var docs = jBlob["docs"]["$values"];

                                     newObj = new JObject(
                                         new JProperty("all_or_nothing", allOrNothing),
                                         new JProperty("non_atomic", nonAtomic),
                                         new JProperty("docs", docs)
                                         );

                                     newAction = newObj.ToString().FromJson<BulkPersist>();
                                 };

        private It should_be_equivalent = () => 
                                          newAction.Documents.SequenceEqual(bulkAction.Documents).ShouldBeTrue();
    }
}