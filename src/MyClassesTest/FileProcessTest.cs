using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    

    [TestClass]
    public class FileProcessTest : TestBase
    {
        private const string BAD_FILE_NAME = "/Users/FileProcess.cs";

        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            //TODO initialize for all test in class
            tc.WriteLine("In ClassInitialize() methods");
            
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            //cllean up for all test in class
        }

        [TestInitialize]
        public void TestInitialize()
        {
            //TODO initialize for every test in class
            TestContext.WriteLine("In TestInitialize() methods");

        }

        [TestCleanup]
        public  void TestCleanup()
        {
            //cllean up for all test in class
        }

       

        [TestMethod]
        [Description("Check for file name exist")]
        [Owner("jukhan")]
        [Priority(1)]
        [Ignore]
        public void FileNameDoesExist()
        {
            var fileProcess = new FileProcess();
            bool fromCall;

            TestContext.WriteLine("Checking file FileProcess.cs");

            fromCall = fileProcess.FilerExist("/Users/jukhan/Downloads/Aspnetmicroservices/src/MyClasses/FileProcess.cs");
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(1000)]
        public void TimeoutExample()
        {
            Thread.Sleep(800);
        }

            [TestMethod]
        public void FileNameDoesNotExist()
        {
            var fileProcess = new FileProcess();
            bool fromCall;

            fromCall = fileProcess.FilerExist(BAD_FILE_NAME);
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullorEmpty_UsingAttribute()
        {
            var fileProcess = new FileProcess();

            fileProcess.FilerExist("");
           
        }

        [TestMethod]
        public void FileNameNullorEmpty_UsingTryCatch()
        {
            var fileProcess = new FileProcess();
            try
            {
                fileProcess.FilerExist("");
            }
            catch (ArgumentNullException)
            {
                //test was passed
                return;
            }

            Assert.Fail("Call to FileExist() did not throw ArgumentNullException.");
        }

        //DATA-ROW

        [TestMethod]
        [DataRow(1, 1, DisplayName = "First Test (1,1)")]
        [DataRow(12, 12, DisplayName = "Second Test (11,11)")]
        public void AreNumberEqual(int num1, int num2)
        {
            Assert.AreEqual(num1, num2);
        }


        [TestMethod]
        [DeploymentItem("FileToDeploy.txt")]
        [DataRow("FileToDeploy.txt", DisplayName ="Deployment item")]
        public void FileNameUsingDataRow(string fileName) {
            var fileProcess = new FileProcess();
            bool fromCall;
            fileName = TestContext.DeploymentDirectory + "/" + fileName;

            fromCall = fileProcess.FilerExist(fileName);
            Assert.IsTrue(fromCall);

           
        }


    }
}
