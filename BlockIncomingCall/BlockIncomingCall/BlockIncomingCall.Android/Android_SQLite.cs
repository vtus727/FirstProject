using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BlockIncomingCall.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(Android_SQLite))]
namespace BlockIncomingCall.Droid
{
    public class Android_SQLite : ISQLite
    {
        public Android_SQLite()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var dbName = "Persons.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = System.IO.Path.Combine(dbPath, dbName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}