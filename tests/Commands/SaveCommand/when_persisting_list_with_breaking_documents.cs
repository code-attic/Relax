using System.Linq;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Config;
using Relax.Impl;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Commands.SaveCommand
{
    class when_persisting_list_with_breaking_documents : with_nested_document_list
    {
        protected static object[] result;
        protected static string serialized;

        private Because of = () =>
                                 {
                                     var configuration = ObjectFactory.GetInstance<ICouchConfiguration>();
                                     configuration.BreakDownDocumentGraphs = true;

                                     result = testDocs.GetDocmentsFromGraph();
                                     serialized = result[0].ToJson();
                                 };

        private It should_produce_eight_documents_total = () =>
                                                          result.Length.ShouldEqual(8);

        private It should_not_include_children_property_in_json = () =>
                                                                      {
                                                                          var json = JObject.Parse(serialized);
                                                                          ShouldExtensionMethods.ShouldNotContain(json.Properties().Select(x => x.Name), "Children");
                                                                      };
    }
}