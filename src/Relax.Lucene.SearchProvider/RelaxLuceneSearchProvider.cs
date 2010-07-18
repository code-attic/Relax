using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Relax.Config;
using Relax.Impl;
using Symbiote.JsonRpc.Client;

namespace Relax.Lucene.SearchProvider
{
    public class RelaxLuceneSearchProvider : IDocumentSearchProvider
    {
        protected IRemoteProxy<IRelaxQueryService> remoteProxy { get; set; }
        protected ICouchConfiguration couchConfiguration { get; set; }

        public object[] GetDocumentIdsForQuery<TModel>(Expression<Func<TModel, bool>> criteria)
        {
            var databaseName = couchConfiguration.GetDatabaseNameForType<TModel>();
            var luceneQuery = ExpressionTreeProcessor.TranslateExpression(criteria);
            return remoteProxy.Call(x => x.GetDocumentIdsForQuery(databaseName, luceneQuery));
        }

        public RelaxLuceneSearchProvider(ICouchConfiguration configuration, IRemoteProxy<IRelaxQueryService> remoteProxy)
        {
            this.remoteProxy = remoteProxy;
            this.couchConfiguration = configuration;
        }
    }
}
