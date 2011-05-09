using System;
using Machine.Specifications;
using Symbiote.Core;
using Symbiote.Core.Serialization;

namespace Relax.Tests.Document
{
    public class when_working_with_simple_couch_document
    {
        protected static string newRev = "01";
        protected static Guid newId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        protected static SimpleDocument simpleDocument;

        private Establish context = () =>
                                        {
                                            Assimilate.Initialize();
                                            simpleDocument = new SimpleDocument() { Message = "test" };
                                        };

        private Because of = () =>
                                 {
                                     simpleDocument.UpdateKeyFromJson(newId.ToJson(false));
                                     simpleDocument.UpdateRevFromJson(newRev);
                                 };

        private It get_document_id_should_return_id = () =>
                                                      simpleDocument.GetDocumentId().ShouldEqual(simpleDocument.DocumentId);

        private It get_doc_id_as_json_should_return_id_to_string =
            () => simpleDocument.DocumentId.ToString().ShouldEqual(simpleDocument.GetDocumentIdAsJson());

        private It should_have_new_id = () => simpleDocument.DocumentId.ShouldEqual(newId.ToString());
        private It should_have_new_rev = () => simpleDocument.DocumentRevision.ShouldEqual(newRev);
    }
}