using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerModel;

namespace Gbmono.CrawlerDB.Extensions
{
    public class FolderHelper
    {
        private static object LockObj = new object();
        private const int CountFile = CrawlerConfiguration.FileCountInFolder;
        string folderPath = ConfigurationManager.AppSettings[CrawlerConfiguration.FolderPath];
        public string GetFolderPathToStore(int groupId)
        {
            var folderIndexToStore = 0;
            string instanceGroupIdPath = folderPath + "/" + groupId;

            lock (LockObj)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                if (!Directory.Exists(instanceGroupIdPath))
                {
                    Directory.CreateDirectory(instanceGroupIdPath);
                    Directory.CreateDirectory(instanceGroupIdPath + "/1");
                    folderIndexToStore = 1;
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(instanceGroupIdPath);
                    var subDirs = dirInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly).Where(m => m.Name != CrawlerConfiguration.ConfigFolderName).OrderByDescending(m => int.Parse(m.Name)).FirstOrDefault();
                    if (subDirs != null)
                    {
                        var fileCount = subDirs.GetFiles().Count();

                        if (fileCount < CountFile
                            )
                        {
                            folderIndexToStore = int.Parse(subDirs.Name);
                        }
                        else
                        {
                            var foldId = int.Parse(subDirs.Name) + 1;
                            var newFolderPath = instanceGroupIdPath + "/" + foldId;
                            Directory.CreateDirectory(newFolderPath);
                            folderIndexToStore = foldId;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(instanceGroupIdPath + "/1");
                        folderIndexToStore = 1;
                    }
                }
            }
            return instanceGroupIdPath + "/" + folderIndexToStore;
        }
    }
}
