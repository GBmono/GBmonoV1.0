using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.CrawlerModel;

namespace Gbmono.Crawler.AdapterInterface
{
    public interface ICrawlerAdapter
    {
        /// <summary>
        /// Start a crawler with the specific crawler name
        /// </summary>
        /// <param name="crawlerName">Produced by system for the rule </param>
        /// <returns>True, start success; false, start failed</returns>
        bool StartCrawler(string crawlerName);

        /// <summary>
        /// Stop the specific crawler with the crawler name
        /// </summary>
        /// <param name="crawlerName">The specific website name to be crawl</param>
        /// <returns>True, stop success; false, stop failed</returns>
        bool StopCrawler(string crawlerName);

        /// <summary>
        /// Judge if the crawler of given name is running now.
        /// </summary>
        /// <param name="crawlerName">The specific crawler name</param>
        /// <returns>Yes, the crawler is running; No, the crawler is not running.</returns>
        bool IsCrawlerRunning(string crawlerName);

        /// <summary>
        /// Initialize a new crawler dictionary to use.
        /// </summary>
        /// <returns>The new crawler dictionary name</returns>
        string InitializeNewCrawler();

        /// <summary>
        /// Set the base info of the crawler, which includes the original websit, the storeRex string, the recipeUrlRex string.
        /// </summary>
        /// <param name="settings">The settings of the crawler info.</param>
        /// <returns>Yes, set the crawler successful; No, set the crawler failed</returns>
        bool SetCrawlInfo(SetUrl settings);

        /// <summary>
        /// Get all the crawl instance directory name list from the root directory.
        /// </summary>
        /// <returns>The list of crawl instance info list</returns>
        Task<IList<CrawlInstanceNameUrl>> GetAllCrawlInstanceName();

        /// <summary>
        /// Gets the file count under the directory async.
        /// </summary>
        /// <returns>The presistent file count.</returns>
        Task<int> GetAllPresistentFileCount();

        /// <summary>
        /// Save the crawl settings to the config files.
        /// </summary>
        /// <param name="filePath">The specific config file path to update</param>
        /// <param name="configuration">The website config infos</param>
        /// <returns></returns>
        Task<bool> SaveProcessConfiguration(string filePath, IList<ProcesserCondition> configuration);

        /// <summary>
        /// Get the crawl setting by the given groupId
        /// </summary>
        /// <param name="configFilePath">The specific file path of the config</param>
        /// <returns>The list of process condition</returns>
        Task<IList<ProcesserCondition>> GetProcessConfiguration(string configFilePath);


    
    }
}
