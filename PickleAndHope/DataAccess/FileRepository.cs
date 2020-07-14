using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PickleAndHope.Helpers;

namespace PickleAndHope.DataAccess
{
    public class FileRepository
    {
        string ConnectionString;

        public FileRepository(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("PickleAndHope");
        }

        public void Add(UploadedFile file)
        {
            var sql = @"Insert into Files(FileName,FileContent,FileContentType,FileLength)
                        values (@filename,@content,@contenttype,@Size)";

            using (var db = new SqlConnection(ConnectionString))
            {
               db.Execute(sql, file);
            }

        }

    }
}
