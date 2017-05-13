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

        public override string ToString()
        {
            string queue = "";
            foreach (var q in tokens)
            {
                queue += q.Content + ", ";
            }
            return string.Format("[{0}] - {1}", this.type.ToString(), queue.Substring(0, queue.Length - 2));
        }
    }
}
