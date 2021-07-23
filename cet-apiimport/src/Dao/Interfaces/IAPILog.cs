using System;
using System.Linq;
using cet_apiimport.Dao.Models;
using System.Collections.Generic;

namespace cet_apiimport.Dao.Interfaces
{
    public interface IAPILogController
	{
		List<APILog> Select(APILog table);
        int Insert(APILog table);
        bool Update(APILog table);
		bool Exists(APILog table);
    }
}