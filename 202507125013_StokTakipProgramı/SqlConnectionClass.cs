using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _202507125013_StokTakipProgramı
{
    class SqlConnectionClass
    {
        public SqlConnection connection = new SqlConnection(
            @"Server=.\SQLEXPRESS;Database=Erensoft;Trusted_Connection=True;"
        );
    }
}