namespace Relax.Impl
{
    public class DocumentRepository
        : BaseDocumentRepository
    {
        public DocumentRepository(ICouchConfiguration configuration, ICouchCommandFactory commandFactory) 
            : base(configuration, commandFactory)
        {
        }

        public DocumentRepository(string configurationName) 
            : base(configurationName)
        {
        }
    }
}
