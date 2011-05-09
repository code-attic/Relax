using System;
using Machine.Specifications;

namespace Relax.Tests.Document
{
    public class with_custom_document_simple_key
    {
        protected static Person person;
        protected static DateTime dob = DateTime.Parse("04/01/1980");

        private Establish context = () => person = new Person()
                                                         {
                                                             FirstName = "Dude", 
                                                             LastName = "Duderson", 
                                                             Social = "000-11-1234",
                                                             DateOfBirth = dob
                                                         };
    }
}
