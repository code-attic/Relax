using Machine.Specifications;

namespace Relax.Tests.Serialization
{
    public abstract class with_view_result_and_documents_included
    {
        protected static string viewResultJson;

        private Establish context = () =>
                                        {
                                            viewResultJson = @"
{
  rows: 
    [
        {
            id: ""8dd62969-9070-4f34-b478-e3e7e1c792aa"",
            key: [""Test"",1],
            value: { 
                _id: ""8dd62969-9070-4f34-b478-e3e7e1c792aa"", 
                _rev:""1-7b9bfed448e9339c7afdbfd42964b02e"", 
                Message: ""Hi""
            },
            doc: { 
                _id: ""8dd62969-9070-4f34-b478-e3e7e1c792aa"", 
                _rev:""1-7b9bfed448e9339c7afdbfd42964b02e"", 
                Message: ""Howdy""
            }
        }
    ]
}
";
                                        };
    }
}