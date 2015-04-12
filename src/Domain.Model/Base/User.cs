namespace Domain.Model.Base
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System;

    public class User : BaseEntity
    {
        public DateTime LoginDate { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public long CompanyId { get; set; }

        public Company Company { get; set; }
    }
}