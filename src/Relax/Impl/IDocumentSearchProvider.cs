using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Relax.Impl
{
    public interface IDocumentSearchProvider
    {
        object[] GetDocumentIdsForQuery<TModel>(Expression<Func<TModel, bool>> criteria);
    }
}
