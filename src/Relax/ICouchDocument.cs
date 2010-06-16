using System.Collections.Generic;
using Relax.Impl;
using Relax.Impl.Model;

namespace Relax
{
    public interface ICouchDocument<TKey>
        : IHaveDocumentId, IHaveDocumentRevision
    {
        TKey DocumentId { get; set; }
        IEnumerable<string> Attachments { get; }
    }
}