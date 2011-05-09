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
using Symbiote.Core.Extensions;
using Symbiote.Core.Serialization;

namespace Relax.Impl.Cache
{
    public class CacheKeyBuilder : ICacheKeyBuilder
    {
        public string GetKey<TModel>( object id )
        {
            return "{0}_{1}"
                .AsFormat( typeof( TModel ).FullName, GetIdAsValidString( id ) );
        }

        public string GetKey<TModel>( object id, string rev )
        {
            return "{0}_{1}_{2}"
                .AsFormat( typeof( TModel ).FullName, GetIdAsValidString( id ), rev );
        }

        public string GetRangeKey<TModel>( object startKey, object endKey )
        {
            return "{0}_{1}_{2}"
                .AsFormat( typeof( TModel ).FullName, GetIdAsValidString( startKey ), GetIdAsValidString( endKey ) );
        }

        public string GetListKey<TModel>()
        {
            return "{0}_list"
                .AsFormat( typeof( TModel ).FullName );
        }

        public string GetListKey<TModel>( int page, int size )
        {
            return "{0}_{1}_{2}"
                .AsFormat( typeof( TModel ).FullName, page, size );
        }

        public string GetIdAsValidString( object id )
        {
            var type = id.GetType();
            if (
                type.FullName.StartsWith( "System" ) &&
                (type.GetInterface( "IEnumerable" ) == null || type.Equals( typeof( string ) )) )
            {
                return id.ToString();
            }
            return id.ToJson( false );
        }
    }
}