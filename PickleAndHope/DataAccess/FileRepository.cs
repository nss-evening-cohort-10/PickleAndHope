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
                        values (@FileName,@FileContent,@FileContentType,@FileLength)";

            using (var db = new SqlConnection(ConnectionString))
            {
               db.Execute(sql, file);
            }

        }

        public UploadedFile GetById(int fileId)
        {
            var sql = @"Select * From Files Where Id = @fileId";

            using (var db = new SqlConnection(ConnectionString))
            {
                return db.QueryFirst<UploadedFile>(sql, new {fileId = fileId});
            }

        }

    }
}
