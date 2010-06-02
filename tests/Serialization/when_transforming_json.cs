using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Relax.Tests.Serialization
{
    public abstract class with_assimilation
    {
        
    }

    public abstract class with_single_document
    {
        protected static string json;

        private Establish context = () =>
                                        {
                                            json = @"{ _id:""test"", _rev:""10""}";
                                        };
    }

    public class Test
    {
        public string Id { get; set; }
        public string Rev { get; set; }
    }

    public class when_trying_custom_serializer : with_single_document
    {
        protected static Test test;

        private Because of = () =>
                                 {
                                     var resolver = new Resolver();
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
