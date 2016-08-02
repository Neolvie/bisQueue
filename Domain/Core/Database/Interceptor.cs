using System;
using NHibernate;

namespace Shared.NHibernate
{
  public class Interceptor : EmptyInterceptor
  {
    public static event EventHandler TransactionComplete;

    protected virtual void OnTransactionComplete()
    {
      TransactionComplete?.Invoke(this, EventArgs.Empty);
    }

    public override void AfterTransactionCompletion(ITransaction tx)
    {
      base.AfterTransactionCompletion(tx);

      OnTransactionComplete();
    }
  }
}