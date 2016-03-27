using System;
using System.Data;
using System.Linq;
using NCrawler;
using NCrawler.Extensions;
using NCrawler.Utils;

namespace Gbmono.CrawlerDB.DbServices
{
    public class DbCrawlQueueService : CrawlerQueueServiceBase
    {
        #region Readonly & Static Fields

        private readonly int m_GroupId;

        #endregion

        #region Constructors

        public DbCrawlQueueService(Uri baseUri, bool resume)
        {
            m_GroupId = baseUri.GetHashCode();
            if (!resume)
            {
                Clean();
            }
        }

        #endregion

        #region Instance Methods

        protected override long GetCount()
        {
            return AspectF.Define.
                Return<long, NCrawlerEntitiesDbServices>(e => e.CrawlQueue.Count(q => q.GroupId == m_GroupId));
        }

        protected override CrawlerQueueEntry PopImpl()
        {
            return AspectF.Define.
                Return<CrawlerQueueEntry, NCrawlerEntitiesDbServices>(e =>
                    {
                        CrawlQueue result = e.CrawlQueue.FirstOrDefault(q => q.GroupId == m_GroupId);
                        if (result.IsNull())
                        {
                            return null;
                        }

                        e.DeleteObject(result);
                        e.SaveChanges();
                        return result.SerializedData.FromBinary<CrawlerQueueEntry>();
                    });
        }

        protected override CrawlerQueueEntry PopWithExclusionImpl()
        {
            return AspectF.Define.
                Return<CrawlerQueueEntry, NCrawlerEntitiesDbServices>(e =>
                {
                    CrawlQueue result = e.ExecuteStoreQuery<CrawlQueue>(
                        //"SELECT TOP (1) * FROM [CrawlQueue] with (index(groupIdE)) WHERE ([GroupId] = {0})  AND ( [Exclusion]=0)", m_GroupId)
                        "SELECT TOP (1) * FROM [CrawlQueue] WHERE ([GroupId] = {0})  AND ( [Exclusion]=0)", m_GroupId)
                        .FirstOrDefault();

                    //CrawlQueue result = e.CrawlQueue.FirstOrDefault(q => q.GroupId == m_GroupId && q.Exclusion==false);
                    if (result.IsNull())
                    {
                        return null;
                    }
                    result.Exclusion = true;

                    e.ExecuteStoreCommand("update Crawlqueue set Exclusion=1 where Id={0}", result.Id);

                    //e.ObjectStateManager.ChangeObjectState(result, EntityState.Modified);
                    //e.SaveChanges();
                    return result.SerializedData.FromBinary<CrawlerQueueEntry>();
                });
        }

        protected override void PushImpl(CrawlerQueueEntry crawlerQueueEntry)
        {
            AspectF.Define.
                Do<NCrawlerEntitiesDbServices>(e =>
                {
                    string storeKey = Uri.UnescapeDataString(crawlerQueueEntry.CrawlStep.Uri.ToString());
                    e.AddToCrawlQueue(new CrawlQueue
                        {
                            GroupId = m_GroupId,
                            SerializedData = crawlerQueueEntry.ToBinary(),
                            Key = storeKey
                        });
                    e.SaveChanges();
                });
        }

        private void Clean()
        {
#if !DOTNET4
            using (NCrawlerEntitiesDbServices e = new NCrawlerEntitiesDbServices())
            {
                foreach (CrawlQueue queueObject in e.CrawlQueue.Where(q => q.GroupId == m_GroupId))
                {
                    e.DeleteObject(queueObject);
                }

                e.SaveChanges();
            }
#else
			AspectF.Define.
				Do<NCrawlerEntitiesDbServices>(e => e.ExecuteStoreCommand("DELETE FROM CrawlQueue WHERE GroupId = {0}", m_GroupId));
#endif
        }

        #endregion
    }
}