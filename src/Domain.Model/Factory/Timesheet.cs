using System;

namespace Domain.Model.Factory
{
    public class Timesheet
    {
        public int Id { get; set; }
        public DateTime DateOfWeek { get; set; }
        public string Title { get; set; }
        public double Mon { get; set; }
        public double Tue { get; set; }
        public double Wed { get; set; }
        public double Thu { get; set; }
        public double Fri { get; set; }
        public double Sat { get; set; }
        public double Sun { get; set; }
    }
}
