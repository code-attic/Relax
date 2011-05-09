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
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class ViewQuery
    {
        protected CouchUri _uri;

        public ViewQuery AllowStale()
        {
            _uri.StaleOk();
            return this;
        }

        public ViewQuery DescendingOrder()
        {
            _uri.Descending();
            return this;
        }

        public ViewQuery EndingWith<TKey>( TKey key )
        {
            _uri.EndKey( key );
            return this;
        }

        public ViewQuery Group()
        {
            _uri.Group();
            return this;
        }

        public ViewQuery Group( int groupLevel )
        {
            _uri.Group( groupLevel );
            return this;
        }

        public ViewQuery UseDocumentsInsteadOfValue()
        {
            _uri.IncludeDocuments();
            return this;
        }

        public ViewQuery LimitTo( int count )
        {
            _uri.Limit( count );
            return this;
        }

        public ViewQuery NoReduce()
        {
            _uri.NoReduce();
            return this;
        }

        public ViewQuery PagedBy( int pageNumber, int pageSize )
        {
            int number = (pageNumber - 1)*pageSize;
            _uri.Skip( number < 0 ? 0 : number ).Limit( pageSize );
            return this;
        }

        public ViewQuery Skip( int count )
        {
            _uri.Skip( count );
            return this;
        }

        public ViewQuery StartingWith<TKey>( TKey key )
        {
            _uri.StartKey( key );
            return this;
        }

        public ViewQuery WithKey<TKey>( TKey key )
        {
            _uri.Key( key );
            return this;
        }

        public ViewQuery WithKeyAndRevision<TKey, TRev>( TKey key, TRev revision )
        {
            _uri.KeyAndRev( key, revision );
            return this;
        }

        public ViewQuery( CouchUri uri )
        {
            _uri = uri;
        }
    }
}