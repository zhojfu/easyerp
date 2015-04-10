namespace Application.Test
{
    using Domain.EntityFramework;
    using Domain.Model;
    using Infrastructure.Domain;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for DoubleTestCURD
    /// </summary>
    [TestClass]
    public class TestDoublesCURD
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Test_DoubleTest_InsertToDatabase()
        {
            var configuration = new EntitySetupConfiguration();

            // const string connectionString = "Data Source=(local); Database=easyERP_test; User ID=sa; Password=; MultipleActiveResultSets=True";
            // EntityFrameworkDbContext dbContext = new EntityFrameworkDbContext(connectionString, configuration);
            // DbConnection dbConnection = new SqlConnection();
            var dbContext = new EntityFrameworkDbContext("easyERP_test");

            IUnitOfWork unitOfWork = new EntityFrameworkUnitOfWork(dbContext);
            var testRepository = new EntityFrameworkRepository<TestDoubles>(dbContext, unitOfWork);
            var testModel = new TestDoubles
            {
                Name = "test description"
            };
            testRepository.Add(testModel);
            unitOfWork.Commit();
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

        #endregion Additional test attributes
    }
}