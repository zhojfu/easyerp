namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Domain.Model;
    using Infrastructure.Utility;

    public abstract class TimesheetService<T, TS> : ITimesheetService<TS> where T : BaseEntity where TS : Statistic
    {
        protected IUnitOfWork unitOfWork;

        protected TimesheetService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Timesheet> GetTimesheetByDate(int page, int pageSize, DateTime selectedDate)
        {
            var categories = GetCategories(page, pageSize);

            if (categories == null)
            {
                return null;
            }

            var timesheets = new List<Timesheet>();

            foreach (var category in categories)
            {
                var timeSheets = GetTimesheetOfWeekByCategory(category.Id, selectedDate);

                var model = new Timesheet
                {
                    Id = category.Id,
                    DateOfWeek = selectedDate,
                    Title = category.Name
                };
                foreach (var timeSheet in timeSheets)
                {
                    switch (timeSheet.Date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            model.Mon = timeSheet.Value;
                            break;
                        case DayOfWeek.Tuesday:
                            model.Tue = timeSheet.Value;
                            break;
                        case DayOfWeek.Wednesday:
                            model.Wed = timeSheet.Value;
                            break;
                        case DayOfWeek.Thursday:
                            model.Thu = timeSheet.Value;
                            break;
                        case DayOfWeek.Friday:
                            model.Fri = timeSheet.Value;
                            break;
                        case DayOfWeek.Saturday:
                            model.Sat = timeSheet.Value;
                            break;
                        case DayOfWeek.Sunday:
                            model.Sun = timeSheet.Value;
                            break;
                    }
                }

                timesheets.Add(model);
            }
            return timesheets;
        }

        public void UpdateTimesheet(DateTime dayOfWeek, Timesheet timesheet)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(dayOfWeek);

            var valueOfWeek = new Dictionary<DateTime, double>
            {
                { dateRange.Item1, timesheet.Mon },
                { dateRange.Item1.AddDays(1), timesheet.Tue },
                { dateRange.Item1.AddDays(2), timesheet.Wed },
                { dateRange.Item1.AddDays(3), timesheet.Thu },
                { dateRange.Item1.AddDays(4), timesheet.Fri },
                { dateRange.Item1.AddDays(5), timesheet.Sat },
                { dateRange.Item1.AddDays(6), timesheet.Sun }
            };

            UpdateTimesheet(timesheet.Id, valueOfWeek);
        }

        protected abstract PagedResult<T> GetCategories(int page, int pageSize);
        protected abstract IEnumerable<TS> GetTimesheetOfWeekByCategory(int categoryId, DateTime dateOfWeek);
        protected abstract TS FindSpecificDataOfDateTime(int categoryId, DateTime date);
        protected abstract void UpdateDataOfTime(TS s);
        protected abstract void AddNewDataForTime(int categoryId, double value, DateTime date);

        protected void UpdateTimesheet(int categoryId, Dictionary<DateTime, double> valueOfWeek)
        {
            const double Tolerance = 0.001;

            foreach (var valueOfDay in valueOfWeek)
            {
                var data = valueOfDay.Value;
                var date = valueOfDay.Key;

                if (Math.Abs(data) < Tolerance)
                {
                    continue;
                }

                var dataOfTime = FindSpecificDataOfDateTime(categoryId, date);
                if (dataOfTime != null)
                {
                    if (Math.Abs(data - dataOfTime.Value) < Tolerance)
                    {
                        continue;
                    }
                    dataOfTime.Value = data;
                    UpdateDataOfTime(dataOfTime);
                }
                else
                {
                    AddNewDataForTime(categoryId, data, date);
                }
            }

            unitOfWork.Commit();
        }
    }
}