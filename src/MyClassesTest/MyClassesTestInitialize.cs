using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTest
{

    [TestClass]
    public class MyClassesTestInitialization
    {
        [AssemblyInitialize]
        public static void AssemblyInitialization(TestContext tc)
        {
            //TODO initialize for all test in the assembly
            tc.WriteLine("In AssemblyInitialize() methods");
            //create ann initialize database 
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            //cllean up database
        }
    }
}
