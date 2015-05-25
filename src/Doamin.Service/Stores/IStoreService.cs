namespace Doamin.Service.Stores
{
    using System.Collections.Generic;
    using Domain.Model.Stores;

    public interface IStoreService
    {
        void DeleteStore(Store store);
        IList<Store> GetAllStores();
        Store GetStoreById(int storeId);
        IList<Store> GetStoresByProductId(int productId);
        void InsertStore(Store store);
        void UpdateStore(Store store);
    }
}