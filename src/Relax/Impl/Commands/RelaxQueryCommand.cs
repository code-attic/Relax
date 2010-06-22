using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Symbiote.Core;
using Symbiote.Restfully;

namespace Relax.Impl.Commands
{
    public class RelaxQueryCommand
    {
        protected IRemoteProxy<IRelaxQueryService> queryService { get; set;}

        public object[] GetDocumentIdsForQuery<TModel>(Expression<Func<TModel,bool>> criteria)
        {
            var luceneQuery = ExpressionTreeProcessor.TranslateExpression(criteria);
            var database = UtilityExtensions.GetDatabaseForType<TModel>();
            return queryService.Call(x => x.GetDocumentIdsForQuery(database, luceneQuery));
        }

        public RelaxQueryCommand(IRemoteProxy<IRelaxQueryService> queryService)
        {
            this.queryService = queryService;
        }
    }
}
