using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Impl.Json;
using Symbiote.Core.Extensions;
using IEnumerableExtenders = Symbiote.Core.Extensions.IEnumerableExtenders;

namespace Relax.Tests.ViewFilter
{
    public class when_filtering_design_documents_from_results : with_test_json
    {
        protected static List<Request> requests;

        private Because of = () =>
                                 {
                                     var jsonDoc = JObject.Parse(json);
                                     IEnumerableExtenders.ForEach<JToken>(jsonDoc["rows"]
                                                              .Children()
                                                              .Where(x => x["doc"]["_id"].ToString().StartsWith(@"""_design")), x => x.Remove());
                                     json = jsonDoc.ToString();

                                     var view = json.FromJson<ViewResult<Request>>();
                                     requests = view.GetList().ToList();
                                 };

        private It should_have_only_5_results = () => requests.Count.ShouldEqual(5);
    }
}