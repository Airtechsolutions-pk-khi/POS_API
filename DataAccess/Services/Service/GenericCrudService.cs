using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Services.IService;

namespace DataAccess.Services.Service
{
    public class GenericCrudService : IGenericCrudService
    {

        private readonly IConfiguration _config;
        public GenericCrudService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(
            string SP,
            U parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                return await con.QueryAsync<T>(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC GET ALL

        public async Task<T> LoadSingleOrDefaultData<T, U>(
            string SP,
            U parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                return await con.QueryFirstOrDefaultAsync<T>(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC GET SINGLE OR DEFAULT

        public async Task SaveData<T>(
            string SP,
            T parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                await con.ExecuteAsync(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC SAVE UPDATE AND DELETE 

        public async Task<S> SaveSingleQueryable<S, T>(
            string SP,
            T parameters)
        {
            using (IDbConnection con = new SqlConnection(_config.GetConnectionString("Default")))
            {
                if (con.State != ConnectionState.Open) con.Open();

                return await con.QuerySingleOrDefaultAsync<S>(
                    SP, parameters, commandType: CommandType.StoredProcedure);
            }
        } // GENERIC SAVE UPDATE AND DELETE QUERYABLE 
    }
}
