using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Test
{
    using Doamin.Service.Factory;

    [TestClass]
    public class TestDateUtils
    {
        [TestMethod]
        public void Test_GetWeekRangeOfCurrentDate()
        {
            DateTime now = new DateTime(2015, 3, 22);
            DateTime startTime = new DateTime(2015, 3, 16);
            DateTime endTime = new DateTime(2015, 3, 22);
            Tuple<DateTime, DateTime> range = DateUtils.GetWeekRangeOfCurrentDate(now);
            Assert.IsTrue(startTime.Equals(range.Item1));
            Assert.IsTrue(endTime.Equals(range.Item2));
        }
    }
}
