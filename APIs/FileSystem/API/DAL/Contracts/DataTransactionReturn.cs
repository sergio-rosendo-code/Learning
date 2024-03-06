namespace API.DAL.Contracts;

public class DataTransactionReturn<T> : DataTransaction
{
    public IEnumerable<T> Results { get; set; } = new List<T>();
}