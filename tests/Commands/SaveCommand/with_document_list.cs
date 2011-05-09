using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_document_list : with_command_factory
    {
        protected static List<TestDoc> testDocs;

        private Establish context = () =>
                                        {
                                            testDocs = new List<TestDoc>()
                                                           {
                                                               new TestDoc() { Message = "Message1"},
                                                               new TestDoc() { Message = "Message2"},
                                                               new TestDoc() { Message = "Message3"},
                                                           };
                                        };
    }
}
