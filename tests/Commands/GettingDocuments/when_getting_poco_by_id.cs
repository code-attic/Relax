using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_poco_by_id : with_get_poco_by_id_setup
    {
        protected static CommandResult result;
        protected static MyPoco model;
        protected static string json;
        protected static IProvideDocumentMetadata MetadataProvider;

        private Because of = () =>
            {
                MetadataProvider = Assimilate.GetInstanceOf<IProvideDocumentMetadata>();
                result = command.GetDocument<MyPoco>("1");
                model = command.Deserialize<MyPoco>(result.Json);
                json = result.Json.Replace("\r\n", "").Replace(" ", "");
            };

        private It should_produce_expected_json = () => json.ShouldEqual(response);

        private It should_create_valid_instance = () =>
            {
                var metadata = MetadataProvider.GetMetadata( "1" );
                metadata._id.ShouldEqual("1");
                metadata._rev.ShouldEqual("1");
                model.MyId.ShouldEqual( "1" );
                model.Message.ShouldEqual("Test");
            };
        private It should_call_action = () => mockAction.Verify();
    }
}