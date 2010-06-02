using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Relax.Tests.Serialization
{
    public class TestResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(JsonObjectContract contract)
        {
            List<MemberInfo> documentMembers =
                GetSubDocumentMembers(contract.UnderlyingType);

            List<MemberInfo> members =
                GetSerializableMembers(contract.UnderlyingType);

            if (members == null)
                throw new JsonSerializationException("Null collection of seralizable members returned.");

            JsonPropertyCollection properties = new JsonPropertyCollection(contract);

            foreach (MemberInfo member in members)
            {
                JsonProperty property = CreateProperty(contract, member);

                if (property != null)
                    properties.AddProperty(property);
            }

            return properties;
        }

        public List<MemberInfo> GetSubDocumentMembers(Type underlyingType)
        {
            var members = underlyingType
                .GetMembers(BindingFlags.Public | 
                            BindingFlags.NonPublic | 
                            BindingFlags.FlattenHierarchy |
                            BindingFlags.Instance);

            return members
                .Where(x => x.MemberType == MemberTypes.Property || x.MemberType == MemberTypes.Field)
                .Where(x =>
                           {
                               bool isDocument = false;
                               var memberType = GetMemberInfoType(x);
                               if (memberType == null)
                                   return isDocument;

                               if(memberType.GetInterface("IEnumerable`1") != null)
                               {
                                   var elementType = memberType.GetGenericArguments().FirstOrDefault();
                                   if(elementType != null)
                                       isDocument = elementType.GetInterface("ICouchDocument`2") != null;
                               }
                               else
                               {
                                   isDocument = memberType.GetInterface("ICouchDocument`2") != null;
                               }
                               return isDocument;
                           }).ToList();
        }

        public Type GetMemberInfoType(MemberInfo memberInfo)
        {
            try
            {
                if (memberInfo.MemberType == MemberTypes.Property)
                    return memberInfo.DeclaringType.GetProperty(memberInfo.Name).PropertyType;
                else
                    return memberInfo.DeclaringType.GetField(memberInfo.Name).FieldType;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}