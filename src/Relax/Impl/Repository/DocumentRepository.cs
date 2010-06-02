namespace Relax.Impl
{
    public class DocumentRepository
        : BaseDocumentRepository
    {
        public DocumentRepository(ICouchConfiguration configuration) 
            : base(configuration)
        {
        }

        public DocumentRepository(string configurationName) 
            : base(configurationName)
        {
        }
    }
}
