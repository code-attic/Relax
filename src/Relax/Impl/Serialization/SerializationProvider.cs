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
using System.IO;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Converters;
using Relax.Impl.Json;
using Relax.Impl.Model;
using Symbiote.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Relax.Impl.Serialization 
{
    public class SerializationProvider :
        ISerializationProvider
    {
        public IContractResolver PocoContractResolver {get; set; }
        public IKeyAccessor KeyAccessor { get; set; }
        public IProvideDocumentMetadata MetadataProvider { get; set; }
        public JsonSerializer DocumentSerializer { get; set; }
        public JsonSerializer PocoSerializer { get; set; }
        public JsonSerializer MetadataSerialzier { get; set; }

        public void Serialize<T>( T instance, TextWriter writer )
        {
            if( !typeof( T ).IsInstanceOfType(( typeof( BaseDocument ) )) )
            {
                PocoSerializer.Serialize( writer, instance );
            }
            else
            {
                DocumentSerializer.Serialize( writer, instance );
            }
        }

        public T Deserialize<T>( string json )
        {
            var type = typeof(T);
            object document = null;
            using ( var reader = new StringReader( json ) )
                document = DocumentSerializer.Deserialize( reader, type );

            using ( var reader = new StringReader( json ) )
                if ( !type.IsInstanceOfType( typeof( BaseDocument ) ) )
                {
                    var key = KeyAccessor.GetId( document, type );
                    var metadata = (DocumentMetadata) MetadataSerialzier.Deserialize( reader, typeof( DocumentMetadata ) );
                    MetadataProvider.SetMetadata( key, metadata );
                }
            return (T) document;
        }

        public ViewResult<T> DeserializeList<T>( string json )
        {
            var type = typeof( T );
            var viewType = typeof( ViewResult<T> );
            ViewResult<T> viewResult = null;
            using ( var reader = new StringReader( json ) )
                viewResult = (ViewResult<T>) DocumentSerializer.Deserialize( reader, viewType );

            using ( var reader = new StringReader( json ) )
                if ( !type.IsInstanceOfType( typeof( BaseDocument ) ) )
                {
                    var metadataView = 
                        (ViewResult<DocumentMetadata>)
                        MetadataSerialzier.Deserialize( reader, typeof( ViewResult<DocumentMetadata> ) );

                    for( int i = 0; i < viewResult.Rows.Length; i++ )
                    {
                        var metadata = metadataView.Rows[i];
                        var document = viewResult.Rows[i];
                        var key = KeyAccessor.GetId( document.Model, type );
                        MetadataProvider.SetMetadata( key, metadata.Model );
                    }
                }
            return viewResult;
        }

        public SerializationProvider( DocumentContractResolver contractResolver, IKeyAccessor keyAccessor, IProvideDocumentMetadata metadataProvider )
        {
            PocoContractResolver = contractResolver;
            KeyAccessor = keyAccessor;
            MetadataProvider = metadataProvider;
            DocumentSerializer = JsonSerializer.Create( GetDocumentSettings() );
            PocoSerializer = JsonSerializer.Create( GetPocoSettings() );
            MetadataSerialzier = JsonSerializer.Create( GetMetadataSettings() );
        }

        public JsonSerializerSettings GetPocoSettings()
        {
            var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore,
                                   TypeNameHandling = TypeNameHandling.All,
                                   TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                                   ContractResolver = PocoContractResolver
                               };
            settings.Converters.Add( new IsoDateTimeConverter() );
            return settings;
        }

        public JsonSerializerSettings GetMetadataSettings()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None,
            };
            settings.Converters.Add(new IsoDateTimeConverter());
            return settings;
        }

        public JsonSerializerSettings GetDocumentSettings()
        {
            var settings = new JsonSerializerSettings
                               {
                                   NullValueHandling = NullValueHandling.Ignore,
                                   MissingMemberHandling = MissingMemberHandling.Ignore,
                                   TypeNameHandling = TypeNameHandling.All,
                                   TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                               };
            settings.Converters.Add( new IsoDateTimeConverter() );
            return settings;
        }
    }
}
