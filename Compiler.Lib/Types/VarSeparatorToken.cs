using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class VarSeparatorToken : Token
    {
        public VarSeparatorToken(string content)
            : base(content)
        {
        }
    }
}
