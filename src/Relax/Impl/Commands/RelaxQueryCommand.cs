using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Relax.Config;
using Symbiote.Core;

namespace Relax.Impl.Commands
{
    public class RelaxQueryCommand
    {
        protected IDocumentSearchProvider searchProvider { get; set; }

        public object[] GetDocumentIdsForQuery<TModel>(Expression<Func<TModel,bool>> criteria)
        {
            if(searchProvider == null)
            {
                throw new RelaxConfigurationException(
                    "No search provider has been configured for Relax. Please specify a search provider via the fluent configuration API.");
            }
            else
            {
                return searchProvider.GetDocumentIdsForQuery(criteria);
            }
        }

        public RelaxQueryCommand(IDocumentSearchProvider searchProvider)
        {
            this.searchProvider = searchProvider;
        }
    }
}
