using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Santase.Logic
{
    public class InternalGameExceptions: Exception
    {
        public InternalGameExceptions(string message)
            : base(message)
        {

        }
    }
}
