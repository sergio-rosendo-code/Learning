using System.Data;
using API.DAL.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.DAL.Repo;

public class DataAccessDapper(IConfiguration appConfig) : IDataAccess
{
    private readonly string _connectionString = appConfig.GetConnectionString("MainDB") ?? string.Empty;
    
    public async Task ExecuteSP(DataTransaction trans)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.QueryAsync(trans.SpName, param: trans.Params, commandType: CommandType.StoredProcedure);
            trans.Status = DataTransaction.TransactionStatus.Success;
        }
        catch(SqlException exception)
        {
            trans.Status = DataTransaction.TransactionStatus.Failure;
        }
    }

    public async Task ExecuteSP<T>(DataTransactionReturn<T> trans)
    {
        try
        {
            await using var connection = new SqlConnection(_connectionString);
            trans.Results =  await connection.QueryAsync<T>(trans.SpName, param: trans.Params, commandType: CommandType.StoredProcedure);
            trans.Status = DataTransaction.TransactionStatus.Success;
        }
        catch(SqlException exception)
        {
            trans.Status = DataTransaction.TransactionStatus.Failure;
        }
    }
}