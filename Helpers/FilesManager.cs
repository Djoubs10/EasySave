using System.Collections.Generic;
using System.IO;

namespace EasySave.Helpers
{
    public class FilesManager
    {
        public static object _transferFile = new object();
        public static List<string> GetFilesList(string directory)
        {
            List<string> files = new List<string>();

            files.AddRange(Directory.GetFiles(directory));
            foreach(string dir in Directory.GetDirectories(directory))
                files.AddRange(GetFilesList(dir));

            return files;
        }

        public static void CopyFile(string source, string target)
        {
            lock (_transferFile)
            {
                string? dir = Path.GetDirectoryName(target);
                if (dir is null)
                    return;
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (!(File.Exists(target) && new FileInfo(target).IsReadOnly))
                    File.Copy(source, target, true);
            }
        }
    }
}
