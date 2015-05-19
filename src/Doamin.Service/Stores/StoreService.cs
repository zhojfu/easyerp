namespace Doamin.Service.Stores
{
    using Domain.Model.Stores;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Store service
    /// </summary>
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> storeRepository;

        private readonly IUnitOfWork unitOfWork;

        public StoreService(IRepository<Store> storeRepository, IUnitOfWork unitOfWork)
        {
            this.storeRepository = storeRepository;
            this.unitOfWork = unitOfWork;
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
            return storeRepository.FindAll(i => i.Id > 0).ToList();
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
            return productId < 0 ? new List<Store>() : storeRepository.FindAll(i => i.Products.Any(p => p.Id == productId)).ToList();
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