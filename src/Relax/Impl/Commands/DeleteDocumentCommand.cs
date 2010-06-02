using System;

namespace Relax.Impl
{
    public class DeleteDocumentCommand : BaseCouchCommand
    {
        public void DeleteDocument<TModel>(object id, object rev)
        {
            try
            {
                CreateUri<TModel>()
                    .IdAndRev(id, rev);

                action.Delete(uri);
            }
            catch (Exception ex)
            {
                throw Exception(ex,
                     "An exception occurred trying to delete a document of type {0} with id {1} at {2}. \r\n\t {3}",
                     typeof(TModel).FullName,
                     id,
                     uri);
            }
        }

        public void DeleteDocument<TModel>(TModel model)
        {
            object id = null;
            object rev = null;

            try
            {
                id = GetDocumentId(model);
                rev = GetDocumentRevision(model);

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
                     uri);
            }
        }

        public DeleteDocumentCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}