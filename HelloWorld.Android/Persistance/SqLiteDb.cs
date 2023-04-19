using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelloWorld.Droid.Persistance;
using HelloWorld.Persistance;
using SQLite;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDb))]
namespace HelloWorld.Droid.Persistance
{
    public class SqLiteDb : ISqliteDb
    {
        public SQLiteConnection GetConnection()
        {
            var path = GetDatabasePath();

            return new SQLiteConnection(path);
        }

        private string GetDatabasePath()
        {
            var databaseName = "HelloWorld.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(documentPath, databaseName);
        }
    }
}