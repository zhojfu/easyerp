namespace Doamin.Service.Vendors
{
    using Doamin.Service.Events;
    using Domain.Model.Vendors;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;

    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class VendorService : IVendorService
    {
        public Vendor GetVendorById(int vendorId)
        {
            throw new NotImplementedException();
        }

        public void DeleteVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Vendor> GetAllVendors(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            throw new NotImplementedException();
        }

        public void InsertVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }

        public void UpdateVendor(Vendor vendor)
        {
            throw new NotImplementedException();
        }
    }
}