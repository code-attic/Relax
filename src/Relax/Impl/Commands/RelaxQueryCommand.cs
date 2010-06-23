using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Relax.Config;
using Symbiote.Core;
using Symbiote.Restfully;

namespace Relax.Impl.Commands
{
    public class RelaxQueryCommand
    {
        protected ICouchConfiguration configuration { get; set; }
        protected IRemoteProxy<IRelaxQueryService> queryService { get; set;}

        public object[] GetDocumentIdsForQuery<TModel>(Expression<Func<TModel,bool>> criteria)
        {
            var luceneQuery = ExpressionTreeProcessor.TranslateExpression(criteria);
            var database = configuration.GetDatabaseNameForType<TModel>();
            return queryService.Call(x => x.GetDocumentIdsForQuery(database, luceneQuery));
        }

        public RelaxQueryCommand(ICouchConfiguration configuration, IRemoteProxy<IRelaxQueryService> queryService)
        {
            this.configuration = configuration;
            this.queryService = queryService;
        }
    }
}
