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
using Relax.Impl.Commands;
using Relax.Impl.Http;

namespace Relax.Impl
{
    public class CouchDbServer : ICouchServer
    {
        protected IDocumentRepository repository { get; set; }
        protected CouchCommandFactory commandFactory { get; set; }

        public void CleanViews<TModel>()
        {
            var command = commandFactory.CreateServerCommand();
            command.CleanViews<TModel>();
        }

        public void CompactDatabase<TModel>()
        {
            var command = commandFactory.CreateServerCommand();
            command.CompactDatabase<TModel>();
        }

        public void CompactView<TModel>( string testview )
        {
            var command = commandFactory.CreateServerCommand();
            command.CompactView<TModel>( testview );
        }

        public void CopyDatabase<TModel>( CouchUri targetUri )
        {
            var command = commandFactory.CreateServerCommand();
            command.CopyDatabase<TModel>( targetUri );
        }

        public void CopyDatabase( CouchUri sourceUri, CouchUri targetUri )
        {
            var command = commandFactory.CreateServerCommand();
            command.CopyDatabase( sourceUri, targetUri );
        }

        public void CreateDatabase<TModel>()
        {
            var command = commandFactory.CreateServerCommand();
            command.CreateDatabase<TModel>();
        }

        public IList<string> DatabaseList
        {
            get
            {
                var command = commandFactory.CreateServerCommand();
                return command.GetDatabaseList();
            }
        }

        public bool DatabaseExists<TModel>()
        {
            var command = commandFactory.CreateServerCommand();
            return command.DatabaseExists<TModel>();
        }

        public virtual void DeleteDatabase<TModel>()
        {
            var command = commandFactory.CreateServerCommand();
            command.DeleteDatabase<TModel>();
        }

        public void Replicate<TModel>( CouchUri targetUri )
        {
            var command = commandFactory.CreateServerCommand();
            command.Replicate<TModel>( targetUri );
        }

        public void Replicate( CouchUri sourceUri, CouchUri targetUri )
        {
            var command = commandFactory.CreateServerCommand();
            command.Replicate( sourceUri, targetUri );
        }

        public IDocumentRepository Repository
        {
            get { return repository; }
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public CouchDbServer( IDocumentRepository repository, CouchCommandFactory commandFactory )
        {
            this.commandFactory = commandFactory;
            this.repository = repository;
        }
    }
}