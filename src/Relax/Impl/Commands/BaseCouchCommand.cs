using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Symbiote.Core.Extensions;
using Symbiote.Core.Reflection;
using JsonExtensions = Symbiote.Core.Extensions.JsonExtensions;

namespace Relax.Impl
{
    public abstract class BaseCouchCommand
    {
        protected const string ID_MEMBER_NAME = "Id";
        protected const string REV_MEMBER_NAME = "Revision";
        protected const string IHANDLE_JSON_ID = "IHandleJsonDocumentId";
        protected const string IHANDLE_DOC_REV = "IHandleJsonDocumentRevision";

        protected virtual CouchUri uri { get; set; }
        protected virtual IHttpAction action { get; set; }
        protected ICouchConfiguration configuration { get; set; }

        public virtual CommandResult<TResult> Get<TResult>()
        {
            return new CommandResult<TResult>(action.Get(uri));
        }

        public virtual CommandResult<TResult> Delete<TResult>()
        {
            return new CommandResult<TResult>(action.Delete(uri));
        }

        public virtual CommandResult<TResult> Post<TResult>()
        {
            return new CommandResult<TResult>(action.Post(uri));
        }

        public virtual CommandResult<TResult> Post<TResult>(string body)
        {
            return new CommandResult<TResult>(action.Post(uri, body));
        }

        public virtual CommandResult<TResult> Put<TResult>()
        {
            return new CommandResult<TResult>(action.Put(uri));
        }

        public virtual CommandResult<TResult> Put<TResult>(string body)
        {
            return new CommandResult<TResult>(action.Put(uri, body));
        }

        public virtual string GetDocumentId(object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            if(doc != null)
            {
                return doc.GetIdAsJson();
            }
            else
            {
                return Reflector.ReadMember(instance, ID_MEMBER_NAME).ToJson(false);
            }
        }

        public virtual string GetDocumentRevision(object instance)
        {
            var doc = instance as IHandleJsonDocumentRevision;
            if(doc != null)
            {
                return doc.GetRevAsJson();
            }
            else
            {
                return Reflector.ReadMember(instance, REV_MEMBER_NAME).ToJson(false);
            }
        }

        public virtual void SetDocumentId(string json, object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            var response = json.FromJson<SaveResponse>();
            if(doc != null)
            {
                doc.UpdateKeyFromJson(response.Id);
            }
            else
            {
                var idType = Reflector.GetMemberType(instance.GetType(), ID_MEMBER_NAME);
                Reflector.WriteMember(instance, ID_MEMBER_NAME, response.Id.FromJson(idType));
            }
        }

        public virtual void SetDocumentRevision(string json, object instance)
        {
            var doc = instance as IHandleJsonDocumentRevision;
            var response = json.FromJson<SaveResponse>();
            if (doc != null)
            {
                doc.UpdateRevFromJson(response.Id);
            }
            else
            {
                var idType = Reflector.GetMemberType(instance.GetType(), REV_MEMBER_NAME);
                Reflector.WriteMember(instance, REV_MEMBER_NAME, response.Id.FromJson(idType));
            }
        }

        public virtual CouchUri CreateUri(string database)
        {
            var baseURI = configuration.Preauthorize ?
                                                         CouchUri.Build(
                                                             configuration.User,
                                                             configuration.Password,
                                                             configuration.Protocol,
                                                             configuration.Server,
                                                             configuration.Port,
                                                             database)
                              : CouchUri.Build(
                                  configuration.Protocol,
                                  configuration.Server,
                                  configuration.Port,
                                  database);
            return baseURI;
        }

        public virtual CouchUri CreateUri<TModel>()
        {
            var database = configuration.GetDatabaseNameForType<TModel>();
            return CreateUri(database);
        }

        public virtual RelaxException Exception(Exception ex, string format, params object[] args)
        {
            format.ToError<IDocumentRepository>(args);
            return new RelaxException(format.AsFormat(args), ex);
        }

        protected BaseCouchCommand(IHttpAction action, ICouchConfiguration configuration)
        {
            this.action = action;
            this.configuration = configuration;
        }
    }
}