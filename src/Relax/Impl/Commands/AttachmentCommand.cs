using System;
using Symbiote.Core.Extensions;

namespace Relax.Impl
{
    public class AttachmentCommand : BaseCouchCommand
    {
        public CommandResult<SaveResponse> DeleteAttachment<TModel>(TModel model, string attachmentName)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(GetDocumentId(model))
                    .Attachment(attachmentName)
                    .Revision(GetDocumentRevision(model));

                var result = Delete<SaveResponse>();
                SetDocumentId(result.Json, model);
                SetDocumentRevision(result.Json, model);
            
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(ex, 
                    "An exception occurred trying to delete attachment {0} from document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}",
                    attachmentName,
                    typeof(TModel).FullName,
                    GetDocumentId(model),
                    GetDocumentRevision(model),
                    uri.ToString());
            }
        }

        public Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(id)
                    .Attachment(attachmentName);

                return action.GetAttachment(uri);
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                                "An exception occurred trying to retrieve an attachment {0} from a document of type {1} with id {2} at {3}. \r\n\t {4}",
                                attachmentName,
                                typeof (TModel).FullName,
                                id.ToString(),
                                uri.ToString());
            }
        }

        public CommandResult<SaveResponse> SaveAttachment<TModel>(TModel model, string attachmentName, string contentType, byte[] content)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(GetDocumentId(model))
                    .Attachment(attachmentName)
                    .Revision(GetDocumentRevision(model));

                var result = new CommandResult<SaveResponse>(action.SaveAttachment(uri, contentType, content));
                SetDocumentId(result.Json, model);
                SetDocumentRevision(result.Json, model);
                model.AddAttachment(attachmentName, contentType, content.LongLength);
                return result;
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                                "An exception occurred trying to save an attachment {0} to a document of type {1} with id {2} and rev {3} at {4}. \r\n\t {5}",
                                attachmentName,
                                typeof (TModel).FullName,
                                GetDocumentId(model),
                                GetDocumentRevision(model),
                                uri.ToString());
            }
        }

        public AttachmentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}