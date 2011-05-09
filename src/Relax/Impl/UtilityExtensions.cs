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
using Relax.Config;
using Relax.Impl.Http;
using Symbiote.Core;

namespace Relax.Impl
{
    public static class UtilityExtensions
    {
        private static CouchUtility _couchUtility;
        private static DocumentUtility _documentUtility;

        private static CouchUtility CouchUtility
        {
            get
            {
                _couchUtility = _couchUtility ?? Assimilate.GetInstanceOf<CouchUtility>();
                return _couchUtility;
            }
        }

        private static DocumentUtility DocumentUtility
        {
            get
            {
                _documentUtility = _documentUtility ?? Assimilate.GetInstanceOf<DocumentUtility>();
                return _documentUtility;
            }
        }

        public static string GetDocumentIdAsJson( this object instance )
        {
            return DocumentUtility.GetDocumentIdAsJson( instance );
        }

        public static object GetDocumentId( this object instance )
        {
            return DocumentUtility.GetDocumentId( instance );
        }

        public static string GetDocumentRevision( this object instance )
        {
            return DocumentUtility.GetDocumentRevision( instance );
        }

        public static void SetDocumentRevision( this object instance, string revision )
        {
            DocumentUtility.SetDocumentRevision( revision, instance );
        }

        public static CouchUri NewUri( this ICouchConfiguration configuration )
        {
            return CouchUtility.NewUri();
        }

        public static CouchUri NewUri<TModel>( this ICouchConfiguration configuration )
        {
            return CouchUtility.NewUri<TModel>();
        }

        public static CouchUri NewUri( this ICouchConfiguration configuration, string database )
        {
            return CouchUtility.NewUri( database );
        }
    }
}