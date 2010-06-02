using System.IO;
using Machine.Specifications;
using Newtonsoft.Json;
using Relax.Impl.Serialization;

namespace Relax.Tests.Serialization
{
    public class when_trying_custom_serializer : with_single_document
    {
        protected static Test test;

        private Because of = () =>
                                 {
                                     var resolver = new ConventionContractResolver(new DocumentConventions());
                                     var serializer = new JsonSerializer()
                                                          {
                                                              ContractResolver = resolver
                                                          };
                                     var txtReader = new StringReader(json);
                                     var jsonReader = new JsonTextReader(txtReader);
                                     test = serializer.Deserialize(jsonReader, typeof(Test)) as Test;
                                 };

        private It should_populate_id = () => test.Id.ShouldEqual("test");
        private It should_populate_rev = () => test.Rev.ShouldEqual("10");
    }
}