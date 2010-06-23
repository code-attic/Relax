using Relax.Config;
using Relax.Impl.Commands;

namespace Relax.Impl.Repository
{
    public class DocumentRepository
        : BaseDocumentRepository
    {
        public DocumentRepository(ICouchConfiguration configuration) : base(configuration)
        {
        }
    }
}
