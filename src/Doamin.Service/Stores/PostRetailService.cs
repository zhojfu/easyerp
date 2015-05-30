using System;
using System.Collections.Generic;
using Domain.Model.Stores;
using Infrastructure.Domain;
using Infrastructure.Utility;

namespace Doamin.Service.Stores
{
    public class PostRetailService : IPostRetailService
    {
        private readonly IRepository<PostRetail> retailRepository;

        private readonly IUnitOfWork unitOfWork;

        public PostRetailService(IRepository<PostRetail> retailRepository, IUnitOfWork unitOfWork)
        {
            this.retailRepository = retailRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddRecord(PostRetail retail)
        {
            this.retailRepository.Add(retail);
            this.unitOfWork.Commit();
        }

        public void AddRecords(IEnumerable<PostRetail> retails)
        {
            foreach (var retail in retails) 
            {
                AddRecord(retail);
            }
        }

        public PagedResult<PostRetail> ShowPostRecord(int pageNumber, int pageSize)
        {
            return retailRepository.FindAll(pageSize, pageNumber, e => true, m => m.Name, SortOrder.Ascending);
        }
    }
}
