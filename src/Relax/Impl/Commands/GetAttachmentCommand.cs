using System;
using Relax.Config;
using Relax.Impl.Http;
using Relax.Impl.Model;

namespace Relax.Impl.Commands
{
    public class GetAttachmentCommand : BaseCouchCommand
    {
        public virtual Tuple<string, byte[]> GetAttachment<TModel>(object id, string attachmentName)
            where TModel : IHaveAttachments
        {
            try
            {
                CreateUri<TModel>()
                    .Id(id)
                    .Attachment(attachmentName);

                return action.GetAttachment(Uri);
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                                "An exception occurred trying to retrieve an attachment {0} from a document of type {1} with id {2} at {3}. \r\n\t {4}",
                                attachmentName,
                                typeof(TModel).FullName,
                                id.ToString(),
                                Uri.ToString(),
                                ex);
            }
        }

        public GetAttachmentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}