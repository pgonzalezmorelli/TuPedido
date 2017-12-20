using Android.OS;
using System;
using System.IO;
using TuPedido.Helpers;

namespace TuPedido.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalPath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}