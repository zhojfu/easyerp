namespace Application.Test
{
    using System;
    using Infrastructure.Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestDateUtils
    {
        [TestMethod]
        public void Test_GetWeekRangeOfCurrentDate()
        {
            var now = new DateTime(2015, 3, 22);
            var startTime = new DateTime(2015, 3, 16);
            var endTime = new DateTime(2015, 3, 22);
            var range = DateHelper.GetWeekRangeOfCurrentDate(now);
            Assert.IsTrue(startTime.Equals(range.Item1));
            Assert.IsTrue(endTime.Equals(range.Item2));
        }
    }
}