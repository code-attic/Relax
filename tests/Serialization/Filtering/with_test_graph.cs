using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Relax.Impl.Serialization;
using StructureMap;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Serialization.Filtering
{
    public class with_test_graph
    {
        protected static ClassA graph;
        protected static string IShouldnTBeHere = "I shouldn't be here!";
        protected static string fullJson;
        protected static string expectedJson;

        private Establish context = () =>
                                        {
                                            ObjectFactory
                                                .Configure(
                                                    x => x.Scan(s =>
                                                                    {
                                                                        s.AssemblyContainingType<IDocumentRepository>();
                                                                        s.AddAllTypesOf<IContractResolverStrategy>();
                                                                    }));

                                            graph = new ClassA()
                                                        {
                                                            B = new ClassB() { Message = IShouldnTBeHere},
                                                            Cs = new List<ClassC>()
                                                                     {
                                                                         new ClassC() { Message = IShouldnTBeHere},
                                                                         new ClassC() { Message = IShouldnTBeHere},
                                                                     },
                                                            Ds = new List<ClassD>()
                                                                     {
                                                                         new ClassD() { Message = IShouldnTBeHere },
                                                                         new ClassD() { Message = IShouldnTBeHere },
                                                                         new ClassD() { Message = IShouldnTBeHere },
                                                                     },
                                                            E = new ClassE() {Message ="stuff"},
                                                            Fs = new List<ClassF>()
                                                                     {
                                                                         new ClassF() {Message = "stuff"},
                                                                         new ClassF() {Message = "stuff"},
                                                                         new ClassF() {Message = "stuff"},
                                                                     }
                                                        };

                                            fullJson =
                                                @"{
""$type"": ""Relax.Tests.Serialization.Filtering.ClassA, Relax.Tests"",
B: { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassB, Relax.Tests"", Message: ""I shouldn't be here!"" },
Cs: [
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassC, Relax.Tests"", Message: ""I shouldn't be here!"" },
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassC, Relax.Tests"", Message: ""I shouldn't be here!"" },
],
Ds: [
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassD, Relax.Tests"", Message: ""I shouldn't be here!"" },
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassD, Relax.Tests"", Message: ""I shouldn't be here!"" },
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassD, Relax.Tests"", Message: ""I shouldn't be here!"" },
],
E: { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassE, Relax.Tests"", Message: ""stuff"" },
Fs: [
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"", Message: ""stuff"" },
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"", Message: ""stuff"" },
    { ""$type"": ""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"", Message: ""stuff"" },
]
}";
                                            expectedJson = 
@"{""$type"":""Relax.Tests.Serialization.Filtering.ClassA, Relax.Tests"",""E"":{""$type"":""Relax.Tests.Serialization.Filtering.ClassE, Relax.Tests"",""Message"":""stuff""},""Fs"":{""$type"":""System.Collections.Generic.List`1[[Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], mscorlib"",""$values"":[{""$type"":""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"",""Message"":""stuff""},{""$type"":""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"",""Message"":""stuff""},{""$type"":""Relax.Tests.Serialization.Filtering.ClassF, Relax.Tests"",""Message"":""stuff""}]},""_id"":""0e4b459b-421a-4708-84ce-7bb7aee652aa"",""$doc_type"":""ClassA"",""$doc_relatedIds"":{""$type"":""System.Collections.Generic.Dictionary`2[[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.Object[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib""},""_attachments"":{}}";
                                        };

        
    }
}
