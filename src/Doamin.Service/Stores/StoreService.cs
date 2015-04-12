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
    public partial class StoreService : IStoreService
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
            this.storeRepository.Remove(store);
            this.unitOfWork.Commit();
        }

        public IList<Store> GetAllStores()
        {
            return this.storeRepository.FindAll(i => i.Id > 0).ToList();
        }

        public Store GetStoreById(int storeId)
        {
            if (storeId < 0)
            {
                throw new ArgumentException("storeId");
            }
            return this.storeRepository.GetByKey(storeId);
        }

        public void InsertStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            this.storeRepository.Add(store);
            this.unitOfWork.Commit();
        }

        public void UpdateStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            this.storeRepository.Update(store);
            this.unitOfWork.Commit();
        }
    }
}