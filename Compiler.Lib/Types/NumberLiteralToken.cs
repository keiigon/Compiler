using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class NumberLiteralToken : Token
    {
        public int Number
        {
            get
            {
                return number;
            }
        }
        private int number;

        public NumberLiteralToken(string content)
            : base(content)
        {
          
        }
    }
}
