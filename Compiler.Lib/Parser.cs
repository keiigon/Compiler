using Compiler.Lib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib
{
    public class Parser
    {
        public Token[] _tokens { get; private set; }

        private int readingPointer;

        public Parser(Token[] tokens)
        {
            this._tokens = tokens;

            readingPointer = 0;
        }

        public bool matchToken(){

        }
    }


}
