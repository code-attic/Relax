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
using System.Collections.Generic;
using Relax.Config;
using Relax.Impl.Cache;
using Relax.Impl.Serialization;

namespace Relax.Impl.Repository
{
    public class CachedDocumentRepository
        : BaseDocumentRepository
    {
        protected ICacheKeyBuilder _builder;
        protected ICouchCacheProvider _cache;
        protected IDocumentRepository _repository;

        public override bool DeleteDocument<TModel>( object id, string rev )
        {
            return _cache.Delete<TModel>( id, rev, base.DeleteDocument<TModel> );
        }

        public override bool DeleteDocument<TModel>( object id )
        {
            return _cache.Delete<TModel>( id, base.DeleteDocument<TModel> );
        }

        public override TModel Get<TModel>( object id, string revision )
        {
            return _cache.Get( id, revision, base.Get<TModel> );
        }

        public override TModel Get<TModel>( object id )
        {
            return _cache.Get( id, base.Get<TModel> );
        }

        public override IList<TModel> GetAll<TModel>()
        {
            return _cache.GetAll( base.GetAll<TModel> );
        }

        public override IList<TModel> GetAll<TModel>( int pageSize, int pageNumber )
        {
            return _cache.GetAllPaged( pageNumber, pageSize, base.GetAll<TModel> );
        }

        public override IList<TModel> GetAllBetweenKeys<TModel>( object startingWith, object endingWith )
        {
            return _cache.GetAllInRange( startingWith, endingWith, base.GetAllBetweenKeys<TModel> );
        }

        public override bool Save<TModel>( TModel model )
        {
            return _cache.Save( model, base.Save );
        }

        public override bool SaveAll<TModel>( IEnumerable<TModel> list )
        {
            return _cache.SaveAll( list, base.SaveAll );
        }

        public CachedDocumentRepository( ICouchCacheProvider cacheProvider, ICouchConfiguration configuration, ISerializationProvider serializer )
            : base( configuration, serializer )
        {
            _cache = cacheProvider;
        }
    }
}