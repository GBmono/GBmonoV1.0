using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Gbmono.EF.Sql
{
    public class SqlService
    {
        // EF Dbcontext
        private readonly DbContext _context;

        // ctor
        public SqlService(DbContext context)
        {
            _context = context;
        }
    }
}
