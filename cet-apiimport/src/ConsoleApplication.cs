// using Injector.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using cet_apiimport.Dao.Controllers;
using cet_apiimport.Dao.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

// using NLog;
namespace cet_apiimport
{
	public class ConsoleApplication
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
		private readonly ILogger<ConsoleApplication> _logger;
        public ConsoleApplication(ILogger<ConsoleApplication> logger,  Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
			_logger = logger;
        }

        public void Run()
        {
			_logger.LogTrace("Run!");
			// one file 
			foreach( var file in System.IO.Directory.GetFileSystemEntries("./importfrom"))
			{
				_logger.LogInformation("At "+ file);


				int i = 0;
				// User ID
				int id = GetUserID(file);
				
				var lines = System.IO.File.ReadAllLines(file);

				// datetime, url, studentid, data 
				while(i < lines.Length)
				{
					var line1 = lines[i];
					var line2 = lines[++i];

					try
					{

							var next = GetNextLine(line1, line2.Replace("\\",""));
							next.UserID = id;

							// insert data 
							using( var ctr = new APILogController(_configuration["DB"]))
							{
								ctr.Insert(next);
							}
					}
					catch(Exception ex)
					{
						_logger.LogError(ex,"line1: " + line1 + " | " + line2);
					}
					
					++i;
				}

				// foreach( var text in System.IO.File.ReadAllLines(file))
				// {



				// }
			}

        }

		public APILog GetNextLine(string line1, string line2)
		{
			var ans = new APILog();
			var firstParts = line1.Split("|");
			var secondParts = line2.Split("|");
			var datestring = firstParts[0];

			ans.DateCreated = System.DateTime.Parse(System.DateTime.Parse(datestring).ToString("yyyy-MM-dd HH:mm:ss"));
			ans.URL = firstParts[1];
			ans.LogData = secondParts[1].Trim();
			ans.StudentID = GetStudentID(JObject.Parse("{"+string.Join("|",secondParts.Skip(1)).Replace("\"{","{").Replace("}\"","}")+"}"));
			// var a = Newtonsoft.Json.JsonConvert.SerializeObject(secondParts[1]);
			

			return ans;			
		}

		public int GetStudentID(JObject json)
		{
			if ( !string.IsNullOrWhiteSpace(json["DATA"]?.ToString()))
			{
				json  =  JObject.Parse(json["DATA"]?.ToString());
			}
			
			var json2 = JObject.Parse(json.ToString().ToLower());

			int studentid = 0;
			var str = "";

			if ( !string.IsNullOrWhiteSpace(json2["uniquestudentid"]?.ToString()))
			{
				str =  json2["uniquestudentid"]?.ToString() ?? "";
			}
			else  if ( !string.IsNullOrWhiteSpace( json2["studentid"]?.ToString()))
			{
				str = json2["studentid"]?.ToString() ?? "";
			}
		

			int.TryParse(str ,out studentid);

			return studentid;
		}


		public int GetUserID(string path)
		{
			
			var name = System.IO.Path.GetFileName(path);

			var parts = name.Split("_");

			return int.Parse(parts[1]);
		}
		

    }
}
