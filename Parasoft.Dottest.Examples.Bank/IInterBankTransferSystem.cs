using System;
using System.Collections.Generic;
using System.Text;
using Parasoft.Dottest.Examples.Bank.Transactions;

namespace Parasoft.Dottest.Examples.Bank
{
    interface IInterBankTransferSystem
    {
        void EnqueueTransfer(ExternalTransfer transfer);
    }
}
