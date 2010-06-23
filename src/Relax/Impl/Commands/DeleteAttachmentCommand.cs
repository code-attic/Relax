using System;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Model;

namespace Relax.Impl.Commands
{
    public class DeleteAttachmentCommand : BaseCouchCommand
    {
        public virtual CommandResult DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(model.GetDocumentIdAsJson())
                    .Attachment(attachmentName)
                    .Revision(model.GetDocumentRevision());

                var result = Delete();
                model.SetDocumentRevision(result.JsonObject.Value<string>("rev"));
                model.RemoveAttachment(attachmentName);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                    "An exception occurred trying to delete attachment {0} from document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}",
                    attachmentName,
                    typeof(TModel).FullName,
                    model.GetDocumentIdAsJson(),
                    model.GetDocumentRevision(),
                    Uri.ToString(),
                    ex);
            }
        }

        public DeleteAttachmentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}