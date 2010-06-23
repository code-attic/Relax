using System;
using Relax.Config;
using StructureMap;

namespace Relax.Impl.Commands
{
    public class CouchCommandFactory 
    {
        protected ICouchConfiguration configuration { get; set; }

        protected TCommand CreateCommand<TCommand>()
        {
            return ObjectFactory.GetInstance<TCommand>();
        }

        public DeleteAttachmentCommand CreateDeleteAttachmentCommand()
        {
            return CreateCommand<DeleteAttachmentCommand>();
        }

        public GetAttachmentCommand CreateGetAttachmentCommand()
        {
            return CreateCommand<GetAttachmentCommand>();
        }

        public SaveAttachmentCommand CreateSaveAttachmentCommand()
        {
            return CreateCommand<SaveAttachmentCommand>();
        }

        public DeleteDocumentCommand CreateDeleteCommand()
        {
            return CreateCommand<DeleteDocumentCommand>();
        }

        public GetDocumentCommand CreateGetDocumentCommand()
        {
            return CreateCommand<GetDocumentCommand>();
        }

        public GetAllDocumentsCommand CreateGetAllDocumentsCommand()
        {
            return CreateCommand<GetAllDocumentsCommand>();
        }

        public GetDocumentsPagedCommand CreateGetDocumentsPagedCommand()
        {
            return CreateCommand<GetDocumentsPagedCommand>();
        }

        public GetDocumentsByIdsCommand CreateGetDocumentsByIdsCommand()
        {
            return CreateCommand<GetDocumentsByIdsCommand>();
        }

        public GetDocumentsInRangeCommand CreateGetDocumentsInRangeCommand()
        {
            return CreateCommand<GetDocumentsInRangeCommand>();
        }

        public GetFromViewCommand CreateGetFromViewCommand()
        {
            return CreateCommand<GetFromViewCommand>();
        }

        public RelaxQueryCommand CreateQueryCommand()
        {
            return CreateCommand<RelaxQueryCommand>();
        }

        public ISaveDocument CreateSaveDocumentCommand()
        {
            return configuration.BreakDownDocumentGraphs ? (ISaveDocument)
                        CreateSaveDocumentGraphCommand()
                       : CreateSaveDocumentOnlyCommand();
        }

        public ISaveDocuments CreateSaveDocumentsCommand()
        {
            return configuration.BreakDownDocumentGraphs
                       ? (ISaveDocuments)
                         CreateSaveDocumentOnlyListCommand()
                       : CreateSaveDocumentGraphListCommand();
        }

        public SaveDocumentCommand CreateSaveDocumentOnlyCommand()
        {
            return CreateCommand<SaveDocumentCommand>();
        }

        public SaveDocumentGraphCommand CreateSaveDocumentGraphCommand()
        {
            return CreateCommand<SaveDocumentGraphCommand>();
        }

        public SaveDocumentListCommand CreateSaveDocumentOnlyListCommand()
        {
            return CreateCommand<SaveDocumentListCommand>();
        }

        public SaveDocumentGraphListCommand CreateSaveDocumentGraphListCommand()
        {
            return CreateCommand<SaveDocumentGraphListCommand>();
        }

        public ServerCommand CreateServerCommand()
        {
            return CreateCommand<ServerCommand>();
        }

        public ChangeStreamCommand CreateStreamCommand()
        {
            return CreateCommand<ChangeStreamCommand>();
        }

        public CouchCommandFactory(ICouchConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}