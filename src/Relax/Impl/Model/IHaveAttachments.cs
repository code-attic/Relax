﻿namespace Relax.Impl
{
    public interface IHaveAttachments
    {
        void AddAttachment(string attachmentName, string contentType, long contentLength);
        void RemoveAttachment(string attachmentName);
    }
}