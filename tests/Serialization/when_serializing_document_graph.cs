using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Machine.Specifications;

namespace Relax.Tests.Serialization
{
    public class when_serializing_document_graph
    {
        protected static TestResolver resolver;
        protected static Type target;
        protected static List<MemberInfo> documentMembers;
        protected static string json;
        protected static string expected = "";

        private Because of = () =>
                                 {
                                     resolver = new TestResolver();
                                     target = typeof (ClassA);
                                     documentMembers = resolver.GetSubDocumentMembers(target);
                                     
                                 };

        private It should_have_four_document_members = () => documentMembers.Count.ShouldEqual(4);
    }
}
