namespace Domain.Model
{
    using Domain.Model.Factory;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Employee : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdNumber { get; set; }

        public bool Male { get; set; }

        public DateTime Birth { get; set; }

        public string Race { get; set; }

        public bool Married { get; set; }

        public string NativePlace { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string CellPhone { get; set; }

        public string EduBackground { get; set; }

        public string Zip { get; set; }

        public string Photo { get; set; }

        public long Department { get; set; }

        public double SalaryOfMonth { get; set; }

        public virtual ICollection<WorkTimeStatistic> WorkTimeRecords { get; set; }
    }
}