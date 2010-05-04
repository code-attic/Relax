
namespace Relax.Tests.Document
{
    public class Driver : CouchDocument<Driver, Person, string>
    {
        public virtual Person Person { get; set; }
        public virtual string LicenseNumber { get; set; }

        protected void Init()
        {
            KeyGetter(x => x.Person);
            KeySetter((x,k) => x.Person = k);
        }

        public Driver(Person person)
        {
            Person = person;
            Init();
        }
    }
}