using System;
using System.IO;
using TuPedido.Helpers;

namespace TuPedido.iOS.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalPath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}