using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class StatementSeparatorToken : Token
    {
        public StatementSeparatorToken(string content)
            : base(content)
        {
        }
    }
}
