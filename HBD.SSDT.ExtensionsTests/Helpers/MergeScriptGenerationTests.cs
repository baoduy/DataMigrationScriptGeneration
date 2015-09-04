using HBD.Framework.Data;
using HBD.SSDT.Extensions.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBD.SSDT.ExtensionsTests.Helpers
{
    [TestClass()]
    public class MergeScriptGenerationTests
    {
        private const string ConnectionStringName = "Northwind";

        [TestMethod()]
        public void GenerateTest()
        {
            using (var merge = new MergeScriptGeneration(ConnectionStringName))
            {
                merge.Generate(MergeScriptOption.All, "dbo.Categories", "Customers", "dbo.[Employees]");
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length > 1);
            }
        }

        [TestMethod()]
        public void GenerateAllTest()
        {
            using (var merge = new MergeScriptGeneration(ConnectionStringName))
            {
                //All
                merge.OutputFolder = "Output/AllOption";
                merge.GenerateAll(MergeScriptOption.All);
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length>1);

                //Insert Only
                merge.OutputFolder = "Output/Insert";
                merge.GenerateAll(MergeScriptOption.Insert);
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length > 1);

                //Update Only
                merge.OutputFolder = "Output/Update";
                merge.GenerateAll(MergeScriptOption.Update);
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length > 1);

                //Update Only
                merge.OutputFolder = "Output/Delete";
                merge.GenerateAll(MergeScriptOption.Delete);
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length > 1);

                //Update Only
                merge.OutputFolder = "Output/Default";
                merge.GenerateAll();
                Assert.IsTrue(System.IO.Directory.GetFiles(merge.OutputFolder).Length > 1);
            }
        }
    }
}