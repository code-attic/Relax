using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Relax.Impl.Json;
using Symbiote.Core.Serialization;

namespace Relax.Tests.ViewFilter
{
    public class when_filtering_design_documents_from_results : with_test_json
    {
        protected static List<Request> requests;

        private Because of = () =>
                                 {
                                     var filter = new DesignDocumentFilter();
                                     json = filter.Filter(json);

                                     var view = json.FromJson<ViewResult<Request>>();
                                     requests = view.GetList().ToList();
                                 };

        private It should_have_only_1_results = () => requests.Count.ShouldEqual(1);
    }
}