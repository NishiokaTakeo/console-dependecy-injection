using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NLog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace cet_apiimportTest
{
    public class GetUserID
    {
		cet_apiimport.ConsoleApplication _app ;
        [SetUp]
        public void Setup()
        {
			var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    		ILogger<cet_apiimport.ConsoleApplication> logger = new Logger<cet_apiimport.ConsoleApplication>(new NullLoggerFactory());

			_app = new cet_apiimport.ConsoleApplication(logger, configuration);
        }


		[Test(Description = "")]
		[TestCase("API_1_21072021_3.log", Description = "Returns UserID")]
		[TestCase(@"C:\Dev\cet-apiimport\API_1_21072021_3.log", Description = "Returns UserID")]
		public void GivenPathThenReturnID(string path)
		{
			
			var expect = 1;

			// Act 
			var actual = _app.GetUserID(path);

			// Assert
			Assert.AreEqual(expect, actual);
		}

    }
}