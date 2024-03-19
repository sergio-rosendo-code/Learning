using Dapper;

namespace API.DAL.Contracts;

public class DataTransaction
{
    public enum TransactionStatus
    {
        Success,
        Failure
    }
    
    public required string SpName { get; init; }
    public DynamicParameters Params { get; init; } = new DynamicParameters();
    public TransactionStatus Status { get; set; } = TransactionStatus.Failure;

}