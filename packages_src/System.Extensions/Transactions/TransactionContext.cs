using System.Security.Claims;

namespace System.Transactions;

public class TransactionContext
{
    public ClaimsPrincipal User { get; }

    public DateTime DateTime { get; }

    public TransactionContext(ClaimsPrincipal user)
    {
        User = user;

        DateTime = DateTime.Now;
    }
}
