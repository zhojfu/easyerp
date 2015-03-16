using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using Domain.EntityFramework;
using Domain.Model;
using Infrastructure.Domain;
using Infrastructure.Domain.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Test
{
    /// <summary>
    /// Summary description for DoubleTestCURD
    /// </summary>
    [TestClass]
    public class TestDoublesCURD
    {
        public TestDoublesCURD()
        {
            //Database database = new Database()
        }

        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Test_DoubleTest_InsertToDatabase()
        {
            EntitySetupConfiguration configuration = new EntitySetupConfiguration();

            // const string connectionString = "Data Source=(local); Database=easyERP_test; User ID=sa; Password=; MultipleActiveResultSets=True";
            // EntityFrameworkDbContext dbContext = new EntityFrameworkDbContext(connectionString, configuration);
            // DbConnection dbConnection = new SqlConnection();
            EntityFrameworkDbContext dbContext = new EntityFrameworkDbContext("easyERP_test", configuration);

            IUnitOfWork unitOfWork = new EntityFrameworkUnitOfWork(dbContext);
            EntityFrameworkRepository<TestDoubles> testRepository = new EntityFrameworkRepository<TestDoubles>(dbContext, unitOfWork);
            TestDoubles testModel = new TestDoubles
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Name = "test description"
            };
            testRepository.Add(testModel);
            unitOfWork.Commit();
        }
    }
}
