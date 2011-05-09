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
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Relax.Impl.Model;
using Symbiote.Core;

namespace Relax.Impl.Serialization
{
    public class DocumentContractResolver : DefaultContractResolver
    {
        public static MetadataValueProviderCache ValueProviderCache { get; set; }
        public static readonly Type DocumentMetadataType = typeof( DocumentMetadata );

        protected override List<System.Reflection.MemberInfo> GetSerializableMembers(Type objectType) 
        {
            var properties = base.GetSerializableMembers(objectType);
            var adds = base.GetSerializableMembers( typeof ( DocumentMetadata ) );
            return properties.Concat( adds ).ToList();
        }

        protected override IValueProvider CreateMemberValueProvider(System.Reflection.MemberInfo member) 
        {
            return member.DeclaringType.Equals( DocumentMetadataType ) 
                       ? ValueProviderCache.GetValueProviderFor( member.Name ) as IValueProvider
                       : new ReflectionValueProvider( member );
        }

        public DocumentContractResolver() : base( true )
        {
            ValueProviderCache = Assimilate.GetInstanceOf<MetadataValueProviderCache>();
        }
    }
}