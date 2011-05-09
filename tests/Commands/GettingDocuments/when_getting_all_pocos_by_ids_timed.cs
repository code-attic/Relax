using System.Diagnostics;
using Machine.Specifications;
using Relax.Impl.Commands;
using Relax.Impl.Json;
using Relax.Impl.Serialization;
using Symbiote.Core;

namespace Relax.Tests.Commands.GettingDocuments
{
    public class when_getting_all_pocos_by_ids_timed : with_get_all_pocos_by_ids
    {
        protected static CommandResult result;
        protected static ViewResult<MyPoco> viewResult;
        protected static string json;
        protected static IProvideDocumentMetadata MetadataProvider;
        protected static Stopwatch watch;

        private Because of = () =>
            {
                MetadataProvider = Assimilate.GetInstanceOf<IProvideDocumentMetadata>();
                result = command.GetDocuments<TestDoc>(new object[] {"1","2"});

                watch = Stopwatch.StartNew();

                for( int i = 0; i < 10000; i++ )
                {
                    viewResult = command.DeserializeView<MyPoco>( result.Json );    
                }
                
                watch.Stop();
            };

        private It should_not_take_longer_than_2_seconds = () => watch.ElapsedMilliseconds.ShouldBeLessThan( 3000 );
    }
}