using Machine.Specifications;

namespace Relax.Tests.Serialization
{
    public abstract class with_single_document
    {
        protected static string json;

        private Establish context = () =>
                                        {
                                            json = @"{ _id:""test"", _rev:""10""}";
                                        };
    }
}