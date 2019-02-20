using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BlockIncomingCall
{
    public class PersonDatabase
    {
        private SQLiteConnection conn;

        //CREATE
        public PersonDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Person>();
        }



        //READ  
        public IEnumerable<Person> GetMembers()
        {
            var members = (from mem in conn.Table<Person>() select mem);
            return members.ToList();
        }
        //INSERT  
        public string AddMember(Person member)
        {
            conn.Insert(member);
            return "success";
        }
        //DELETE  
        public string DeleteMember(int id)
        {
            conn.Delete<Person>(id);
            return "success";
        }
    }
}
