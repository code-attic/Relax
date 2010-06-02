using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class DeleteDocumentCommand : BaseCouchCommand
    {
        public void DeleteDocument<TModel>(object id, object rev)
        {
            try
            {
                CreateUri<TModel>()
                    .IdAndRev(id, rev);

                action.Delete(Uri);
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                     "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}",
                     typeof(TModel).FullName,
                     id,
                     Uri,
                     ex);
            }
        }

        public void DeleteDocument<TModel>(TModel model)
        {
            object id = null;
            object rev = null;

            try
            {
                id = model.GetDocumentId();
                rev = model.GetDocumentRevision();

                DeleteDocument<TModel>(id, rev);
            }
            catch(RelaxException rex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw Exception(ex, 
                     "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}",
                     typeof(TModel).FullName, 
                     id,
                     Uri,
                     ex);
            }
        }

        public DeleteDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}