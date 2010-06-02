using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Relax.Impl.Configuration;
using Relax.Impl.Http;

namespace Relax.Impl.Commands
{
    public class ChangeStreamCommand : BaseCouchCommand
    {
        public IHttpAction BeginStreaming<TModel>(int since, Action<ChangeRecord> onUpdate, AsyncCallback streamInterrupted)
        {
            Action<CouchUri, int, Action<ChangeRecord>> proxy = action.GetContinuousResponse;
            proxy.BeginInvoke(CreateUri<TModel>(), since, onUpdate, streamInterrupted, null);
            return action;
        }

        public IHttpAction BeginStreaming(string database, int since, Action<ChangeRecord> onUpdate, AsyncCallback streamInterrupted)
        {
            Action<CouchUri, int, Action<ChangeRecord>> proxy = action.GetContinuousResponse;
            proxy.BeginInvoke(CreateUri(database), since, onUpdate, streamInterrupted, null);
            return action;
        }

        public ChangeStreamCommand(IHttpAction action, ICouchConfiguration configuration) : base(action, configuration)
        {
        }
    }
}
