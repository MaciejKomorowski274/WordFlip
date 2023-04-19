using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelloWorld.Droid.Services;
using HelloWorld.Services;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace HelloWorld.Droid.Services
{
    public class FileService : IFileService
    {
        public async Task WriteTextAsync(string name, string content)
        {
            var basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filePath = System.IO.Path.Combine(basePath, name);
            using (var writer = File.CreateText(filePath))
            {
                await writer.WriteAsync(content);
            }
        }
    }
}