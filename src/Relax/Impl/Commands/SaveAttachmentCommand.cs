using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Model;

namespace Relax.Impl.Commands
{
    public class SaveAttachmentCommand : BaseCouchCommand
    {
        public virtual CommandResult SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(model.GetDocumentId())
                    .Attachment(attachmentName)
                    .Revision(model.GetDocumentRevision());

                var result = new CommandResult(action.SaveAttachment(Uri, contentType, content));
                model.SetDocumentRevision(result.JsonObject["rev"].ToString());
                model.AddAttachment(attachmentName, contentType, content.LongLength);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                                "An exception occurred trying to save an attachment {0} to a document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}",
                                attachmentName,
                                typeof(TModel).FullName,
                                model.GetDocumentId(),
                                model.GetDocumentRevision(),
                                Uri.ToString(),
                                ex);
            }
        }

        public SaveAttachmentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}