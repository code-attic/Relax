using System;
using Relax.Config;
using Relax.Impl.Http;
using Symbiote.Core.Extensions;

namespace Relax.Impl.Commands
{
    public class BaseCouchCommand
    {
        public virtual CouchUri Uri { get; protected set; }
        protected virtual IHttpAction action { get; set; }
        protected ICouchConfiguration configuration { get; set; }

        public virtual CommandResult Get()
        {
            return new CommandResult(action.Get(Uri));
        }

        public virtual CommandResult Delete()
        {
            return new CommandResult(action.Delete(Uri));
        }

        public virtual CommandResult Post()
        {
            return new CommandResult(action.Post(Uri));
        }

        public virtual CommandResult Post(string body)
        {
            return new CommandResult(action.Post(Uri, body));
        }
        
        public virtual CommandResult Put()
        {
            return new CommandResult(action.Put(Uri));
        }

        public virtual CommandResult Put(string body)
        {
            return new CommandResult(action.Put(Uri, body));
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