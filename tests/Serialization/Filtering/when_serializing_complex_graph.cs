using Machine.Specifications;
using Symbiote.Core;
using Symbiote.Core.Extensions;
using Relax.Impl;

namespace Relax.Tests.Serialization.Filtering
{
    public class when_serializing_complex_graph : with_test_graph
    {
        protected static ClassA result;
        protected static string resultJson;
        protected static object[] documentList;
        protected static object[] listOnly;

        private Because of = () =>
                                 {
                                     Assimilate.Core().Relax(x => x.UseDefaults());
                                     result = fullJson.FromJson<ClassA>();
                                     resultJson = graph.ToJson();
                                     documentList = graph.GetDocmentsFromGraph();
                                     listOnly = graph.Cs.GetDocmentsFromGraph();
                                 };

        private It should_deserialize_full_graph = () =>
                                                       {
                                                           result.B.Message = IShouldnTBeHere;
                                                           result.Cs.Count.ShouldEqual(2);
                                                           result.Ds.Count.ShouldEqual(3);
                                                           result.E.Message.ShouldEqual("stuff");
                                                           result.Fs.Count.ShouldEqual(3);
                                                       };

        private It should_produce_abbreviated_json = () => 
            resultJson.ShouldEqual(expectedJson);

        private It should_have_six_documents = () => 
            documentList.Length.ShouldEqual(7);

        private It should_have_only_c_documents = () => 
            listOnly.Length.ShouldEqual(2);
    }
}