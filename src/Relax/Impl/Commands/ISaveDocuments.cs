using System.Collections.Generic;

namespace Relax.Impl.Commands
{
    public interface ISaveDocuments
    {
        CommandResult SaveAll<TModel>(IEnumerable<TModel> models);
        CommandResult SaveAll(string database, IEnumerable<object> models);
    }
}