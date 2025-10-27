using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CoPilodMD.Core
{
    public static class StorageHandler
    {
        public const string StorageDir = "Storage";
        public static void SaveDcmFile(Stream stream, string fileId)
        {
            var dir = Path.Combine(StorageDir, fileId);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string fullPath = Path.Combine(dir, $"{fileId}.dcm");

            using FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            {
                stream.CopyTo(fs);
            }
        }

        public static string GetDir(string fileId)
        {
            return Path.Combine(StorageDir, fileId);
        }

        public static string GetFileName(string fileId)
        {
            return Path.Combine(GetDir(fileId), $"{fileId}.dcm");
        }
    }
}
