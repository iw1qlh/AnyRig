using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnyRigBase.Models;
using System;
using System.IO;

namespace AnyRigBase.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var files = Directory.EnumerateFiles("../../../../../Rigs/", "*.INI");
            foreach (var file in files)
            {
                Console.WriteLine(file);
                TRigCommands rc = new TRigCommands();
                rc.FromIni(file);
                Console.WriteLine(rc);
            }
        }
    }
}