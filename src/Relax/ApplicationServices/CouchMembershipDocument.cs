using System.Web.Security;

namespace Relax.ApplicationServices
{
    public class CouchMembershipDocument : CouchDocument
    {
        public MembershipUser User { get; set; }

        public CouchMembershipDocument()
        {
        }

        public CouchMembershipDocument(MembershipUser user)
        {
            User = user;
        }
    }
}