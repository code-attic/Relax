using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbiote.Relax.Impl
{
    public interface ICouchCommandFactory
    {
        ICouchCommand GetCommand();
    }
}
