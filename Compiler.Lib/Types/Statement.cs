using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class Statement
    {
        public StatementType type { set; get; }
        public Queue<Token> tokens { set; get; }

        public Statement()
        {
            tokens = new Queue<Token>();
        }
    }
}
