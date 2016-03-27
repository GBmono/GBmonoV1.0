using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerModel;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Management;



namespace Gbmono.Crawler.AdapterInterface
{
    public class CrawlerAdapter : ICrawlerAdapter
    {
        private string InstanceFolder = ConfigurationManager.AppSettings[CrawlerConfiguration.CrawlerFolder];

        private string CrawlExecuteNameWithExtension = ConfigurationManager.AppSettings[CrawlerConfiguration.CrawlExecuteNameWithExtension];

        private string excuteName = ConfigurationManager.AppSettings[CrawlerConfiguration.CrawlExecuteName];

        log4net.ILog log = log4net.LogManager.GetLogger("logger-name");

        public async Task<IList<CrawlInstanceNameUrl>> GetAllCrawlInstanceName()
        {
            try
            {
                //List<CIHashCodeAndProcessedCount> hashAndCoun = GetPersistentCount();

                //string instanceForderPath = System.Web.Hosting.HostingEnvironment.MapPath(InstanceFolder + "/");
                string instanceForderPath = InstanceFolder;
                if (!Directory.Exists(instanceForderPath))
                {
                    return null;
                }

                DirectoryInfo aDir = new DirectoryInfo(instanceForderPath);
                DirectoryInfo[] dirs = aDir.GetDirectories();
                List<CrawlInstanceNameUrl> result = new List<CrawlInstanceNameUrl>();

                //foreach (var directoryInfo in dirs)
                Parallel.ForEach(dirs, (directoryInfo) =>
                {
                    var crawlInstance = new CrawlInstanceNameUrl();
                    var fullPath = directoryInfo.FullName;
                    var originalWebSiteTxt = fullPath + "\\OriginalWebSite.txt";
                    var url = "";
                    int bloggerUserId = 0;
                    if (File.Exists(originalWebSiteTxt))
                    {
                        using (StreamReader reader = new StreamReader(originalWebSiteTxt, Encoding.UTF8))
                        {
                            var serializedText = JsonConvert.DeserializeObject<OriginalWebSiteTxtInner>(reader.ReadToEnd());
                            url = serializedText.OriginalWebSite;
                            bloggerUserId = serializedText.ThridPartyUserId;
                        }
                    }
                    crawlInstance.BloggerUserId = bloggerUserId;
                    var uri = new Uri(url);
                    var groupId = uri.GetHashCode();
                    //if (hashAndCoun != null)
                    //{
                    //    var tryToMap = hashAndCoun.Where(m => m.CrawlHashCode == groupId);
                    //    if (tryToMap != null && tryToMap.Any())
                    //    {
                    //        crawlInstance.PersistentCount = tryToMap.FirstOrDefault().Count;
                    //    }
                    //    else
                    //    {
                    //        crawlInstance.PersistentCount = 0;
                    //    }
                    //}
                    //else
                    //{
                    //    crawlInstance.PersistentCount = 0;
                    //}

                    //using (var ncrawlEntity = new NCrawlerEntities())
                    //{
                    //    var crawlHistoryCount = ncrawlEntity.CrawlHistory.Count(m => m.GroupId == groupId);
                    //    crawlInstance.CrawlHistoryCount = crawlHistoryCount;
                    //}
                    crawlInstance.CrawlHistoryCount = 0;

                    crawlInstance.InstanceName = directoryInfo.Name;
                    crawlInstance.WebSiteUrl = url;
                    crawlInstance.GroupId = groupId;
                    result.Add(crawlInstance);
                });

                //string[] result = dirs.Select(m => m.Name).ToArray();
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        public async Task<int> GetAllPresistentFileCount()
        {
            return await Task.Run(() =>
            {
                var listPresistent = GetPersistentCount();

                try
                {
                    if (listPresistent != null && listPresistent.Any())
                    {
                        return listPresistent.Select(m => m.Count).Aggregate((m1, m2) => (m1 + m2));
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<IList<ProcesserCondition>> GetProcessConfiguration(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                return null;
            }

            return await Task.Run(() =>
            {
                using (StreamReader reader = new StreamReader(configFilePath, Encoding.UTF8))
                {
                    var configText = JsonConvert.DeserializeObject<List<ProcesserCondition>>(reader.ReadToEnd());
                    return configText;
                }
            });
        }



        public string InitializeNewCrawler()
        {
            var instancePath = "~/CrawlerInstanceResource";
            var newInstanceName = "CrawlerInstance" + DateTime.Now.ToString("yyyymmddhhmmss");
            string instanceExcutePath = System.Web.Hosting.HostingEnvironment.MapPath(instancePath);
            var path = String.Format(InstanceFolder + "/{0}/", newInstanceName);
            var newInstanceFolderPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            CopyDirectory(instanceExcutePath, newInstanceFolderPath);

            return newInstanceName;
        }

        public bool IsCrawlerRunning(string crawlerName)
        {
            var instancePath = GetInstancePath(crawlerName);
            string instanceExcutePath = System.Web.Hosting.HostingEnvironment.MapPath(instancePath) + CrawlExecuteNameWithExtension;


            if (!IsProcessOpen(excuteName, instanceExcutePath))
            {
                return true;
            }
            else
            {
                //Still Runing
                return false;
            }
        }

        public async Task<bool> SaveProcessConfiguration(string filePath, IList<ProcesserCondition> configuration)
        {
            return await Task.Run(() =>
            {
                File.WriteAllText(filePath, JsonConvert.SerializeObject(configuration));
                return true;
            });
        }

        public bool SetCrawlInfo(SetUrl settings)
        {
            var instancePath = GetInstancePath(settings.InstanceName);
            string instanceExcutePath = System.Web.Hosting.HostingEnvironment.MapPath(instancePath) + CrawlExecuteNameWithExtension;

            var recipeRegex = settings.RecipeRegex == null ? "empty" : settings.RecipeRegex;
            var storeRegex = settings.StoreRegex == null ? "empty" : settings.StoreRegex;
            var blockRegex = settings.BlockRegex == null ? "empty" : settings.BlockRegex;

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(instanceExcutePath, settings.Url + " " + settings.ThirdPartyRecipeUserId + " " + recipeRegex + " " + storeRegex + " " + blockRegex + " " + settings.CrawlDepth);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = info;

            if (!IsProcessOpen(excuteName, instanceExcutePath))
            {
                p.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StartCrawler(string crawlerName)
        {
            var instancePath = GetInstancePath(crawlerName);
            var excuteFileName = CrawlExecuteNameWithExtension;
            string instanceExcutePath = System.Web.Hosting.HostingEnvironment.MapPath(instancePath) + excuteFileName;

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(instanceExcutePath);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = info;


            if (!IsProcessOpen(excuteName, instanceExcutePath))
            {
                p.Start();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StopCrawler(string crawlerName)
        {
            var instancePath = GetInstancePath(crawlerName);
            var instanceExcutePath = System.Web.Hosting.HostingEnvironment.MapPath(instancePath) + CrawlExecuteNameWithExtension;
            var excuteName = ConfigurationManager.AppSettings[CrawlerConfiguration.CrawlExecuteName];
            if (KillProcessOpen(excuteName, instanceExcutePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region PrivateMethods
        public bool IsProcessOpen(string name, string fileFullPath)
        {
            foreach (var clsProcess in System.Diagnostics.Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name) && GetMainModuleFilepath(clsProcess.Id) == fileFullPath)
                {
                    return true;
                }
            }
            return false;
        }

        public bool KillProcessOpen(string name, string fileFullPath)
        {
            try
            {
                var processes = System.Diagnostics.Process.GetProcessesByName(name);
                foreach (var process in processes)
                {
                    if (GetMainModuleFilepath(process.Id) == fileFullPath)
                    {
                        process.Kill();
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }

        }

        private static void CopyDirectory(string sourcePath, string destPath)
        {
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            foreach (string file in Directory.GetFiles(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(file));
                File.Copy(file, dest);
            }

            foreach (string folder in Directory.GetDirectories(sourcePath))
            {
                string dest = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, dest);
            }
        }

        private string GetMainModuleFilepath(int processId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
        }
        private List<CIHashCodeAndProcessedCount> GetAllProcessCount()
        {
            var processedResult = ConfigurationManager.AppSettings["ProcessedResult"];
            var directory = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["CrawlerData"]);
            if (!Directory.Exists(directory))
            {
                return null;
            }
            var dirInfo = new DirectoryInfo(directory);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            if (dirs != null && dirs.Any())
            {
                var returnList = new List<CIHashCodeAndProcessedCount>();
                foreach (var directoryInfo in dirs)
                {
                    var CIHashCodeCount = new CIHashCodeAndProcessedCount();
                    CIHashCodeCount.CrawlHashCode = int.Parse(directoryInfo.Name);
                    var processedResultDir = directoryInfo.FullName + "\\" + processedResult;
                    if (Directory.Exists(processedResultDir))
                    {
                        var processInfo = new DirectoryInfo(processedResultDir);
                        FileInfo[] files = processInfo.GetFiles();
                        CIHashCodeCount.Count = files.Count();
                    }
                    else
                    {
                        CIHashCodeCount.Count = 0;
                    }
                    returnList.Add(CIHashCodeCount);
                }
                return returnList;
            }
            else
            {
                return null;
            }
        }

        private List<CIHashCodeAndProcessedCount> GetPersistentCount()
        {
            //var directory = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["CrawlerData"]);
            var directory = ConfigurationManager.AppSettings[CrawlerConfiguration.FolderPath];
            if (!Directory.Exists(directory))
            {
                return null;
            }
            var dirInfo = new DirectoryInfo(directory);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            if (dirs != null && dirs.Any())
            {
                var returnList = new List<CIHashCodeAndProcessedCount>();
                foreach (var directoryInfo in dirs)
                {
                    var CIHashCodeCount = new CIHashCodeAndProcessedCount();
                    CIHashCodeCount.CrawlHashCode = int.Parse(directoryInfo.Name);
                    try
                    {
                        var directorys = directoryInfo.GetDirectories().Where(m => m.Name != CrawlerConfiguration.ConfigFolderName).OrderByDescending(m => int.Parse(m.Name));
                        var dirCount = directorys.Count();
                        if (dirCount == 0)
                        {
                            CIHashCodeCount.Count = 0;
                        }
                        else
                        {
                            var lastIndexFolder = directorys.First();
                            var maxDirIndex = int.Parse(lastIndexFolder.Name) - 1;
                            CIHashCodeCount.Count = lastIndexFolder.GetFiles().Count() + maxDirIndex * CrawlerConfiguration.FileCountInFolder;
                        }

                    }
                    catch (Exception)
                    {
                        CIHashCodeCount.Count = 0;
                    }
                    returnList.Add(CIHashCodeCount);
                }
                return returnList;
            }
            else
            {
                return null;
            }
        }

        private string GetInstancePath(string crawlerName)
        {
            return String.Format("{1}/{0}/", crawlerName, InstanceFolder);
        }

        #endregion


    }
}
