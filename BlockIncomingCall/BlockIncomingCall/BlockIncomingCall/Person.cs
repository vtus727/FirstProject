using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockIncomingCall
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string Phone { get; set; }
        public Person()
        {

        }
    }
}
