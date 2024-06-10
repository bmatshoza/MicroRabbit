using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Commands
{
    public  class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int From,  int To, decimal Amount)
        
        {
            base.From = From;
            base.To = To;
            base.Amount = Amount;
            
        }
    }
}
