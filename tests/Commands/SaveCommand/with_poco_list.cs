using System.Collections.Generic;
using Machine.Specifications;

namespace Relax.Tests.Commands.SaveCommand
{
    public abstract class with_poco_list : with_command_factory
    {
        protected static List<MyPoco> testDocs;

        private Establish context = () =>
            {
                testDocs = new List<MyPoco>()
                    {
                        new MyPoco() { Message = "Message1", MyId = "1" },
                        new MyPoco() { Message = "Message2", MyId = "2" },
                        new MyPoco() { Message = "Message3", MyId = "3" },
                    };
            };
    }
}