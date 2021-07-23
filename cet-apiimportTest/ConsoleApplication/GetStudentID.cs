using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NLog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json.Linq;

namespace cet_apiimportTest
{
    public class GetStudentID
    {
		cet_apiimport.ConsoleApplication _app ;
        [SetUp]
        public void Setup()
        {
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    		ILogger<cet_apiimport.ConsoleApplication> logger = new Logger<cet_apiimport.ConsoleApplication>(new NullLoggerFactory());

			_app = new cet_apiimport.ConsoleApplication(logger, configuration);
        }


		[Test(Description = "GetStudentID")]
		[TestCase("StudentId", Description = "It returns id")]
		[TestCase("uniqueStudentId", Description = "It returns id")]
		[TestCase("studentId", Description = "It returns id")]
		[TestCase("studentid", Description = "It returns id")]
		[TestCase("uniquestudentid", Description = "It returns id")]

		public void GivenOKhenReturns(string key)
		{
			// Arrange
			var json = new JObject();
			json.Add(key,1);

			// Act 
			var actual = _app.GetStudentID(json);

			
			// Assert
			Assert.AreEqual(1, actual);




		}


    }
}