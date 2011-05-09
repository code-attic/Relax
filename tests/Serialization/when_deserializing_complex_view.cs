using System.Linq;
using Machine.Specifications;
using Relax.Impl.Json;
using Symbiote.Core.Serialization;

namespace Relax.Tests.Serialization
{
    public class when_deserializing_complex_view : with_complex_view_result
    {
        protected static ViewResult<ComplexDocument> documents;

        private Because of = () =>
                                 {
                                     documents = json.FromJson<ViewResult<ComplexDocument>>();
                                 };

        private It should_have_correct_revision = () => 
            documents.GetList().ToList()[0].DocumentRevision.ShouldEqual("1-39759d4fdfa5e162acec9551b741c82e");
    }
}