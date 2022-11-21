using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog3B
{
    public class CallClass
    {
        string callNum;
        string callName;

        public CallClass()
        {
        }

        public string CallNum { get => callNum; set => callNum = value; }
        public string CallName { get => callName; set => callName = value; }
    }
}
