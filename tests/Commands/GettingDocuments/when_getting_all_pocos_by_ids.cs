using System.Linq;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_all_pocos_by_ids : with_get_all_pocos_by_ids
    {
        protected static CommandResult result;
        protected static ViewResult<MyPoco> viewResult;
        protected static string json;
        protected static IProvideDocumentMetadata MetadataProvider;

        private Because of = () =>
            {
                MetadataProvider = Assimilate.GetInstanceOf<IProvideDocumentMetadata>();
                result = command.GetDocuments<TestDoc>(new object[] {"1","2"});
                viewResult = command.DeserializeView<MyPoco>( result.Json );
                json = result.Json.Replace("\r\n", "").Replace(" ", "");
            };

        private It should_produce_expected_json = () => json.ShouldEqual(response);

        private It should_have_two_rows = () => ShouldExtensionMethods.ShouldEqual(viewResult.GetList().Count(), 2);

        private It should_create_valid_instances = () =>
            {
                var metadata = MetadataProvider.GetMetadata( "1" );
                var firstDoc = viewResult.GetList().ToList()[0];
                metadata._id.ShouldEqual("1");
                metadata._rev.ShouldEqual("1");
                firstDoc.Message.ShouldEqual("Test1");

                var secondDoc = viewResult.GetList().ToList()[1];
                metadata = MetadataProvider.GetMetadata( "2" );
                metadata._id.ShouldEqual("2");
                metadata._rev.ShouldEqual("1");
                secondDoc.Message.ShouldEqual("Test2");
            };

        private It should_call_action = () => mockAction.Verify();
    }
}