using System;


namespace Gbmono.Api.Service
{
    public interface IDataCollector
    {
        void Save(string userId, int productId, short actionType, DateTime dateTime);
    }
}
