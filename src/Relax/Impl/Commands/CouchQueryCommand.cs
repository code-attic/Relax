// /* 
// Copyright 2008-2011 Alex Robson
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// */
using System;
using System.Linq.Expressions;
using Relax.Config;

namespace Relax.Impl.Commands
{
    public class CouchQueryCommand
    {
        protected IDocumentSearchProvider searchProvider { get; set; }

        public object[] GetDocumentIdsForQuery<TModel>( Expression<Func<TModel, bool>> criteria )
        {
            if ( searchProvider == null )
            {
                throw new RelaxConfigurationException(
                    "No search provider has been configured for Couch. Please specify a search provider via the fluent configuration API." );
            }
            else
            {
                return searchProvider.GetDocumentIdsForQuery( criteria );
            }
        }

        public CouchQueryCommand( IDocumentSearchProvider searchProvider )
        {
            this.searchProvider = searchProvider;
        }
    }
}