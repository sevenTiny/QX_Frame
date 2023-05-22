using System;
using System.Transactions;

namespace QX_Frame.Bantina
{
    /**
     * author:qixiao
     * time:2017-3-6 12:52:15
     * desc:transaction
     * */
    public class Transaction_Helper_DG
    {
        #region Transaction
        public static void Transaction(Action action)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                action();
                trans.Complete();
            }
        }
        #endregion
    }
}
