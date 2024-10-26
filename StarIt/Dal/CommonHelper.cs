using System.Transactions;

namespace StarIt.Dal;

public class CommonHelper
{
    public static TransactionScope CreateTransactionScope(int seconds = 6000)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TimeSpan(0, 0, seconds),
            TransactionScopeAsyncFlowOption.Enabled
        );
    }
}