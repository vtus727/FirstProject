using System;
using System.Collections.Generic;
using System.Text;

namespace BlockIncomingCall
{
    public class PersonView
    {
        public List<Person> Persons;

        public PersonView()
        {
            Persons = new List<Person>();

        }

        public List<Person> GetListPerson()
        {
            return Persons;
        }
    }
}
