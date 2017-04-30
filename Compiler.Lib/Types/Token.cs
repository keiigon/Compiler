using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class Token
    {
        public string Content { get; private set; }

        public Token(string content)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            return string.Format("[{0}] - {1}", this.GetType().Name, Content);
        }
    }
}
