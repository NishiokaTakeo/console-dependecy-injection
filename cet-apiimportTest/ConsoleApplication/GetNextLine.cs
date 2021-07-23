using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NLog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace cet_apiimportTest
{
    public class GetNextLine
    {
		cet_apiimport.ConsoleApplication _app ;
        [SetUp]
        public void Setup()
        {
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    		ILogger<cet_apiimport.ConsoleApplication> logger = new Logger<cet_apiimport.ConsoleApplication>(new NullLoggerFactory());

			_app = new cet_apiimport.ConsoleApplication(logger, configuration);
        }


		[Test(Description = "GetNextLine")]
		[TestCase("21/07/2021 7:13:44 AM | http://10.40.1.20:8080/WebServices/utilService.asmx/GetCompany",@"21/07/2021 7:13:44 AM | DATA: """, Description = "it returns without logdata")]
		// [TestCase("21/07/2021 7:43:49 AM | http://10.40.1.20:8080/WebServices/StudentModules/personalService.asmx/GetConcessionCardImageList","21/07/2021 7:43:49 AM | DATA: "{\"StudentId\":\"201102\"}", Description = "It returns with logdata")]

		public void GivenOKhenReturns(string line1, string line2 )
		{
			
			// var expect = 1;

			// Act 
			var actual = _app.GetNextLine(line1,line2);

			
			// Assert
			// Assert.Greater(actual.DateCreated, System.DateTime.MinValue);
			Assert.AreEqual(System.DateTime.Parse("2021-07-21 19:13:44").ToString("yyyy-MM-dd hh:mm:ss"), actual.DateCreated.ToString("yyyy-MM-dd hh:mm:ss"));
			Assert.AreEqual(0,actual.StudentID);
			// Assert.Greater(actual.UserID, 0);
			Assert.AreEqual(actual.LogData, @"DATA: """);
		}


		[Test(Description = "GetNextLine")]
		[TestCase("21/07/2021 7:43:49 AM | http://10.40.1.20:8080/WebServices/StudentModules/personalService.asmx/GetConcessionCardImageList",@"21/07/2021 7:43:49 AM | DATA: {""StudentId"":""201102""}", Description = "It returns with logdata")]

		public void GivenOK2henReturns(string line1, string line2 )
		{
			
			// var expect = 1;

			// Act 
			var actual = _app.GetNextLine(line1,line2);

			
			// Assert
			Assert.AreEqual(System.DateTime.Parse("2021-07-21 19:43:49").ToString("yyyy-MM-dd hh:mm:ss"), actual.DateCreated.ToString("yyyy-MM-dd hh:mm:ss"));
			Assert.AreEqual(actual.StudentID, 201102);
			Assert.AreEqual(actual.LogData, @"DATA: {""StudentId"":""201102""}");
		}

    }
}