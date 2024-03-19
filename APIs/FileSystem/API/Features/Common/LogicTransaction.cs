using API.DAL.Repo;

namespace API.Features.Common;

public enum LogicTransactionStatus
{
    Conflict,
    Created, 
    InternalError
}

public class LogicTransaction<I, R>
{
    public I? Request { get; set; }
    public R? Result { get; set; }
    public IDataAccess? DataAccess { get; set; } 
    public LogicTransactionStatus Status { get; set; } = LogicTransactionStatus.InternalError;
}