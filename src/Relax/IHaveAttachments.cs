namespace Relax
{
    public interface IHaveAttachments
    {
        void AddAttachment(string attachmentName, string contentType, long contentLength);
        void RemoveAttachment(string attachmentName);
    }
}