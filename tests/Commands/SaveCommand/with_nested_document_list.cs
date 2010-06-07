using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_nested_document_list : with_command_factory
    {
        protected static List<ParentDoc> testDocs;

        private Establish context = () =>
                                        {
                                            testDocs = new List<ParentDoc>()
                                                           {
                                                               new ParentDoc() { 
                                                                                   Message="Parent1", 
                                                                                   Children = new List<TestDoc>
                                                                                                  {
                                                                                                      new TestDoc() { Message = "Child1"},
                                                                                                      new TestDoc() { Message = "Child2"},
                                                                                                      new TestDoc() { Message = "Child3"},
                                                                                                  }
                                                                               },
                                                               new ParentDoc() { 
                                                                                   Message="Parent1", 
                                                                                   Children = new List<TestDoc>
                                                                                                  {
                                                                                                      new TestDoc() { Message = "Child1"},
                                                                                                      new TestDoc() { Message = "Child2"},
                                                                                                      new TestDoc() { Message = "Child3"},
                                                                                                  }
                                                                               },
                                                           };
                                        };
    }
}