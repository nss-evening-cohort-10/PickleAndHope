using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PickleAndHope.Models;
using Dapper;

namespace PickleAndHope.DataAccess
{
    public class PickleRepository
    {
        const string ConnectionString = "Server=localhost;Database=PickleAndHope;Trusted_Connection=True;";

        public Pickle Add(Pickle pickle)
        {
            var sql = @"insert into Pickle(NumberInStock,Price,Size,Type)
                        output inserted.*
                        values(@NumberInStock,@Price,@Size,@Type)";

            using (var db = new SqlConnection(ConnectionString))
            {
                var result = db.QueryFirstOrDefault<Pickle>(sql, pickle);
                return result;
            }
        }

        public void Remove(string type)
        {
            throw new NotImplementedException();
        }

        public Pickle Update(Pickle pickle)
        {
            var sql = @"update Pickle
                        set NumberInStock = NumberInStock + @NewStock
                        output inserted.*
                        where Id = @Id";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new
                {
                    NewStock = pickle.NumberInStock, 
                    Id = pickle.Id
                };

                return db.QueryFirstOrDefault<Pickle>(sql, parameters);
            }


        }

        public Pickle GetByType(string typeOfPickle)
        {
            var query = @"select *
                          from Pickle
                          where Type = @Type";

            //Sql Connection
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new {Type = typeOfPickle};

                var pickle = db.QueryFirstOrDefault<Pickle>(query, parameters);

                return pickle;
            }

        }

        public IEnumerable<Pickle> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<Pickle>("select * from pickle");
            }
        }

        public Pickle GetById(int id)
        {
            var query = @"select *
                          from Pickle
                          where id = @id
                                numberinstock = @numberInStock";

            using (var db = new SqlConnection(ConnectionString))
            {
                var parameters = new {Id = id};
                
                var pickle = db.QueryFirstOrDefault<Pickle>(query, parameters);
                return pickle;
            }
        }
    }
}
