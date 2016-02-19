using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gbmono.IO
{
    public static class FileHelper
    {
        /// <summary>
        /// get files under folder withe extionsion names
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileExtensions"></param>
        /// <returns></returns>
        public static IList<FileInfo> GetFiles(string folderPath, string[] fileExtensions)
        {
            // create file directoryinfo class
            var directoryInfo = new DirectoryInfo(folderPath);

            var files = new List<FileInfo>();

            // load files by the file extensions
            foreach (var fileExtension in fileExtensions)
            {
                var targetFiles = directoryInfo.GetFiles(string.Format("*.{0}", fileExtension));
                files.AddRange(targetFiles.ToList());
            }

            return files;
        }


        /// <summary>
        /// move file from source to target 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void MoveFile(string source, string target)
        {
            // Ensure that the target does not exist. 
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            // move file to target path
            File.Move(source, target);
        }


        /// <summary>
        /// copy file from source to target 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyFile(string from, string to)
        {
            if (File.Exists(to))
            {
                // rename the existing file
                var rename = to + "." + Guid.NewGuid().ToString("N");
                File.Move(to, rename);
            }

            // copy file
            File.Copy(from, to);
        }

    }
}
