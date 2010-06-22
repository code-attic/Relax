using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_single_parent_document : with_command_factory
    {
        protected static ParentDoc testDoc;

        private Establish context = () =>
                                        {
                                            testDoc = new ParentDoc()
                                                          {
                                                              DocumentId = "doc 1",
                                                              Message = "Parent1",
                                                              Children = new List<TestDoc>()
                                                                             {
                                                                                 new TestDoc() { Message = "Child1"}
                                                                             }
                                                          };
                                        };
    }
}