using API.DAL.Contracts;
using Dapper;

namespace API.DAL.Repo;

public interface IDataAccess
{
    Task ExecuteSP(DataTransaction trans);
    Task ExecuteSP<T>(DataTransactionReturn<T> trans);
}