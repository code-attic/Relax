using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Relax
{
    public abstract class BaseDocument : IHaveAttachments
    {
        [JsonProperty("_attachments")]
        private JObject attachments { get; set; }

        [JsonProperty(PropertyName = "$doc_type")]
        protected virtual string UnderlyingDocumentType
        {
            get
            {
                return GetType().Name;
            }
            set
            {
                //do nothing, this is effectively read only in the model
            }
        }

        [JsonProperty("$doc_relatedIds")]
        protected virtual Dictionary<string, object[]> RelatedDocumentIds { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<string> Attachments
        {
            get
            {
                return attachments.Root.Children().Select(x => (x as JProperty).Name);
            }
        }

        public virtual void AddAttachment(string attachmentName, string contentType, long contentLength)
        {
            var attachment = new
            {
                Stub = true,
                ContentType = contentType,
                ContentLength = contentLength
            };

            if (!attachments.Properties().Any(x => x.Name == attachmentName))
            {
                var jsonStub = new JProperty(attachmentName, JToken.FromObject(attachment));
                attachments.Add(jsonStub);
            }
            else
            {
                attachments.Property(attachmentName).Value = JToken.FromObject(attachment);
            }
        }

        public virtual void RemoveAttachment(string attachmentName)
        {
            if (attachments.Properties().Any(x => x.Name == attachmentName))
            {
                attachments.Remove(attachmentName);
            }
        }

        protected BaseDocument()
        {
            attachments = JObject.FromObject(new object());
            RelatedDocumentIds = new Dictionary<string, object[]>();
        }
    }
}