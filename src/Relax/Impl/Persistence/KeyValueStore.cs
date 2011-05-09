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
using Symbiote.Core.Persistence;

namespace Relax.Impl.Persistence
{
    public class KeyValueStore : IKeyValueStore
    {
        public IDocumentRepository Repository { get; set; }

        public bool Delete<T>( string key )
        {
            return Repository.DeleteDocument<T>( key );
        }

        public T Get<T>( string key )
        {
            return Repository.Get<T>( key );
        }

        public IEnumerable<T> GetAll<T>()
        {
            return Repository.GetAll<T>();
        }

        public bool Persist<T>( string key, T instance )
        {
            return Repository.Save( key, instance );
        }

        public KeyValueStore( IDocumentRepository repository )
        {
            Repository = repository;
        }
    }
}
