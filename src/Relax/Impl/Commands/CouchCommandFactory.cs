using StructureMap;

namespace Relax.Impl.Commands
{
    public class CouchCommandFactory 
    {
        protected TCommand GetCommand<TCommand>()
        {
            return ObjectFactory.GetInstance<TCommand>();
        }

        public DeleteAttachmentCommand GetDeleteAttachmentCommand()
        {
            return GetCommand<DeleteAttachmentCommand>();
        }

        public GetAttachmentCommand GetGetAttachmentCommand()
        {
            return GetCommand<GetAttachmentCommand>();
        }

        public SaveAttachmentCommand GetSaveAttachmentCommand()
        {
            return GetCommand<SaveAttachmentCommand>();
        }

        public DeleteDocumentCommand GetDeleteCommand()
        {
            return GetCommand<DeleteDocumentCommand>();
        }

        public GetDocumentCommand GetGetDocumentCommand()
        {
            return GetCommand<GetDocumentCommand>();
        }
        
        public GetFromViewCommand GetViewCommand()
        {
            return GetCommand<GetFromViewCommand>();
        }

        public RelaxQueryCommand GetQueryCommand()
        {
            return GetCommand<RelaxQueryCommand>();
        }

        public SaveDocumentCommand GetSaveCommand()
        {
            return GetCommand<SaveDocumentCommand>();
        }

        public ServerCommand GetServerCommand()
        {
            return GetCommand<ServerCommand>();
        }

        public ChangeStreamCommand GetStreamCommand()
        {
            return GetCommand<ChangeStreamCommand>();
        }

        public CouchCommandFactory()
        {
        }
    }
}