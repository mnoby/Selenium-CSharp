//using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Dapper;

namespace SeleniumProject.TestDataAccess
{
    class ExcelDataAccess
    {
        public static string TestDataFileConnection()
        {
            //var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];
            var baseDir = Environment.CurrentDirectory;
            var mainDir = baseDir[..baseDir.LastIndexOf("bin")] + "TestDataAccess";
            var fileName = "userdataTest.xlsx";
            var actualPath = Path.Combine(mainDir, fileName);
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", actualPath);
            return con;
        }

        public static UserData GetTestData(string keyName)
        {
            using var connection = new OleDbConnection(TestDataFileConnection());
            connection.Open();
            var query = string.Format("select * from [DataTest$] where key='{0}'", keyName);
            var value = connection.Query<UserData>(query).FirstOrDefault();
            connection.Close();
            return value;
        }
    }
}
