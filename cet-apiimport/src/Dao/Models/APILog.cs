// using Injector.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
// using NLog;
namespace cet_apiimport.Dao.Models
{
	public class APILog : cet_apiimport.TableBase
	{

		public int ID { get; set; } = 0;
		public DateTime DateCreated { get; set; }
		public string URL { get; set; } = "";
		public int UserID { get; set; } = 0;
		public int StudentID { get; set; } = 0;
		public string LogData { get; set; } = "";

		public override string TableName => "APILog";

		public override string DBKey => "ID";

		public APILog()
		{

		}

		public override string[] GetColumns()
		{
			return new string[]{
				"ID",
				"DateCreated",
				"URL",
				"UserID",
				"StudentID",
				"LogData"
			};
		}
	}

}