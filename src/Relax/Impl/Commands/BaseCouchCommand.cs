using System;
using Relax.Impl.Configuration;
using Relax.Impl.Http;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
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