using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Relax.Impl.Json;
using Relax.Impl.Model;
using StructureMap;
using Symbiote.Core.Extensions;
using Symbiote.Core.Reflection;

namespace Relax.Impl.Commands
{
    public class CouchUtility
    {
        protected ICouchConfiguration configuration { get; set; }

        public virtual string GetDocumentId(object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            if (doc != null)
            {
                return doc.GetIdAsJson();
            }
            else
            {
                return Reflector.ReadMember(instance, configuration.Conventions.IdPropertyName).ToJson(false);
            }
        }

        public virtual string GetDocumentRevision(object instance)
        {
            var doc = instance as IHandleJsonDocumentRevision;
            if (doc != null)
            {
                return doc.GetRevAsJson();
            }
            else
            {
                return Reflector.ReadMember(instance, configuration.Conventions.RevisionPropertyName).ToJson(false);
            }
        }

        public virtual void SetDocumentId(string json, object instance)
        {
            var doc = instance as IHandleJsonDocumentId;
            var response = json.FromJson<SaveResponse>();
            if (doc != null)
            {
                doc.UpdateKeyFromJson(response.Id);
            }
            else
            {
                var idType = Reflector.GetMemberType(instance.GetType(), configuration.Conventions.IdPropertyName);
                Reflector.WriteMember(instance, configuration.Conventions.IdPropertyName, response.Id.FromJson(idType));
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
                var idType = Reflector.GetMemberType(instance.GetType(), configuration.Conventions.RevisionPropertyName);
                Reflector.WriteMember(instance, configuration.Conventions.RevisionPropertyName, response.Id.FromJson(idType));
            }
        }

        public CouchUtility(ICouchConfiguration couchConfiguration)
        {
            configuration = couchConfiguration;
        }
    }

    public static class UtilityExtensions
    {
        private static CouchUtility _utility;
        private static CouchUtility Utility
        {
            get
            {
                _utility = _utility ?? ObjectFactory.GetInstance<CouchUtility>();
                return _utility;
            }
        }

        public static string GetDocumentId(this object instance)
        {
            return Utility.GetDocumentId(instance);
        }

        public static string GetDocumentRevision(this object instance)
        {
            return Utility.GetDocumentRevision(instance);
        }

        public static void SetDocumentId(this object instance, string json)
        {
            Utility.SetDocumentId(json, instance);
        }

        public static void SetDocumentRevision(this object instance, string json)
        {
            Utility.SetDocumentRevision(json, instance);
        }
    }

    public abstract class BaseCouchCommand
    {
        public virtual CouchUri Uri { get; protected set; }
        protected virtual IHttpAction action { get; set; }
        protected ICouchConfiguration configuration { get; set; }

        public virtual CommandResult<TResult> Get<TResult>()
        {
            return new CommandResult<TResult>(action.Get(Uri));
        }

        public virtual CommandResult<TResult> Delete<TResult>()
        {
            return new CommandResult<TResult>(action.Delete(Uri));
        }

        public virtual void Delete()
        {
            action.Delete(Uri);
        }

        public virtual CommandResult<TResult> Post<TResult>()
        {
            return new CommandResult<TResult>(action.Post(Uri));
        }

        public virtual CommandResult<TResult> Post<TResult>(string body)
        {
            return new CommandResult<TResult>(action.Post(Uri, body));
        }

        public virtual void Post()
        {
            action.Post(Uri);
        }

        public virtual void Post(string body)
        {
            action.Post(Uri, body);
        }

        public virtual CommandResult<TResult> Put<TResult>()
        {
            return new CommandResult<TResult>(action.Put(Uri));
        }

        public virtual CommandResult<TResult> Put<TResult>(string body)
        {
            return new CommandResult<TResult>(action.Put(Uri, body));
        }

        public virtual void Put()
        {
            action.Put(Uri);
        }

        public virtual void Put(string body)
        {
            action.Put(Uri, body);
        }

        public virtual CouchUri CreateUri(string database)
        {
            Uri = configuration.NewUri(database);
            return Uri;
        }

        public virtual CouchUri CreateUri()
        {
            Uri = configuration.NewUri();
            return Uri;
        }

        public virtual CouchUri CreateUri<TModel>()
        {
            Uri = configuration.NewUri<TModel>();
            return Uri;
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