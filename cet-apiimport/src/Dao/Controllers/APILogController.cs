using System;
using System.Linq;
using cet_apiimport.Dao.Models;
using System.Collections.Generic;

namespace cet_apiimport.Dao.Controllers
{
    public class APILogController : cet_apiimport.DaoBase, cet_apiimport.Dao.Interfaces.IAPILogController
	{
        static String conn ; //= "Data Source=CETJNSQL02;Initial Catalog=CETNEWSMS_imp;Persist Security Info=True;User ID=sa_dev;Password=Slayer6zed";

        public APILogController( string connection) : base(connection)
        {
			conn = connection;
        }

		public List<APILog> Select(APILog table)
		{
			return this.ExecuteSQLDynamic<APILog>(table).ToList();
		}

        public int Insert(APILog table)
        {
            return this.ExecuteInsertSQL<APILog>(table);
        }

        public bool Update(APILog table)
        {
            return this.ExecuteUpdateSQL<APILog>(table);
        }
        public bool Exists(APILog table)
        {
            var records =  this.ExecuteSQLDynamic<APILog>(table);

			return records.Count() > 0;
        }

    }
}