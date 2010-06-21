using Machine.Specifications;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Serialization
{
    public class when_deserializing_complex_document : with_complex_document
    {
        protected static ComplexDocument document;

        private Because of = () =>
                                 {
                                     document = json.FromJson<ComplexDocument>();
                                 };

        private It should_have_correct_revision = () => 
                                                  document.DocumentRevision.ShouldEqual("1-39759d4fdfa5e162acec9551b741c82e");
    }
}