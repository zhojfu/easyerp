using System.Collections.Generic;
using Domain.Model.Stores;
using Infrastructure.Utility;

namespace Doamin.Service.Stores
{
    public  interface IPostRetailService
    {
        void AddRecord(PostRetail retail);
        void AddRecords(IEnumerable<PostRetail> retails);
        PagedResult<PostRetail> ShowPostRecord(int pageNumber, int pageSize);
    }
}
