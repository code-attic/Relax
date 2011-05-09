using System.Diagnostics;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_poco_by_id_timed : with_get_poco_by_id_setup
    {
        protected static CommandResult result;
        protected static MyPoco model;
        protected static string json;
        protected static IProvideDocumentMetadata MetadataProvider;
        protected static Stopwatch watch;

        private Because of = () =>
            {
                MetadataProvider = Assimilate.GetInstanceOf<IProvideDocumentMetadata>();

                watch = Stopwatch.StartNew();

                result = command.GetDocument<MyPoco>("1");
                for( int i = 0; i < 10000; i++ )
                {
                    model = command.Deserialize<MyPoco>(result.Json);
                }

                watch.Stop();
            };

        private It should_not_take_over_1_second = () => watch.ElapsedMilliseconds.ShouldBeLessThan( 1000 );
    }
}