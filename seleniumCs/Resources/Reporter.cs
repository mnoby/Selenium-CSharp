//using AventStack.ExtentReports.Reporter;
//using AventStack.ExtentReports;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework.Interfaces;
//using seleniumCs.Utils;

//namespace seleniumCs.Resources
//{
//    public class Reporter
//    {
//        //private Guid uuid;
//        //public static ExtentReports extent;
//        //public static ExtentTest test;
//        public void StartReporter(string uid)
//        {
//            //string shortUID = uuid.ToString()[..4];
//            var dir = TestContext.CurrentContext.TestDirectory;
//            string actualPath = dir[..dir.LastIndexOf("bin")] + @"Reports\";
//            var fileName = this.GetType().ToString() + $"-{uid}" + ".html";
//            var htmlReporter = new ExtentSparkReporter(actualPath + fileName);


//            extent = new ExtentReports();
//            extent.AttachReporter(htmlReporter);
//        }

//        public void GenerateReport()
//        {
//            var status = TestContext.CurrentContext.Result.Outcome.Status;
//            var errorMessage = TestContext.CurrentContext.Result.Message;
//            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
//            ? ""
//            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
//            Status logstatus;

//            switch (status)
//            {
//                case TestStatus.Failed:
//                    logstatus = Status.Fail;
//                    //var screenShotPath = Capture("ScreenShotName");
//                    //test.Log(Status.Fail, stacktrace + errorMessage);
//                    //test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
//                    break;
//                case TestStatus.Inconclusive:
//                    logstatus = Status.Warning;
//                    break;
//                case TestStatus.Skipped:
//                    logstatus = Status.Skip;
//                    break;
//                default:
//                    logstatus = Status.Pass;
//                    break;
//            }
//            if (logstatus == Status.Fail)
//            {
//                //var screenShotPath = Capture(driver, "zz");
//                test.Log(logstatus, $"Test ended with: {logstatus}" + $"<br>Stack Trace: {stacktrace}" + $"<br>Error Message: {errorMessage}");
//            }
//            else
//            {
//                test.Log(logstatus, $"Test ended with: {logstatus} " + $"<br>Stack Trace: {stacktrace}");
//            }
//        }
//    }
//}
