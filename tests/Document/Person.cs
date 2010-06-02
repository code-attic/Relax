using System;
using Relax.Impl;
using Relax.Impl.Model;
using Symbiote.Core.Extensions;

namespace Relax.Tests.Document
{
    public class Person : ComplexCouchDocument<Person, string, string>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Social { get; set; }
        public virtual DateTime DateOfBirth { get; set; }

        protected void Init()
        {
            KeyGetter(x => "{0}, {1} born {2}".AsFormat(LastName, FirstName, DateOfBirth));
            KeySetter((x,k) => {});
        }

        public Person()
        {
            Init();
        }
    }
}