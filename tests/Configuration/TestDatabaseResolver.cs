using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Relax.Tests.Configuration
{
    public class TestDatabaseResolver : IResolveDatabaseNames
    {
        public string GetDatabaseNameFor<TModel>()
        {
            if (typeof(TModel).Equals(typeof(object)))
                return null;    
            return typeof (TModel).Name.ToLower();
        }
    }
}
