using System.Collections.Generic;
using Relax.Impl;
using Relax.Impl.Model;

namespace Relax.ApplicationServices
{
    public class UserViews : DesignDocument
    {
        public UserViews()
        {
            DocumentId = @"design/users";

            Views = new Dictionary<string, DesignView>()
                        {
                            {"by_name_and_password", new DesignView()
                                                         {
                                                             Map = @"function(doc) { }"
                                                         }},
                        };
        }
    }
}