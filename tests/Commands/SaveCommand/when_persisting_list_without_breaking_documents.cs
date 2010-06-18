using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Newtonsoft.Json.Linq;
using Relax.Config;
using Relax.Impl;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Commands.SaveCommand
{
    class when_persisting_list_without_breaking_documents : with_nested_document_list
    {
        protected static object[] result;
        protected static string serialized;

        private Because of = () =>
                                 {
                                     var configuration = ObjectFactory.GetInstance<ICouchConfiguration>();
                                     configuration.BreakDownDocumentGraphs = false;

                                     result = testDocs.GetDocmentsFromGraph();

                                     serialized = result[0].ToJson();
                                 };
        
        private It should_produce_six_documents_total = () => 
            result.Length.ShouldEqual(2);

        private It should_have_children_collection_included_in_json = () =>
                                                                       {
                                                                           var json = JObject.Parse(serialized);
                                                                           json["Children"]["$values"].Children().Count().
                                                                               ShouldEqual(3);
                                                                       };

    }
}
