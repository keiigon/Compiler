using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Lib.Types
{
    public class OpenBraceToken : Token
    {
        static readonly Dictionary<string, BraceType> validBraces = new Dictionary<string, BraceType>()
        {
            { "(", BraceType.Round },
            { "{", BraceType.Curly },
            
        };

        public BraceType BraceType { get; private set; }

        public OpenBraceToken(string content)
            : base(content)
        {
            BraceType = validBraces[content];
        }
    }
}
