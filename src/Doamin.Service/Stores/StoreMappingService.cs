namespace Doamin.Service.Stores
{
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Store mapping service
    /// </summary>
    public partial class StoreMappingService : IStoreMappingService
    {
        public void DeleteStoreMapping(StoreMapping storeMapping)
        {
            throw new NotImplementedException();
        }

        public StoreMapping GetStoreMappingById(int storeMappingId)
        {
            throw new NotImplementedException();
        }

        public IList<StoreMapping> GetStoreMappings<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            throw new NotImplementedException();
        }

        public void InsertStoreMapping(StoreMapping storeMapping)
        {
            throw new NotImplementedException();
        }

        public void InsertStoreMapping<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            throw new NotImplementedException();
        }

        public void UpdateStoreMapping(StoreMapping storeMapping)
        {
            throw new NotImplementedException();
        }

        public int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            throw new NotImplementedException();
        }

        public bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            throw new NotImplementedException();
        }

        public bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            throw new NotImplementedException();
        }
    }
}