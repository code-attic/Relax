using System.Collections.Generic;
using Relax.Impl;
using Relax.Impl.Model;

namespace Relax
{
    public interface ICouchDocument<TKey, TRev>
        : IHandleJsonDocumentId, IHandleJsonDocumentRevision
    {
        TKey DocumentId { get; set; }
        TRev DocumentRevision { get; set; }
        IEnumerable<string> Attachments { get; }
    }
}