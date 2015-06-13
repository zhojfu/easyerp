using EasyErp.Core;

namespace Doamin.Service.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Stores;
    using Infrastructure.Domain;

    /// <summary>
    /// Store service
    /// </summary>
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> storeRepository;

        private readonly IUnitOfWork unitOfWork;
        private readonly IWorkContext workContext;

        public StoreService(IRepository<Store> storeRepository, IUnitOfWork unitOfWork, IWorkContext workContext)
        {
            this.storeRepository = storeRepository;
            this.unitOfWork = unitOfWork;
            this.workContext = workContext;
        }

        public void DeleteStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            storeRepository.Remove(store);
            unitOfWork.Commit();
        }

        public IList<Store> GetAllStores()
        {
            var stores =  storeRepository.FindAll(i => i.Id > 0).ToList();
            if (workContext.CurrentUser.IsAdmin)
            {
                return stores;
            }

            return stores.Where(s => s.Id == workContext.CurrentUser.StoreId).ToList();
        }

        public Store GetStoreById(int storeId)
        {
            if (storeId < 0)
            {
                throw new ArgumentException("storeId");
            }
            return storeRepository.GetByKey(storeId);
        }

        public IList<Store> GetStoresByProductId(int productId)
        {
            return productId < 0
                       ? new List<Store>()
                       : storeRepository.FindAll(i => i.ProductInventories.Any(p => p.Id == productId)).ToList();
        }

        public void InsertStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            storeRepository.Add(store);
            unitOfWork.Commit();
        }

        public void UpdateStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            storeRepository.Update(store);
            unitOfWork.Commit();
        }
    }
}